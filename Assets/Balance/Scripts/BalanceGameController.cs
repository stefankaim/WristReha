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
    public SpoonController spoon;

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
    private string[] tutorialTexts = { "Controller auf/ab und\n rechts/links bewegen", "Den Ball nicht fallen lassen", "Der Balken rechts zeigt wann\n Kräfte auf den Ball wirken", "Das Spiel ist geschafft, wenn\nder Ball die eingestellte Zeit\nnicht am Boden fliegt" };


    private void Awake()
    {
        ball = GameBall.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Timer.StartCountdown(3);
        nextForce = Random.Range(timeForce.x, timeForce.y);
        nextForceIndicator.SetNextForceTime(nextForce);
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
                Vector3 ranDir = new Vector3();
                if (Random.value <= 0.5f) ranDir.x = -1f;
                else ranDir.x = 1f;
                if (Random.value <= 0.5f) ranDir.y = -1f;
                else ranDir.y = 1f;
                if (Random.value <= 0.5f) ranDir.z = -1f;
                else ranDir.z = 1f;
                float forceX = Random.Range(randomForceX.x, randomForceX.y);
                float forceY = Random.Range(randomForceY.x, randomForceY.y);
                float forceZ = Random.Range(randomForceZ.x, randomForceZ.y);
                Vector3 randomForce = new Vector3(ranDir.x * forceX, ranDir.y * forceY, ranDir.z * forceZ);
                ball.AddRelativeForce(randomForce, ForceMode.Impulse);

                Vector3 p = new Vector3(forceX / randomForceX.y, forceY / randomForceY.y, forceZ / randomForceZ.y);
                float percent = (p.x + p.y + p.z) / 3;
                spoon.RumbleJoyCon(percent);
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
        spoon.StopPolling();
        Timer.StopTimer();
        gameOver = true;
        InfoText.SetGameOver();
    }

    /// <summary>
    /// Sets the game done
    /// </summary>
    public void SetGameDone()
    {
        spoon.StopPolling();
        Timer.StopTimer();
        gameDone = true;
        InfoText.SetLevelDone();
    }
}
