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
		player = GetComponent<Rigidbody2D> ();
		swim = GetComponent<Animator> ();
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

		//resetBoostTime();
		//controllerManager();


		Vector2 move = new Vector2();
		move.x = moveSpeed;
		move.y = movementY * 5f;
		player.velocity = move * Time.fixedDeltaTime * speedMulti;

		swim.SetFloat ("Speed", Mathf.Abs(player.velocity.x));

	 
		
	}

	void MoveY(float deltaY)
	{
		if (deltaY >= 1 || deltaY <= -1)
		{
			Debug.Log("Y-movement: " + deltaY);
			movementY = deltaY;
		}
		else movementY = 0;
		/*
		if (Input.GetAxisRaw ("Horizontal") > 0f)
		{
			transform.localScale = new Vector3(1f,1f,1f);
			movePlayer ();
		}
		else if (Input.GetAxisRaw ("Horizontal") < 0f)
		{			
			transform.localScale = new Vector3(-1f,1f,1f);
			movePlayer ();
		}
		else if (Input.GetAxisRaw ("Vertical") > 0f)
		{
			player.velocity = new Vector3 (player.velocity.x, moveSpeed, 0f);
		}
		else if (Input.GetAxis ("Vertical") < 0f)
		{
			player.velocity = new Vector3 (player.velocity.x, -moveSpeed, 0f);
		}

		if(Input.GetButtonDown("Jump") && !rushing )
		{
			rushing = true;
			speedMod = 2;
			Instantiate(bubbles, gameObject.transform.position, gameObject.transform.rotation);
			movePlayer();
		}*/
	}

    private void OnEnable()
    {
		mouseInput.Enable();
    }

    private void OnDisable()
    {
		mouseInput.Disable();
    }

    void movePlayer()
	{
		/*
		if (transform.localScale.x == 1)
		{
			player.velocity = new Vector3 (moveSpeed + speedMod, player.velocity.y, 0f);	
		}
		else
		{
			player.velocity = new Vector3 (- (moveSpeed + speedMod), player.velocity.y, 0f);
		}*/
	}

	/*
	void resetBoostTime()
	{
		if (timeLeft <= 0)
		{
			timeLeft = 2f;
			rushing = false;
			speedMod = 0;
		}
		else if(rushing)
		{
			timeLeft -= Time.deltaTime;
		}	
	}*/

	public void hurt()
	{
		/*if(!rushing)
		{
			gameObject.GetComponent<Animator> ().Play ("PlayerHurt");		
		}*/
	}
}
