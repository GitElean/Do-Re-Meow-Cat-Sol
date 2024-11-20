using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private EventInstance musicInstance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Más de un AudioManager en escena");
            return;
        }
        instance = this;

        // Configurar el tamaño del buffer de FMOD para evitar starvation
        ConfigureFMODBuffer();

        // Cargar bancos de sonido
        RuntimeManager.LoadBank("SFX", true); // Banco de SFX
        RuntimeManager.LoadBank("Music", true); // Banco de música
    }

    private void ConfigureFMODBuffer()
    {
        // Obtén el sistema de FMOD Studio
        FMOD.Studio.System studioSystem = FMODUnity.RuntimeManager.StudioSystem;

        // Accede al sistema de bajo nivel utilizando un puntero
        FMOD.System lowLevelSystem;
        studioSystem.getCoreSystem(out lowLevelSystem);

        // Configurar tamaño del buffer (1024 samples, 4 buffers)
        lowLevelSystem.setDSPBufferSize(2048, 6);

        Debug.Log("FMOD buffer size configured: 1024 samples, 4 buffers.");
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    void Start()
    {
        PlaySong(GameManager.instance.levelSong);
    }

    public void PlaySong(EventReference songEvent)
    {
        if (musicInstance.isValid())
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            musicInstance.release();
        }

        musicInstance = RuntimeManager.CreateInstance(songEvent);
        musicInstance.start();
    }

    private void Update()
    {
        HandleMusicPause();
    }

    private void HandleMusicPause()
    {
        if (musicInstance.isValid())
        {
            // Pausar música si el juego está en pausa, el jugador ha muerto o ha ganado
            if (GameManager.instance.isPaused || GameManager.instance.death || GameManager.instance.win)
            {
                musicInstance.setPaused(true);
            }
            else
            {
                // Reanudar música si ninguna de las condiciones se cumple
                musicInstance.setPaused(false);
            }
        }
    }

    private void OnDestroy()
    {
        if (musicInstance.isValid())
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            musicInstance.release();
        }
    }

    public void SetMusicParameter(string parameterName, float value)
    {
        if (musicInstance.isValid())
        {
            musicInstance.setParameterByName(parameterName, value);
        }
    }
}
