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

	// Use this for initialization
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

	void MoveY(float deltaY)
	{
		if (deltaY >= 1 || deltaY <= -1)
		{
			//Debug.Log("Y-movement: " + deltaY);
			movementY = deltaY;
		}
		else movementY = 0;
	}

	private void OnEnable()
	{
		mouseInput.Enable();
	}

	private void OnDisable()
	{
		mouseInput.Disable();
	}

	public void hurt()
	{
		gameObject.GetComponent<Animator>().Play("PlayerHurt");		
	}

	// Disable the behaviour when it becomes invisible...
	void OnBecameInvisible()
	{
		Debug.Log("Dead");
	}
}
