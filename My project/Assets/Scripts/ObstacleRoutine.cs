using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ObstacleBeat
{
    public float time;     // Tiempo en segundos para generar el obstáculo
    public int laneIndex;  // El carril donde se genera el obstáculo
    public bool isJumpable; // Si el obstáculo es saltable o no
}

public class ObstacleRoutine : MonoBehaviour
{
    public List<ObstacleBeat> obstacleBeats; // Lista de tiempos y tipos de obstáculos
    private int currentBeatIndex = 0;
    private float startTime;

    // Referencias a los prefabs de obstáculos
    public GameObject jumpableObstaclePrefab;
    public GameObject unjumpableObstaclePrefab;

    public void StartRoutine()
    {
        startTime = Time.time;
        StartCoroutine(RunRoutine());
    }

    IEnumerator RunRoutine()
    {
        while (currentBeatIndex < obstacleBeats.Count)
        {
            float beatTime = obstacleBeats[currentBeatIndex].time;
            if (Time.time - startTime >= beatTime)
            {
                SpawnObstacle(obstacleBeats[currentBeatIndex]);
                currentBeatIndex++;
            }
            yield return null;
        }
    }

    void SpawnObstacle(ObstacleBeat beat)
    {
        GameObject prefabToSpawn = beat.isJumpable ? jumpableObstaclePrefab : unjumpableObstaclePrefab;
        Vector3 spawnPosition = new Vector3(GameController.instance.lanes[beat.laneIndex].x, 0f, GameController.instance.obstacleSpawnZ);
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}
