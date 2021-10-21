using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 2.5f;
    public float speedIncreaser = 0.2f;

    private float speedTimer;
    public float increaseSpeed = 3f;
    public float movementY;
    public float speedMulti = 25f;
    public float speedDecreaser = 0f;
    private float speedDecreaseTimer;
    private float speedDecreaseTime;

    public bool rushing = false;
    public bool gameOver = false;
    public bool gameDone = false;

    public bool tutorial = false;
    private float tutorialTimer;
    private float nextTutorial = 5f;
    private bool showed1 = false, showed2 = false, showed3 = false, showed4 = false, showed5 = false, showed6 = false;

    public MouseInput mouseInput;
    private Rigidbody2D player;
    private Animator swim;
    public GameObject bubbles;
    public TimeCounter Timer;
    public TutorialText Tutorial;
    public MoveRight CamMover;
    public InfoText info;

    void Awake()
    {
        mouseInput = new MouseInput();
        mouseInput.Player.Move.performed += ctx => MoveY(ctx.ReadValue<float>());
        mouseInput.Player.Move.canceled += ctx => MoveY(0);
    }

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        swim = GetComponent<Animator>();
        speedTimer = 0;
        Timer.StartCountdown(3);
    }

    void Update()
    {
        if (Timer.countdownOver && !gameOver && !gameDone)
        {
            speedTimer += Time.deltaTime;
            if (speedTimer >= increaseSpeed)
            {
                speedTimer = 0;
                moveSpeed += speedIncreaser;
            }

            if (speedDecreaser > 0)
            {
                speedDecreaseTimer += Time.deltaTime;
                if (speedDecreaseTimer >= speedDecreaseTime)
                {
                    speedDecreaser = 0f;
                }
            }

            if (tutorial)
            {
                tutorialTimer += Time.deltaTime;
                if (tutorialTimer >= nextTutorial)
                {
                    tutorialTimer = 0f;
                    if (!showed1)
                    {
                        Tutorial.SetText("Den Taucher auf\nund ab bewegen\nHervorstehendes Gelände meiden");
                        showed1 = true;
                    }
                    else if (!showed2)
                    {
                        Tutorial.SetText("Pflanzen verletzen\nden Taucher nicht");
                        nextTutorial = 4f;
                        showed2 = true;
                    }
                    else if (!showed3)
                    {
                        Tutorial.SetText("Der Taucher wird\nalle 3 Sekunden schneller");
                        nextTutorial = 5f;
                        showed3 = true;
                    }
                    else if (!showed4)
                    {
                        Tutorial.SetText("Fische verringern\ndie Geschwindigkeit\ndes Tauchers kurzzeitig");
                        nextTutorial = 10f;
                        showed4 = true;
                    }
                    else if (!showed5)
                    {
                        Tutorial.SetText("Minen verringern\ndie Geschwindigkeit\nauch");
                        nextTutorial = 8f;
                        showed5 = true;
                    }
                    else if (!showed6)
                    {
                        Tutorial.SetText("Luftblasen zeigen\ndas Ende des Levels");
                        showed6 = true;
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (Timer.countdownOver && !gameOver && !gameDone)
        {
            Vector2 move = new Vector2();
            move.x = moveSpeed - speedDecreaser;
            move.y = movementY * 5f;
            player.velocity = move * Time.fixedDeltaTime * speedMulti;

            swim.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        }
    }

    /// <summary>
    /// Moves the diver up and down
    /// </summary>
    /// <param name="deltaY">The delta to the last position of the Input</param>
    void MoveY(float deltaY)
    {
        if (deltaY >= 1 || deltaY <= -1)
        {
            //Debug.Log("Y-movement: " + deltaY);
            movementY = deltaY;
        }
        else movementY = 0;
    }

    /// <summary>
    /// Enables the Input System
    /// </summary>
    private void OnEnable()
    {
        mouseInput.Enable();
    }

    /// <summary>
    /// Disables the Input System
    /// </summary>
    private void OnDisable()
    {
        mouseInput.Disable();
    }

    /// <summary>
    /// When the diver touches a mine or a fish
    /// </summary>
    public void Hurt()
    {
        gameObject.GetComponent<Animator>().Play("PlayerHurt");
    }

    /// <summary>
    /// When the diver gets invisible the level ends
    /// </summary>
    void OnBecameInvisible()
    {
        if (!gameDone)
        {
            if (tutorial) Tutorial.SetText("");
            Timer.StopTimer();
            CamMover.Stop();
            gameOver = true;
            info.SetGameOver();
        }
    }

    /// <summary>
    /// When the player collides with an object
    /// </summary>
    /// <param name="collision">The colliding object</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("EndOfLevel") || collision.gameObject.name.Contains("SkipTutorial"))
        {
            if (tutorial) Tutorial.SetText("");
            Timer.StopTimer();
            CamMover.Stop();
            gameDone = true;
            info.SetLevelDone();
        }
    }

    /// <summary>
    /// When collided with a trigger game object
    /// </summary>
    /// <param name="collision">The other colliding object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name);
        if (collision.gameObject.name.Contains("Fish"))
        {
            if (collision.gameObject.name.Contains("Big"))
            {
                speedDecreaser = 0.5f;
            }
            else if (collision.gameObject.name.Contains("Mid"))
            {
                speedDecreaser = 0.4f;
            }
            else
            {
                speedDecreaser = 0.3f;
            }
            speedDecreaseTime = 1f;
            speedDecreaseTimer = 0f;
        }
        else if (collision.gameObject.name.Contains("Mine"))
        {
            if (collision.gameObject.name.Contains("Big"))
            {
                speedDecreaser = 0.8f;
            }
            else if (collision.gameObject.name.Contains("Small"))
            {
                speedDecreaser = 0.6f;
            }
            else
            {
                speedDecreaser = 0.7f;
            }
            speedDecreaseTime = 2f;
            speedDecreaseTimer = 0f;
        }
        Debug.Log("speedDecreaser: " + speedDecreaser);
    }
}
