using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    public Text text;
    public int Game;

    public float vanishTime = 5f;
    public float timePassed;

    private void Awake()
    {
        if (Game == 1) text.text = "";
        else if (Game == 2) text.text = "B‰lle fliegen auf\ndas Tor";
        else if (Game == 3) text.text = "Pflanzen gieﬂen";
        timePassed = 0f;
    }

    /// <summary>
    /// Sets the tutorial text on the display
    /// </summary>
    /// <param name="text">Text that should be displayed</param>
    public void SetText(string text)
    {
        this.text.text = text;
        timePassed = 0f;
    }

    /// <summary>
    /// Deletes the tutorial text after 5 seconds
    /// </summary>
    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= vanishTime)
        {
            text.text = string.Empty;
        }
    }
}
