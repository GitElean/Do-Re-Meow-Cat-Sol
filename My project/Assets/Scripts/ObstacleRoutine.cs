using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRoutine : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Prefabs de los diferentes obst�culos
    public int BPM = 120; // Velocidad de la canci�n en BPM
    public float songDuration = 120f; // Duraci�n de la canci�n en segundos
    public int maxObstaclesPerRow = 2; // M�ximo de obst�culos por fila (para evitar llenar todos los carriles)

    private float timePerBeat;
    private int totalBeats;

    void Start()
    {
        // Calcular el tiempo entre beats basado en los BPM
        timePerBeat = 60f / BPM;
        totalBeats = Mathf.FloorToInt(songDuration / timePerBeat);

        // Iniciar la rutina de generaci�n de obst�culos
        StartCoroutine(GenerateObstaclesRoutine());
    }

    IEnumerator GenerateObstaclesRoutine()
    {
        for (int i = 0; i < totalBeats; i++)
        {
            GenerateObstacleRow(); // Generar una fila de obst�culos
            yield return new WaitForSeconds(timePerBeat); // Esperar el tiempo entre beats
        }
    }

    void GenerateObstacleRow()
    {
        // Asegurarse de que los prefabs de obst�culos est�n asignados
        if (obstaclePrefabs.Length == 0)
        {
            Debug.LogError("No hay prefabs de obst�culos disponibles para generar.");
            return;
        }

        // Escoger cu�ntos obst�culos poner en esta fila (m�ximo 2)
        int obstacleCount = Random.Range(1, maxObstaclesPerRow + 1);
        List<int> usedLanes = new List<int>();

        for (int i = 0; i < obstacleCount; i++)
        {
            // Seleccionar un carril al azar que no haya sido usado a�n
            int laneIndex;
            do
            {
                laneIndex = Random.Range(0, GameController.instance.lanes.Length);  // Usar los carriles definidos en GameController
            }
            while (usedLanes.Contains(laneIndex)); // Evitar usar el mismo carril dos veces

            usedLanes.Add(laneIndex);

            // Seleccionar un obst�culo al azar (jumpable o notJumpable)
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            // Instanciar el obst�culo en la posici�n del carril desde el array lanes de GameController
            Vector3 spawnPosition = GameController.instance.lanes[laneIndex]; // Usar las posiciones del array lanes del GameController
            spawnPosition.z = GameController.instance.obstacleSpawnZ; // Ajustar la posici�n Z para que siempre spawneen adelante

            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
