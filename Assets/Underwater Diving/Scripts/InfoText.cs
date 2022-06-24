using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfoText : MonoBehaviour
{
    public Text text;
    public Button buttonLevel;
    public Button buttonMenu;
    private Scene currentScene;
    private bool nextSceneExists = false;
    private bool gameOver = false;
    private string nextSceneName;
    private string currentSceneName;


    private void Awake()
    {
        buttonLevel.gameObject.SetActive(false);
        buttonMenu.gameObject.SetActive(false);
        text.enabled = false;
        currentScene = SceneManager.GetActiveScene();

        string currentName = currentScene.name;
        currentSceneName = currentName;
        if (currentName.ToLowerInvariant().Contains("tutorial"))
        {
            nextSceneName = currentName.Substring(0, 5) + "Level1";
        }
        else
        {
            int nr;
            if (!int.TryParse(currentName.Substring(currentName.Length - 2), out nr))
            {
                int.TryParse(currentName.Substring(currentName.Length - 1), out nr);
                nr++;
                nextSceneName = currentName.Substring(0, currentName.Length - 1) + nr;
            }
            else
            {
                nr++;
                nextSceneName = currentName.Substring(0, currentName.Length - 2) + nr;
            }

        }

        List<string> scenesInBuild = new List<string>();
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            int lastSlash = scenePath.LastIndexOf("/");
            scenesInBuild.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
        }

        if (scenesInBuild.Contains(nextSceneName)) nextSceneExists = true;
        else nextSceneExists = false;
    }

    /// <summary>
    /// Sets the Game Over text
    /// </summary>
    public void SetGameOver()
    {
        if (text != null)
        {
            text.color = Color.red;
            text.text = "Game Over!";
            text.enabled = true;
            gameOver = true;
        }
        ActivateMenuButton();
        ActivateReplayButton();
    }

    /// <summary>
    /// Sets the level finished text
    /// </summary>
    public void SetLevelDone()
    {
        if (text != null)
        {
            text.color = Color.green;
            text.text = "Level geschafft!";
            text.enabled = true;
        }
        ActivateNextLevelButton();
        ActivateMenuButton();
    }

    /// <summary>
    /// Activates a button to replay the level
    /// </summary>
    private void ActivateReplayButton()
    {
        if (buttonLevel != null)
        {
            buttonLevel.GetComponentInChildren<Text>().text = "Neu starten";
            buttonLevel.enabled = true;
            buttonLevel.gameObject.SetActive(true);
            buttonLevel.Select();
        }
    }

    /// <summary>
    /// Activates the button for the next level
    /// </summary>
    private void ActivateNextLevelButton()
    {
        if (nextSceneExists && buttonLevel != null)
        {
            buttonLevel.GetComponentInChildren<Text>().text = "N�chstes Level";
            buttonLevel.enabled = true;
            buttonLevel.gameObject.SetActive(true);
            buttonLevel.Select();
        }
    }

    /// <summary>
    /// Activates the button for the main menu
    /// </summary>
    private void ActivateMenuButton()
    {
        if (buttonMenu != null)
        {
            buttonMenu.enabled = true;
            buttonMenu.gameObject.SetActive(true);
            if (!nextSceneExists) buttonMenu.Select();
        }
    }

    /// <summary>
    /// When the level button is clicked load either next level or main menu
    /// </summary>
    public void LevelButtonClicked()
    {
        if (gameOver) Settings.sceneToLoad = currentSceneName;
        else if (nextSceneExists) Settings.sceneToLoad = nextSceneName;
        else Settings.sceneToLoad = "Menu";
        SceneManager.LoadScene("Loading");
    }

    /// <summary>
    /// When the menu button is clicked load the main menu
    /// </summary>
    public void MenuButtonClicked()
    {
        Settings.sceneToLoad = "Menu";
        SceneManager.LoadScene("Loading");
    }
}
