using UnityEngine;

public class BeatPulse : MonoBehaviour
{
    public float bpm = 120f; // BPM de la canción
    public float pulseScale = 1.2f; // Escala máxima durante el pulso
    public float pulseDuration = 0.1f; // Duración del pulso en segundos

    private float beatInterval; // Tiempo entre beats
    private float nextBeatTime; // Momento en que ocurre el próximo beat
    private Vector3 originalScale; // Escala original del objeto
    private bool isPulsing = false; // Para controlar el pulso actual

    void Start()
    {
        // Calcula el intervalo entre beats
        beatInterval = 60f / bpm;

        // Configura el tiempo para el primer beat
        nextBeatTime = Time.time + beatInterval;

        // Guarda la escala original
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Revisa si es hora de un nuevo beat
        if (Time.time >= nextBeatTime)
        {
            StartCoroutine(Pulse());
            nextBeatTime += beatInterval; // Actualiza el tiempo para el próximo beat
        }
    }

    private System.Collections.IEnumerator Pulse()
    {
        if (isPulsing) yield break; // Evita múltiples pulsos al mismo tiempo

        isPulsing = true;

        // Escalar hacia el tamaño máximo
        Vector3 targetScale = originalScale * pulseScale;
        float elapsedTime = 0f;

        while (elapsedTime < pulseDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / pulseDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Regresar al tamaño original
        elapsedTime = 0f;
        while (elapsedTime < pulseDuration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / pulseDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale; // Asegura la escala final
        isPulsing = false;
    }
}
