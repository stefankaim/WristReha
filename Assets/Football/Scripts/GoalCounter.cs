using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalCounter : MonoBehaviour
{
    public Image Lost1, Lost2, Lost3, Lost4, Lost5;
    public int goals = 0;

    private void Awake()
    {
        Lost1.enabled = false;
        Lost2.enabled = false;
        Lost3.enabled = false;
        Lost4.enabled = false;
        Lost5.enabled = false;
    }

    /// <summary>
    /// Sets the UI for the heart indicator
    /// </summary>
    /// <returns>True if the game is over</returns>
    public void GotGoal()
    {
        goals++;
        if (goals == 1) Lost1.enabled = true;
        else if (goals == 2) Lost2.enabled = true;
        else if (goals == 3) Lost3.enabled = true;
        else if (goals == 4) Lost4.enabled = true;
        else Lost5.enabled = true;
    }
}
