using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject titleScreenPanel;
    public GameObject mainMenuPanel;
    public GameObject songSelectionPanel;
    public GameObject optionsPanel;
    public GameObject controlsPanel; 
    public GameObject soundPanel; 
    public GameObject creditsPanel;

    void Start()
    {
        ShowTitleScreen(); // Iniciar con el título
    }

    public void ShowTitleScreen()
    {
        titleScreenPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        songSelectionPanel.SetActive(false);
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(false); // Empieza oculto
        soundPanel.SetActive(false); // Empieza oculto
        creditsPanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        titleScreenPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void GoBackToMainMenu()
    {
        // Desactiva todos los paneles, asegurándose de que sólo el menú principal esté visible
        songSelectionPanel.SetActive(false);
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        soundPanel.SetActive(false);
        creditsPanel.SetActive(false);

        // Activa el menú principal
        mainMenuPanel.SetActive(true);
    }
    public void ShowSongSelection()
    {
        mainMenuPanel.SetActive(false);
        songSelectionPanel.SetActive(true);
    }

    public void SelectDifficulty(string difficulty)
    {
        // Cambia a la escena del nivel basado en la dificultad seleccionada
        SceneManager.LoadScene(difficulty);
    }

    public void ShowOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        controlsPanel.SetActive(false); // Ocultamos los sub-paneles
        soundPanel.SetActive(false);
    }

    public void GoBackToOptions()
    {
        controlsPanel.SetActive(false);
        soundPanel.SetActive(false);
        optionsPanel.SetActive(true); // Activa las opciones de nuevo
    }

    public void ShowControls()
    {
        controlsPanel.SetActive(true); // Muestra los controles
        soundPanel.SetActive(false);   // Oculta el panel de sonido por si está activo
    }

    public void ShowSound()
    {
        soundPanel.SetActive(true);    // Muestra el panel de sonido
        controlsPanel.SetActive(false); // Oculta el panel de controles por si está activo
    }

    public void ShowCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
