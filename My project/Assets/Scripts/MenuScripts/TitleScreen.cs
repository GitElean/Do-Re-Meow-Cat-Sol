using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitleScreen : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject titleScreenPanel;

    void Update()
    {
        // Detecta si se ha presionado cualquier tecla o el mouse
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            ShowMainMenu();
        }
    }

    void ShowMainMenu()
    {
        // Ocultar el TitleScreen y mostrar el MainMenu
        titleScreenPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
