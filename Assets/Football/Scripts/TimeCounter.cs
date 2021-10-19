using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public Text timerText;
    private bool started = false;
    private float time = 0;
    private float second = 0;

    public Text countdownText;
    private bool startedCountdown = false;
    private float countdown;
    private float secondCountdown = 0;

    private void Awake()
    {
        countdownText.enabled = false;
    }

    private void Update()
    {
        if (started)
        {
            time += Time.deltaTime;
            second += Time.deltaTime;
            if (second >= 1)
            {
                UpdateUI();
                second = 0;
            }
        }

        if (startedCountdown)
        {
            countdown -= Time.deltaTime;
            secondCountdown += Time.deltaTime;
            if (secondCountdown >= 1)
            {
                UpdateCountdownUI();
                secondCountdown = 0;
            }
            if (countdown <= 0)
            {
#warning Activate InputController just right here, not before (delta!)
                startedCountdown = false;
                countdownText.enabled = false;
            }
        }
    }

    /// <summary>
    /// Updates the UI for the timer
    /// </summary>
    private void UpdateUI()
    {
        int s = (int)time % 60;
        int m = Mathf.FloorToInt(time / 60);
        string t;
        if (m < 10) t = "0" + m;
        else t = m.ToString();
        t += ":";
        if (s < 10) t += "0" + s;
        else t += s.ToString();
        timerText.text = t;
    }

    /// <summary>
    /// Updates the UI for the countdown
    /// </summary>
    private void UpdateCountdownUI()
    {
        countdownText.text = "Start in " + Mathf.FloorToInt(countdown);
    }

    /// <summary>
    /// Starts the timer
    /// </summary>
    public void StartTimer()
    {
        started = true;
    }

    /// <summary>
    /// Stops the timer
    /// </summary>
    public void StopTimer()
    {
        started = false;
    }

    /// <summary>
    /// Starts the countdown
    /// </summary>
    /// <param name="time">The time for the countdown</param>
    public void StartCountdown(int time)
    {
        countdown = time;
        countdownText.text = "Start in " + time;
        countdownText.enabled = true;
        startedCountdown = true;
    }
}
