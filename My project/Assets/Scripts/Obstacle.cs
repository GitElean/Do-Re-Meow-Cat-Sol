using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento hacia el jugador

    void Update()
    {
        // Mover el obst�culo hacia el jugador en el eje Z positivo
        transform.position += Vector3.forward * speed * Time.deltaTime;

        // Destruir el obst�culo si pasa del jugador (Z >= 51)
        if (transform.position.z >= 51f)
        {
            Destroy(gameObject);
        }
    }
}
