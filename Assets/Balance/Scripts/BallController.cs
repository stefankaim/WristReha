using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public BalanceGameController gameController;
    public TimeCounter Timer;

    private void FixedUpdate()
    {
        if (Timer.countdownOver)
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    /// <summary>
    /// If the ball touches the ground or water, the game is also over
    /// </summary>
    /// <param name="collision">The collided object</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Water") || collision.gameObject.name.Equals("Ground"))
        {
            gameController.SetGameOver();
        }
    }
}
