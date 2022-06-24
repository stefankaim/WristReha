using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private string nextSceneName;
    private float loadingTime = 8f;
    private float loading = 0f;

    /// <summary>
    /// Gets the next scene to load from the settings
    /// </summary>
    private void Awake()
    {
        nextSceneName = Settings.sceneToLoad;
    }

    // Update is called once per frame
    void Update()
    {
        loading += Time.deltaTime;
        if (loading >= loadingTime)
        {
            LoadScene();
        }
    }

    /// <summary>
    /// Loads the new scene after loading time has passed
    /// The loading time is required to disconnect from the joycon and allow the next scene to connect to the joycon
    /// </summary>
    private void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
