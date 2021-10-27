using UnityEngine;
using System.Collections;

public class GloveController : MonoBehaviour
{

    //COLLECTED OBJECTS
    public int collectedObjects = 0;
    public int totalCollectedObjects = 0;
    //COLLECTED COINS
    public int coins = 0;

    private Transform _gloves;
    public CatchCounter Catcher;

    /// <summary>
    /// Start this instance.
    /// </summary>
    void OnEnable()
    {
        _gloves = transform.Find("glove");
    }

    /// <summary>
    /// Raises the trigger enter2d event.
    /// </summary>
    /// <param name="other">The other object that collided</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("ball"))
        {
            GetComponent<AudioSource>().Play();
            OnObjectCollision();
            this.collectedObjects++;
            this.totalCollectedObjects++;
            Catcher.BallCatched();

        }
        else if (other.name.Contains("coin"))
        {
            GetComponent<AudioSource>().Play();
            this.coins++;
        }
    }
    /// <summary>
    /// If the gloves collide with (catch) a ball
    /// </summary>
    private void OnObjectCollision()
    {
        if (_gloves != null)
        {
            if (_gloves.GetComponent<Animator>() != null)
            {
                _gloves.GetComponent<Animator>().SetTrigger("stopped");
            }
        }
    }
}