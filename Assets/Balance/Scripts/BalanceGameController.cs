using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceGameController : MonoBehaviour
{
    /// <summary>
    /// The ball that gets balanced
    /// </summary>
    public GameObject GameBall;
    private Rigidbody ball;

    public NextForceIndicator nextForceIndicator;
    public TimeCounter Timer;
    public InfoText InfoText;
    public TutorialText TutorialText;

    /// <summary>
    /// Min and max time when the next wind force will be applied to the game ball (random between)
    /// </summary>
    public Vector2 timeForce;
    /// <summary>
    /// The force on each coordinate that is added to the game ball as an impulse, depends on the balls mass!
    /// </summary>
    public Vector2 randomForceX, randomForceY, randomForceZ;
    public float nextForce = 1f;
    public float forceTimeElapsed;

    public bool gameOver, gameDone;
    public bool isTutorial = false;

    private float timeElapsed;
    private float timeUntilNextTutorial = 5f;
    private int tutorial = 0;
    private string[] tutorialTexts = { "Controller auf/ab und\n rechts/links bewegen", "Den Ball nicht fallen lassen", "Der Balken rechts zeigt wann\n Kr�fte auf den Ball wirken", "Das Spiel ist geschafft, wenn\nder Ball die eingestellte Zeit\nnicht am Boden fliegt" };


    private void Awake()
    {
        ball = GameBall.GetComponent<Rigidbody>();
        nextForce = Random.Range(timeForce.x, timeForce.y);
        nextForceIndicator.SetNextForceTime(nextForce);
    }

    private void Start()
    {
        Timer.StartCountdown(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.countdownOver && !gameOver && !gameDone)
        {
            forceTimeElapsed += Time.deltaTime;
            if (forceTimeElapsed >= nextForce)
            {
                forceTimeElapsed = 0;
                nextForce = Random.Range(timeForce.x, timeForce.y);
                nextForceIndicator.SetNextForceTime(nextForce);
                Vector3 randomForce = new Vector3(Random.Range(randomForceX.x, randomForceX.y), Random.Range(randomForceY.x, randomForceY.y), Random.Range(randomForceZ.x, randomForceZ.y));
                ball.AddRelativeForce(randomForce, ForceMode.Impulse);
            }

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
        InfoText.SetGameOver();

    }

    /// <summary>
    /// Sets the game done
    /// </summary>
    public void SetGameDone()
    {
        Timer.StopTimer();
        gameDone = true;
        InfoText.SetLevelDone();
    }
}