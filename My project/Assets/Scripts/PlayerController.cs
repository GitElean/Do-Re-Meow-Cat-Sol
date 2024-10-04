using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private int currentLane = 1;  // Comenzamos en el carril 2 (índice 1)
    public float moveSpeed = 5f;  // Velocidad de movimiento entre carriles
    public float jumpHeight = 2f; // Altura del salto
    public float jumpDuration = 0.5f; // Duración del salto (medio segundo)
    private bool isJumping = false;   // Controla si el jugador está saltando
    private bool canDash = true;      // Controla si el jugador puede hacer dash
    public float dashCooldown = 3f;   // Tiempo de enfriamiento del dash (3 segundos)

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        // Detectar dash con Shift presionado
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (canDash)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 1) // Dash a la izquierda
                {
                    currentLane -= 2;
                    StartCoroutine(DashCooldown());
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < GameController.instance.lanes.Length - 2) // Dash a la derecha
                {
                    currentLane += 2;
                    StartCoroutine(DashCooldown());
                }
            }
        }
        else
        {
            // Movimiento normal a la izquierda
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentLane > 0)
                {
                    currentLane--;
                }
            }

            // Movimiento normal a la derecha
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (currentLane < GameController.instance.lanes.Length - 1)
                {
                    currentLane++;
                }
            }
        }

        // Movimiento suave entre carriles
        transform.position = Vector3.Lerp(transform.position, GameController.instance.lanes[currentLane], Time.deltaTime * moveSpeed);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(Jump());
        }
    }

    IEnumerator Jump()
    {
        isJumping = true;

        // Elevar al jugador
        float elapsedTime = 0;
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + jumpHeight, transform.position.z);

        while (elapsedTime < jumpDuration / 2)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / (jumpDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Bajar al jugador
        elapsedTime = 0;
        originalPosition = transform.position;  // Ahora la posición actual es el punto más alto
        targetPosition = new Vector3(transform.position.x, 0, transform.position.z);  // Volver a y=0

        while (elapsedTime < jumpDuration / 2)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / (jumpDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isJumping = false;
    }

    IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
