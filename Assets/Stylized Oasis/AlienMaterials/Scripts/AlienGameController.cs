using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGameController : MonoBehaviour
{
    public bool gameOver = false;
    public bool gameDone = false;
    public bool isTutorial = false;

    private float timeElapsed;
    private float timeUntilNextTutorial = 5f;
    private int tutorial = 0;
    private string[] tutorialTexts = { "Blaue Pflanzen gieﬂen\nbis sie farbig werden", "Controller in alle Richtungen\nbewegen und auch halten", "Das Spiel ist vorbei wenn\n alle Pflanzen gegossen sind\noder das Wasser aus ist" };

    public TimeCounter Timer;
    public InfoText InfoText;
    public WateredPlantsCounter WateredPlants;
    public TutorialText TutorialText;
    public RemainingWater RemainingWater;
    public GameObject waterParticles;

    private ParticleSystem water;

    private void Awake()
    {
        water = waterParticles.GetComponent<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Timer.StartCountdown(3);
        //Timer.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.countdownOver)
        {
            if (isTutorial)
            {
                timeElapsed += Time.deltaTime;
                if (timeElapsed >= timeUntilNextTutorial)
                {
                    if (tutorialTexts.Length > tutorial)
                    {
                        timeElapsed = 0;
                        TutorialText.SetText(tutorialTexts[tutorial]);
                        tutorial++;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Sets the game over
    /// </summary>
    public void SetGameOver()
    {
        Timer.StopTimer();
        gameOver = true;
        water.Stop();
        InfoText.SetGameOver();

    }

    /// <summary>
    /// Sets the game done
    /// </summary>
    public void SetGameDone()
    {
        Timer.StopTimer();
        gameDone = true;
        water.Stop();
        InfoText.SetLevelDone();
    }
}
