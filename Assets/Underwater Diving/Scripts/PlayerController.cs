using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 2.5f;

    private float speedTimer;
    public float increaseSpeed = 3;
    public float movementY;
    public float speedMulti = 25f;

    public bool rushing = false;

    public MouseInput mouseInput;
    private Rigidbody2D player;
    private Animator swim;
    public GameObject bubbles;

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
    }

    void Update()
    {
        speedTimer += Time.deltaTime;
        if (speedTimer >= increaseSpeed)
        {
            speedTimer = 0;
            moveSpeed += 0.1f;
        }
    }

    void FixedUpdate()
    {
        Vector2 move = new Vector2();
        move.x = moveSpeed;
        move.y = movementY * 5f;
        player.velocity = move * Time.fixedDeltaTime * speedMulti;

        swim.SetFloat("Speed", Mathf.Abs(player.velocity.x));
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
        Debug.Log("Dead");
    }
}
