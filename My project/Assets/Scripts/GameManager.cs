using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private EventReference levelSong; // Canción del nivel
    public GameObject deathMenu;
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
        AudioManager.instance.PlaySong(levelSong);
        deathMenu.SetActive(false);
    }

    public void ReduceLife()
    {
        lives--;
        Debug.Log("Vidas restantes: " + lives);

        if (lives == 1)
        {
            // Cambiar parámetro a música intensa
            AudioManager.instance.SetMusicParameter("LifeState", 1f);
        }
        else if (lives <= 0)
        {
            // Cambiar parámetro a música de horror
            AudioManager.instance.SetMusicParameter("LifeState", 2f);
            Debug.Log("Game Over!");
            deathMenu.SetActive(true);
            Time.timeScale = 0f; // Pausar el juego
        }
    }
}
