using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPaused;
    public bool death;
    public bool win;
    public EventReference levelSong; // Canción del nivel
    public float songDuration = 120f;
    // public GameObject deathMenu;
    public int lives = 3;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantener el GameManager entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Reproducir la canción al inicio del nivel
        win = false;
        lives = 3;
    }

    public void ReduceLife()
    {
        lives--;

        if (lives <= 0)
        {
            // Cambiar parámetro a música de horror
            Time.timeScale = 0f; // Pausar el juego
            death = true;
        }
    }
}
