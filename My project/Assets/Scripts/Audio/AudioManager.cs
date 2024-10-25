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

        // Cargar bancos de sonido (ejemplo para SFX y Music)
        RuntimeManager.LoadBank("SFX", true); // Carga el banco de SFX
        RuntimeManager.LoadBank("Music", true); // Si tienes un banco separado para la música
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
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

    private void OnDestroy()
    {
        if (musicInstance.isValid())
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            musicInstance.release();
        }
    }
}
