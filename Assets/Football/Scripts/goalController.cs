using UnityEngine;
using System.Collections;



public class goalController : MonoBehaviour
{
    public GoalCounter Health;
    public CatcherController catcher;

    /// <summary>
    /// Triggered when a goal is scored
    /// </summary>
    /// <param name="other">Collided object</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            if (other.name.Contains("ball"))
            {
                Health.GotGoal();
                GetComponent<AudioSource>().Play();
                if (Health.goals < Health.maxGoals) catcher.RumbleJoyCon(1f);
            }
            other.gameObject.SetActive(false);
        }
    }
}