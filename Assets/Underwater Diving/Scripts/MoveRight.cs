using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float increaseSpeed = 3;
    public float speedMulti = 25f;
    private bool stop = false;

    private Rigidbody2D mover;
    public TimeCounter Timer;
    public PlayerController playerController;

    private float speedTimer;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<Rigidbody2D>();
        speedTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.countdownOver && !stop)
        {
            speedTimer += Time.deltaTime;
            if (speedTimer >= increaseSpeed)
            {
                speedTimer = 0;
                moveSpeed += playerController.speedIncreaser;
            }
        }
    }

    void FixedUpdate()
    {
        if (Timer.countdownOver && !stop)
        {
            Vector2 move = new Vector2();
            move.x = moveSpeed;
            mover.velocity = move * Time.fixedDeltaTime * speedMulti;
        }
    }

    /// <summary>
    /// Stops the camera movement
    /// </summary>
    public void Stop()
    {
        stop = true;
    }
}
