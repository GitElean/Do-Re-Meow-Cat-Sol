using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider progressBar; // Referencia al Slider

    private float songDuration; // Duración de la canción
    private float elapsedTime = 0f; // Tiempo transcurrido

    void Start()
    {
        // Obtener la duración de la canción desde el GameManager
        songDuration = GameManager.instance.songDuration;

        // Inicializar el Slider
        if (progressBar != null)
        {
            progressBar.maxValue = songDuration;
            progressBar.value = 0;
        }
    }

    void Update()
    {
        // Verificar si el juego está pausado
        if (GameManager.instance.isPaused)
        {
            return; // Salir de Update si el juego está pausado
        }

        // Continuar actualizando el progreso si no está pausado
        if (elapsedTime < songDuration)
        {
            // Actualizar el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            // Actualizar el valor del Slider
            if (progressBar != null)
            {
                progressBar.value = elapsedTime;
            }
        }
        else
        {
            // Si la canción termina, puedes ocultar o bloquear la barra
            if (progressBar != null)
            {
                progressBar.value = songDuration;
            }
        }
    }
}
