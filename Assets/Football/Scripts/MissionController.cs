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
    public float timeElapsed = 0f;

    //The time range when balls should spawn
    public Vector2 timeRange = new Vector2(1.0f, 2.0f);

    //For the max with of the game
    private float maxWidth;

    public GoalCounter Health;
    public CatchCounter Catched;
    public TimeCounter Timer;
    public MissionText MissionInfo;
    public CatcherController catcherController;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    private void Start()
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
    private void Update()
    {
        if (Timer.countdownOver)
        {
            if (Health.goals >= 5) GameOver();
            timeElapsed += Time.deltaTime;
            if (Settings.gameDuration + 1 <= timeElapsed) GameDone();
        }
    }

    /// <summary>
    /// Sets game over for the tutorial
    /// </summary>
    private void GameOver()
    {
        if (catcherController != null) catcherController.StopControl();
        gameOver = true;
        Timer.StopTimer();
        MissionInfo.SetGameOver();
    }

    /// <summary>
    /// When the game was successful
    /// </summary>
    private void GameDone()
    {
        if (catcherController != null) catcherController.StopControl();
        gameDone = true;
        Timer.StopTimer();
        MissionInfo.SetGameDone();
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
        Timer.StartCountdown(3);
        yield return new WaitForSeconds(3.0f);
        Timer.StartTimer();

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