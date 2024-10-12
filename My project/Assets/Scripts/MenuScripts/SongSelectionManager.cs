using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;


[System.Serializable]
public class Song
{
    public string songName;  // Nombre de la canci�n
    public string sceneName; // Nombre de la escena asociada a la canci�n
}

public class SongSelectionManager : MonoBehaviour
{
    public GameObject difficultyPanelPrefab; // Prefab del men� de dificultad peque�o
    public Button songButtonPrefab;       // Prefab para crear los botones de canciones
    public Transform songButtonContainer; // Contenedor donde se colocar�n los botones de las canciones

    public List<Song> songs = new List<Song>(); // Lista de canciones
    private Song selectedSong;  // La canci�n seleccionada
    private string selectedDifficulty; // Dificultad seleccionada (F�cil, Normal, Dif�cil)

    void Start()
    {
        GenerateSongButtons(); // Generar los botones de las canciones din�micamente al iniciar
    }

    // Generar los botones de las canciones din�micamente
    void GenerateSongButtons()
    {
        foreach (Song song in songs)
        {
            // Instanciar el bot�n de canci�n
            Button newButton = Instantiate(songButtonPrefab, songButtonContainer);

            // Asignar el nombre de la canci�n al texto del bot�n usando TextMeshPro
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = song.songName;

            // Asignar el listener al bot�n para mostrar el men� de dificultades al hacer clic
            newButton.onClick.AddListener(() => OnSongSelected(song, newButton));
        }
    }

    // Cuando se selecciona una canci�n, muestra el men� de dificultad
    void OnSongSelected(Song song, Button songButton)
    {
        Debug.Log("OnSongSelected llamado para la canci�n: " + song.songName);

        selectedSong = song;

        // Instanciar el nuevo panel de dificultades debajo del bot�n de la canci�n seleccionada
        GameObject difficultyMenu = Instantiate(difficultyPanelPrefab, songButton.transform);
        Debug.Log("DifficultyPanel instanciado para la canci�n: " + song.songName);

        // Asegurarse de que el panel est� activo y su escala sea la correcta
        difficultyMenu.SetActive(true);
        difficultyMenu.transform.localScale = new Vector3(1, 1, 1);

        // Mostrar el panel en una posici�n fija de prueba (centro de la pantalla)
        difficultyMenu.transform.position = new Vector3(0, 0, 0);

        // Verificar el tama�o y posici�n
        RectTransform rect = difficultyMenu.GetComponent<RectTransform>();
        Debug.Log("Posici�n del DifficultyPanel: " + rect.position);
        Debug.Log("Tama�o del DifficultyPanel: " + rect.sizeDelta);

        // Asignar funciones a los botones de dificultad (f�cil, normal, dif�cil)
        Button easyButton = difficultyMenu.transform.Find("EasyButton").GetComponent<Button>();
        Button normalButton = difficultyMenu.transform.Find("NormalButton").GetComponent<Button>();
        Button hardButton = difficultyMenu.transform.Find("HardButton").GetComponent<Button>();

        easyButton.onClick.AddListener(() => OnDifficultySelected("Easy"));
        normalButton.onClick.AddListener(() => OnDifficultySelected("Normal"));
        hardButton.onClick.AddListener(() => OnDifficultySelected("Hard"));
    }



    // Cuando se selecciona la dificultad
    void OnDifficultySelected(string difficulty)
    {
        selectedDifficulty = difficulty;
        LoadGame();
    }

    // Cargar la escena del juego con la canci�n y dificultad seleccionada
    void LoadGame()
    {
        // Pasar la dificultad y la canci�n seleccionada al GameManager en la siguiente escena usando PlayerPrefs
        PlayerPrefs.SetString("SelectedDifficulty", selectedDifficulty);
        PlayerPrefs.SetString("SelectedSong", selectedSong.songName);

        // Cargar la escena correspondiente a la canci�n seleccionada
        SceneManager.LoadScene(selectedSong.sceneName);
    }
}
