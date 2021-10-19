using UnityEngine;
using System.Collections;



public class MissionController : MonoBehaviour
{
    //Main camera
    public Camera cam;

    //Array for the different balls
    public GameObject[] objectGame;
    public GameObject[] objectGamePool;

    //GAME DURATION
    public float gameDuration = 30.0f;

    //Game Over / Done
    public bool gameOver = false; //Game Over got >= 5 goals
    public bool gameDone = false; //Game Done when >= 6 balls are catched

    //The time range when balls should spawn
    public Vector2 timeRange = new Vector2(1.0f, 2.0f);

    //For the max with of the game
    private float maxWidth;

    public GoalCounter Health;
    public CatchCounter Catched;
    public TimeCounter Time;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start()
    {
        if (!cam) cam = Camera.main;

        initObjectArray();

        if (objectGame.Length > 0) StartCoroutine(Spawn());
    }

    /// <summary>
    /// Inits the object array
    /// </summary>
    private void initObjectArray()
    {
        if (objectGame != null)
        {
            objectGamePool = new GameObject[objectGame.Length];
            for (int i = 0; i < objectGame.Length; i++)
            {
                objectGamePool[i] = Instantiate(objectGame[i]) as GameObject;
                objectGamePool[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// Update this instance
    /// </summary>
    void Update()
    {
        if (Health.goals >= 5) GameOver();
#warning Zielzeiteingabe, wenn erreicht dann Level bestanden!
    }

    /// <summary>
    /// Sets game over for the tutorial
    /// </summary>
    void GameOver()
    {
        gameOver = true;
        Time.StopTimer();
    }

    /// <summary>
    /// When the game was successful
    /// </summary>
    private void GameDone()
    {
        gameDone = true;
        Time.StopTimer();
    }

    /// <summary>
    /// Activates the ball
    /// </summary>
    public GameObject activateObject(int index = 0)
    {

        if (!objectGamePool[index].activeSelf) return objectGamePool[index];

        for (int i = 0; i < objectGamePool.Length; i++)
        {
            if (!objectGamePool[i].activeSelf) return objectGamePool[i];
        }
        return null;
    }

    /// <summary>
    /// Spawns the balls
    /// </summary>
    IEnumerator Spawn()
    {
        Time.StartCountdown(3);
        yield return new WaitForSeconds(3.0f);
        Time.StartTimer();

        while (!gameOver && !gameDone)
        {
            GameObject obj;
            if (objectGamePool.Length == 1) obj = this.activateObject(0);
            else obj = this.activateObject(Random.Range(0, this.objectGamePool.Length));


            if (obj != null)
            {
                Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
                Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
                float objectWidth = obj.GetComponent<Renderer>().bounds.extents.x;
                maxWidth = targetWidth.x - objectWidth;
                Vector3 spawnPosition = new Vector3(Random.Range(-maxWidth, maxWidth), transform.position.y, 0.0f);
                Quaternion spawnRotation = Quaternion.identity;
                obj.transform.position = spawnPosition;
                obj.transform.rotation = spawnRotation;
                obj.SetActive(true);
            }

            yield return new WaitForSeconds(Random.Range(timeRange.x, timeRange.y));
        }
    }

    /// <summary>
    /// Gets or sets the volume.
    /// </summary>
    /// <value>The volume.</value>
    private float volume
    {
        get { return 0; }
        set { value = 0; }
        //get{return AudioListener.volume;}
        //set{AudioListener.volume=value;}
    }
}