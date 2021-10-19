using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
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
    /// Starts the choosen game with choosen difficulty
    /// </summary>
    public static void StartGameDiff()
    {
        Debug.Log("Game" + game + difficulty);
        SceneManager.LoadScene("Game" + game + difficulty);

        //maybe do parameters
        //SceneManager.LoadScene(string sceneName, LoadSceneParameters parameters);
    }

    /// <summary>
    /// Starts the choosen game with choosen level
    /// </summary>
    public static void StartGameLevel()
    {
        if (level > 0)
        {
            Debug.Log("Game" + game + "Level" + level);
            SceneManager.LoadScene("Game" + game + "Level" + level);
        }
        else
        {
            Debug.Log("Game" + game + "Tutorial");
            SceneManager.LoadScene("Game" + game + "Tutorial");
        }

        //maybe do parameters
        //SceneManager.LoadScene(string sceneName, LoadSceneParameters parameters);
    }

    /// <summary>
    /// Closes the application
    /// </summary>
    public static void QuitGame()
    {
        Debug.Log("Quit!");
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
    public static void SetGame(int gameNr)
    {
        game = gameNr;
    }

    /// <summary>
    /// Sets the difficulty of the game
    /// Also starts the selected game
    /// </summary>
    /// <param name="diff">The difficulty</param>
    public static void SetDifficulty(string diff)
    {
        difficulty = diff;
        StartGameDiff();
    }

    /// <summary>
    /// Sets the level of the game
    /// Also starts the selected game
    /// </summary>
    /// <param name="lvl">The level</param>
    public static void SetLevel(int lvl)
    {
        level = lvl;
        StartGameLevel();
    }
}
