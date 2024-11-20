using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterMovement : MonoBehaviour
{
    public GameObject Monster; // El objeto que representa al fantasma
    public float movementRange = 0.5f; // Rango máximo de movimiento en cada dirección
    public float movementSpeed = 1.0f; // Velocidad del movimiento

    private Vector3 startPosition;

    void Start()
    {
        // Guardamos la posición inicial del objeto
        startPosition = Monster.transform.position;
    }

    void Update()
    {
        // Calculamos un movimiento aleatorio en los ejes X e Y usando Perlin Noise para suavidad
        float offsetX = Mathf.PerlinNoise(Time.time * movementSpeed, 0) * movementRange - (movementRange / 2);
        float offsetY = Mathf.PerlinNoise(0, Time.time * movementSpeed) * movementRange - (movementRange / 2);

        // Actualizamos la posición del objeto con el movimiento calculado
        Monster.transform.position = startPosition + new Vector3(offsetX, offsetY, 0);
    }
}
