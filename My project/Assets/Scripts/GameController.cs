using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Vector3[] lanes = new Vector3[4]; // Posiciones de los carriles
    public float obstacleSpawnZ = -45f; // Coordenada Z donde se generan los obst�culos

    public ObstacleRoutine currentRoutine; // Rutina espec�fica de la canci�n o nivel

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Definir las posiciones de los carriles
        lanes[0] = new Vector3(-12f, 2f, 30f);
        lanes[1] = new Vector3(-4f, 2f, 30f);
        lanes[2] = new Vector3(4f, 2f, 30f);
        lanes[3] = new Vector3(12f, 2f, 30f);

        // No es necesario llamar a currentRoutine.StartRoutine() porque se ejecuta autom�ticamente
        if (currentRoutine != null)
        {
            // currentRoutine se encargar� de iniciar la generaci�n de obst�culos
        }
    }

    public void SpawnObstacle(int laneIndex, GameObject obstaclePrefab)
    {
        Vector3 spawnPosition = new Vector3(lanes[laneIndex].x, 0f, obstacleSpawnZ);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
