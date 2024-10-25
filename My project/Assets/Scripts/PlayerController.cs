using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerController : MonoBehaviour
{
    private int currentLane = 1;  // Comenzamos en el carril 2 (índice 1)
    public float moveSpeed = 5f;  // Velocidad de movimiento entre carriles
    public float jumpHeight = 2f; // Altura del salto
    public float jumpDuration = 0.5f; // Duración del salto (medio segundo)
    private bool isJumping = false;   // Controla si el jugador está saltando
    private bool canDash = true;      // Controla si el jugador puede hacer dash
    public float dashCooldown = 0.2f;   // Tiempo de enfriamiento del dash (3 segundos)
    public int lives = 3; //vidas del gato

    //sonidos
    [SerializeField] private EventReference moveSound;
    [SerializeField] private EventReference jumpSound;
    [SerializeField] private EventReference dashSound;
    [SerializeField] private EventReference hitSound;
    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void PlaySound(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle")) // Detectar colisión con un obstáculo
        {
            TakeDamage();
            AudioManager.instance.PlayOneShot(hitSound, transform.position); // Reproducir el sonido de golpe
            Destroy(other.gameObject); // Destruir el obstáculo al colisionar
        }
    }

    private void TakeDamage()
    {
        lives--;
        Debug.Log("Vidas restantes: " + lives);

        if (lives <= 0)
        {
            // Aquí podrías implementar lógica de Game Over
            Debug.Log("Game Over!");
        }
    }
    void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (canDash)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 1) // Dash a la izquierda
                {
                    currentLane -= 2;
                    PlaySound(dashSound); // Sonido de dash
                    StartCoroutine(DashCooldown());
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < GameController.instance.lanes.Length - 2) // Dash a la derecha
                {
                    currentLane += 2;
                    PlaySound(dashSound); // Sonido de dash
                    StartCoroutine(DashCooldown());
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentLane > 0)
                {
                    currentLane--;
                    PlaySound(moveSound); // Sonido de cambio de carril
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (currentLane < GameController.instance.lanes.Length - 1)
                {
                    currentLane++;
                    PlaySound(moveSound); // Sonido de cambio de carril
                }
            }
        }

        transform.position = Vector3.Lerp(transform.position, GameController.instance.lanes[currentLane], Time.deltaTime * moveSpeed);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            PlaySound(jumpSound); // Sonido de salto
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
