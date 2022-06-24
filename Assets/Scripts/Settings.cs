using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    /// <summary>
    /// How long timed games should be played until finished
    /// </summary>
    public static float gameDuration = 500f;

    /// <summary>
    /// The parameter of the wrist, to know if the controls have to be flipped (* -1)
    /// As the majority of people use the right hand, the value is set to default 1 (right)
    /// </summary>
    /// [Obsolete] Joyconlib is used and takes care of left or right
    public static int wrist = 1;

    /// <summary>
    /// The next scene that should be loaded
    /// </summary>
    public static string sceneToLoad;

    /// <summary>
    /// When clicked on "Hauptmenü"
    /// true: The scene loads to the game choose menu
    /// false: The scene loads to start screen
    /// </summary>
    public static bool gameScreen = false;
}
