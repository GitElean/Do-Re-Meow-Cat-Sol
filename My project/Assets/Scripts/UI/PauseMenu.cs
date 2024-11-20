using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject winMenu;
    void Start()
    {
        winMenu.SetActive(false);
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
        GameManager.instance.isPaused = false;
        GameManager.instance.death = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.instance.isPaused)
            {
                ResumeGame();

            }
            else
            {
                PauseGame();

            }
        }

        if (GameManager.instance.death)
        {
            deathMenu.SetActive(true);
        }

        if (GameManager.instance.win)
        {
            deathMenu.SetActive(true);
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameManager.instance.isPaused = true;
    }

 
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameManager.instance.isPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
