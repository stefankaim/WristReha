using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Menu : MonoBehaviour
{
    /// <summary>
    /// Text to manipulate the duration
    /// </summary>
    public static Text DurationText;

    /// <summary>
    /// Slider to set the duration of a game
    /// </summary>
    public static Slider DurationSlider;

    /// <summary>
    /// To know which game is choosen
    /// </summary>
    public static int game;

    /// <summary>
    /// To know the difficulty of the game
    /// </summary>
    public static string difficulty;

    /// <summary>
    /// To know the level of the game
    /// </summary>
    public static int level;

    /// <summary>
    /// The parameter of the wrist, to know if the controls have to be flipped (* -1)
    /// As the majority of people use the right hand, the value is set to default 1 (right)
    /// </summary>
    public static int wrist = 1;

    /// <summary>
    /// Sets the time of how long a game should be played unitl its set as finished
    /// </summary>
    public static float timeToGame = 300f;

    /// <summary>
    /// Information about the connected JoyCon
    /// </summary>
    public GameObject JoyConManager;
    private List<Joycon> joycons;
    private Joycon activeJoycon;
    private int jc_ind = 0;
    public TMPro.TextMeshProUGUI conn;
    public GameObject startMenu;
    public GameObject gameMenu;

    private void Awake()
    {

    }

    private void Start()
    {
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            conn.text = "Kein JoyCon gefunden";
        }
        else
        {
            activeJoycon = joycons[jc_ind];
            if (activeJoycon.isLeft) conn.text = "Linker JoyCon verbunden";
            else conn.text = "Rechter JoyCon verbunden";
        }

        if (Settings.gameScreen)
        {
            startMenu.SetActive(false);
            gameMenu.SetActive(true);
        }
        else
        {
            startMenu.SetActive(true);
            gameMenu.SetActive(false);
        }
    }

    /// <summary>
    /// Starts the choosen game with choosen difficulty
    /// </summary>
    public void StartGameDiff()
    {
        if (activeJoycon != null) activeJoycon.Detach();
        //Debug.Log("Game" + game + difficulty);
        //SceneManager.LoadScene("Game" + game + difficulty);
        Settings.sceneToLoad = "Game" + game + difficulty;
        SceneManager.LoadScene("Loading");

        //maybe do parameters
        //SceneManager.LoadScene(string sceneName, LoadSceneParameters parameters);
    }

    /// <summary>
    /// Starts the choosen game with choosen level
    /// </summary>
    public void StartGameLevel()
    {
        if (activeJoycon != null) activeJoycon.Detach();
        if (level > 0)
        {
            //Debug.Log("Game" + game + "Level" + level);
            //SceneManager.LoadScene("Game" + game + "Level" + level);
            Settings.sceneToLoad = "Game" + game + "Level" + level;
            SceneManager.LoadScene("Loading");
        }
        else
        {
            //Debug.Log("Game" + game + "Tutorial");
            //SceneManager.LoadScene("Game" + game + "Tutorial");
            Settings.sceneToLoad = "Game" + game + "Tutorial";
            SceneManager.LoadScene("Loading");
        }

        //maybe do parameters
        //SceneManager.LoadScene(string sceneName, LoadSceneParameters parameters);
    }

    /// <summary>
    /// Closes the application
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit!");
        if (activeJoycon != null) activeJoycon.Detach();
        Application.Quit();
    }

    /// <summary>
    /// Sets the wrist
    /// </summary>
    /// <param name="value">The id of the wrist selection</param>
    public static void ChangeWrist(int value)
    {
        if (value == 1) wrist = -1;
        else if (value == 2) wrist = -1;
        else wrist = 1;
        Debug.Log("Selected wrist: " + wrist);
    }

    /// <summary>
    /// Sets the number of the selected game
    /// </summary>
    /// <param name="gameNr">Number of the game selected</param>
    public void SetGame(int gameNr)
    {
        game = gameNr;
    }

    /// <summary>
    /// Sets the difficulty of the game
    /// Also starts the selected game
    /// </summary>
    /// <param name="diff">The difficulty</param>
    public void SetDifficulty(string diff)
    {
        difficulty = diff;
        StartGameDiff();
    }

    /// <summary>
    /// Sets the level of the game
    /// Also starts the selected game
    /// </summary>
    /// <param name="lvl">The level</param>
    public void SetLevel(int lvl)
    {
        level = lvl;
        StartGameLevel();
    }

    /// <summary>
    /// Sets the time via the slider
    /// </summary>
    public void SetGameDuration()
    {
        DurationSlider = Component.FindObjectsOfType<Slider>().ToList().Find(x => x.name == "DurationSlider");
        DurationText = Component.FindObjectsOfType<Text>().ToList().Find(x => x.name == "DurationText");

        timeToGame = DurationSlider.value * 10;
        int m = Mathf.FloorToInt(timeToGame / 60);
        int s = (int)timeToGame % 60;
        string t;
        t = m < 10 ? "0" + m + ":" : m + ":";
        t += s < 10 ? "0" + s : s.ToString();
        DurationText.text = t;

        Settings.gameDuration = timeToGame;
    }

    /// <summary>
    /// Opens the bluetooth menu of the computer
    /// </summary>
    public void OpenBluetoothConnection()
    {
        Process.Start("control", "bthprops.cpl");
    }

    /// <summary>
    /// Refreshes the Text for the connected JoyCon
    /// </summary>
    public void RefreshControllerText()
    {
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            conn.text = "Kein JoyCon gefunden";
        }
        else
        {
            activeJoycon = joycons[jc_ind];
            if (activeJoycon.isLeft) conn.text = "Linker JoyCon verbunden";
            else conn.text = "Rechter JoyCon verbunden";
        }
    }
}
