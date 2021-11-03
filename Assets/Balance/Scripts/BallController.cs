using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public BalanceGameController gameController;

    /// <summary>
    /// When the ball gets invisible the level ends
    /// </summary>
    void OnBecameInvisible()
    {
        if (!gameController.gameDone)
        {
            gameController.SetGameOver();
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
