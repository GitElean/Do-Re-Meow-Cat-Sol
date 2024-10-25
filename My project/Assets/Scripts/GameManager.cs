using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private EventReference levelSong; // Canci�n del nivel

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
        // Reproducir la canci�n al inicio del nivel
        AudioManager.instance.PlaySong(levelSong);
    }

    // Otros m�todos globales pueden a�adirse aqu�, como manejar las vidas o puntuaci�n
}
