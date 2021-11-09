using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextForceIndicator : MonoBehaviour
{
    public Slider slider;
    public TimeCounter Timer;
    public BalanceGameController gameController;

    private void Awake()
    {
        slider.minValue = 0;
        slider.maxValue = 1;
        slider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.countdownOver && !gameController.gameOver && !gameController.gameDone)
        {
            slider.value -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Sets the new value for when the next force will be applied to the game ball
    /// </summary>
    /// <param name="value">Time for the next force</param>
    public void SetNextForceTime(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }
}
