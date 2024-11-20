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
   // private bool isJumping = false;   // Controla si el jugador está saltando
    private bool canDash = true;      // Controla si el jugador puede hacer dash
    public float dashCooldown = 0.2f;   // Tiempo de enfriamiento del dash (3 segundos)

    // Sonidos
    [SerializeField] private EventReference moveSound;
    [SerializeField] private EventReference winSound;
    [SerializeField] private EventReference hitSound;
    [SerializeField] private EventReference deathSound;

    void Update()
    {
        HandleMovement();
        //HandleJump();
        if (GameManager.instance.win)
        {
            AudioManager.instance.PlayOneShot(winSound, transform.position);
        }
        if (GameManager.instance.death)
        {
            AudioManager.instance.PlayOneShot(winSound, transform.position);
        }
    }

    private void PlaySound(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle")) // Detectar colisión con un obstáculo
        {
            AudioManager.instance.PlayOneShot(hitSound, transform.position); // Reproducir el sonido de golpe
            GameManager.instance.ReduceLife(); // Delegar reducción de vidas al GameManager
            Destroy(other.gameObject); // Destruir el obstáculo al colisionar
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
                    //PlaySound(dashSound); // Sonido de dash
                    //StartCoroutine(DashCooldown());
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < GameController.instance.lanes.Length - 2) // Dash a la derecha
                {
                    currentLane += 2;
                    //PlaySound(dashSound); // Sonido de dash
                    //StartCoroutine(DashCooldown());
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

        Vector3 targetPosition = new Vector3(GameController.instance.lanes[currentLane].x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    
    /*
    IEnumerator Jump()
    {
        isJumping = true;

        float elapsedTime = 0;
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + jumpHeight, transform.position.z);

        while (elapsedTime < jumpDuration / 2)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / (jumpDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;
        originalPosition = transform.position;
        targetPosition = new Vector3(transform.position.x, 0, transform.position.z);

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

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            PlaySound(jumpSound); // Sonido de salto
            StartCoroutine(Jump());
        }
    }*/
}
