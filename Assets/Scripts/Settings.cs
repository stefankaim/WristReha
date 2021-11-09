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
    public static int wrist = 1;
}
