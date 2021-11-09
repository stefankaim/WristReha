using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WateredPlantsCounter : MonoBehaviour
{
    public Text text;
    public int plantsToWater, plantsWatered = 0;
    public AlienGameController gameController;

    private void Awake()
    {
        text.text = plantsWatered + " / " + plantsToWater;
    }

    /// <summary>
    /// When a new plant is done
    /// </summary>
    public void IncreaseWateredPlants()
    {
        if (gameController != null) gameController.PlantWatered();
        plantsWatered++;
        text.text = plantsWatered + " / " + plantsToWater;
    }

    private void Update()
    {
        if (plantsWatered >= plantsToWater)
        {
            gameController.SetGameDone();
        }
    }
}
