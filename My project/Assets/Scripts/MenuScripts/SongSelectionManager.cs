using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;


[System.Serializable]
public class Song
{
    public string songName;  // Nombre de la canción
    public string sceneName; // Nombre de la escena asociada a la canción
}

public class SongSelectionManager : MonoBehaviour
{
    public GameObject difficultyPanelPrefab; // Prefab del menú de dificultad pequeño
    public Button songButtonPrefab;       // Prefab para crear los botones de canciones
    public Transform songButtonContainer; // Contenedor donde se colocarán los botones de las canciones

    public List<Song> songs = new List<Song>(); // Lista de canciones
    private Song selectedSong;  // La canción seleccionada
    private string selectedDifficulty; // Dificultad seleccionada (Fácil, Normal, Difícil)

    void Start()
    {
        GenerateSongButtons(); // Generar los botones de las canciones dinámicamente al iniciar
    }

    // Generar los botones de las canciones dinámicamente
    void GenerateSongButtons()
    {
        foreach (Song song in songs)
        {
            // Instanciar el botón de canción
            Button newButton = Instantiate(songButtonPrefab, songButtonContainer);

            // Asignar el nombre de la canción al texto del botón usando TextMeshPro
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = song.songName;

            // Asignar el listener al botón para mostrar el menú de dificultades al hacer clic
            newButton.onClick.AddListener(() => OnSongSelected(song, newButton));
        }
    }

    // Cuando se selecciona una canción, muestra el menú de dificultad
    void OnSongSelected(Song song, Button songButton)
    {
        Debug.Log("OnSongSelected llamado para la canción: " + song.songName);

        selectedSong = song;

        // Instanciar el nuevo panel de dificultades debajo del botón de la canción seleccionada
        GameObject difficultyMenu = Instantiate(difficultyPanelPrefab, songButton.transform);
        Debug.Log("DifficultyPanel instanciado para la canción: " + song.songName);

        // Asegurarse de que el panel esté activo y su escala sea la correcta
        difficultyMenu.SetActive(true);
        difficultyMenu.transform.localScale = new Vector3(1, 1, 1);

        // Mostrar el panel en una posición fija de prueba (centro de la pantalla)
        difficultyMenu.transform.position = new Vector3(0, 0, 0);

        // Verificar el tamaño y posición
        RectTransform rect = difficultyMenu.GetComponent<RectTransform>();
        Debug.Log("Posición del DifficultyPanel: " + rect.position);
        Debug.Log("Tamaño del DifficultyPanel: " + rect.sizeDelta);

        // Asignar funciones a los botones de dificultad (fácil, normal, difícil)
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

    // Cargar la escena del juego con la canción y dificultad seleccionada
    void LoadGame()
    {
        // Pasar la dificultad y la canción seleccionada al GameManager en la siguiente escena usando PlayerPrefs
        PlayerPrefs.SetString("SelectedDifficulty", selectedDifficulty);
        PlayerPrefs.SetString("SelectedSong", selectedSong.songName);

        // Cargar la escena correspondiente a la canción seleccionada
        SceneManager.LoadScene(selectedSong.sceneName);
    }
}
