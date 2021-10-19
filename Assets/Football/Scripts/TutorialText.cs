using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    public Text text;

    private void Awake()
    {
        text.text = "Bälle fliegen auf\ndas Tor";
    }

    /// <summary>
    /// Sets the tutorial text on the display
    /// </summary>
    /// <param name="text">Text that should be displayed</param>
    public void SetText(string text)
    {
        this.text.text = text;
    }
}
