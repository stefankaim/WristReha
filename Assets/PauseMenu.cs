using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public Button buttonResume;
    public Button buttonMain;
    private Joycon activeJoycon;
    private int selected;
    private float cooldown = 0.25f;
    private float timeElapsed;

    public void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        timeElapsed += Time.unscaledDeltaTime;
        if (cooldown <= timeElapsed)
        {
            if (activeJoycon.GetButton(Joycon.Button.PLUS) || activeJoycon.GetButton(Joycon.Button.MINUS)) Resume();

            if (activeJoycon.GetButton(Joycon.Button.DPAD_LEFT) || activeJoycon.GetButton(Joycon.Button.DPAD_RIGHT))
            {
                timeElapsed = 0;
                if (selected == 0) selected = 1;
                else selected = 0;
            }

            if (selected == 0) buttonResume.Select();
            else buttonMain.Select();

            if (activeJoycon.GetButton(Joycon.Button.SHOULDER_1) || activeJoycon.GetButton(Joycon.Button.SHOULDER_2))
            {
                timeElapsed = 0;
                if (selected == 0) Resume();
                else MainMenu();
            }
        }
    }

    /// <summary>
    /// Opens the Pause Menu and pauses the game
    /// </summary>
    public void Pause(Joycon activeJoycon)
    {
        Time.timeScale = 0f;
        this.activeJoycon = activeJoycon;
        buttonResume.Select();
        selected = 0;
        this.gameObject.SetActive(true);
        timeElapsed = 0;
    }

    /// <summary>
    /// Closes the Pause Menu and resumes the game
    /// </summary>
    public void Resume()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Closes the current scene and opens the main menu
    /// </summary>
    public void MainMenu()
    {
        activeJoycon.Detach();
        Time.timeScale = 1f;
        //SceneManager.LoadScene(0);
        Settings.sceneToLoad = "Menu";
        SceneManager.LoadScene("Loading");
    }
}
