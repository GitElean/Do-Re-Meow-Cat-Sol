using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private EventReference levelSong; // Canción del nivel

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Mantener el GameManager entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Reproducir la canción al inicio del nivel
        AudioManager.instance.PlaySong(levelSong);
    }

    // Otros métodos globales pueden añadirse aquí, como manejar las vidas o puntuación
}
