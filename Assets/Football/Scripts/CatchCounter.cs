using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchCounter : MonoBehaviour
{
    public Text text;
    public int catched = 0;

    /// <summary>
    /// Increases the counter of catched balls
    /// </summary>
    public void BallCatched()
    {
        catched++;
        text.text = catched.ToString();
    }
}
