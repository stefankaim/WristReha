using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainingWater : MonoBehaviour
{
    public Slider slider;
    public TimeCounter Timer;
    public float timeForLevel = 30f;
    public GameObject waterParticles;
    public AlienGameController gameController;

    private void Awake()
    {
        waterParticles.SetActive(false);
        slider.minValue = 0;
        slider.maxValue = timeForLevel;
        slider.value = timeForLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.countdownOver)
        {
            if (slider.value == slider.maxValue)
            {
                waterParticles.SetActive(true);
            }
            slider.value -= Time.deltaTime;
            if (slider.value <= 0)
            {
                gameController.SetGameOver();
            }
        }
    }
}
