using UnityEngine;
using System.Collections;



public class Tutorial : MonoBehaviour
{
    //Main camera
    public Camera cam;

    //Array for different balls
    public GameObject[] objectGame;
    public GameObject[] objectGamePool;

    //Game Over / Done
    public bool gameOver = false; //Game Over got >= 5 goals
    public bool gameDone = false; //Game Done when >= 6 balls are catched

    //The time range when balls should spawn
    public Vector2 timeRange = new Vector2(1.0f, 2.0f);

    //For the max with of the game
    private float maxWidth;

    private bool showed2 = false, showed3 = false, showed4 = false;

    public GoalCounter Health;
    public CatchCounter Catched;
    public TimeCounter Time;
    public TutorialText TuText;
    public MissionText MissionInfo;
    public CatcherController catcherController;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start()
    {
        if (!cam) cam = Camera.main;

        InitObjectArray();

        if (objectGame.Length > 0) StartCoroutine(Spawn());
    }

    /// <summary>
    /// Inits the object array
    /// </summary>
    private void InitObjectArray()
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
        if (Catched.catched >= 6) GameDone();

        if (Catched.catched > 1 && !showed3)
        {
            TuText.SetText("Das Spiel endet\nnach 5 erhaltenen Toren");
            showed3 = true;
        }

        if (Catched.catched >= 4 && !showed4)
        {
            TuText.SetText("Die Anleitung endet\nnach 6 gehaltenen Bällen");
            showed4 = true;
        }
    }

    /// <summary>
    /// Sets game over for the tutorial
    /// </summary>
    private void GameOver()
    {
        //TuText.SetText("Schade!\nMehr als 5 Tore bekommen");
        if (TuText != null) TuText.SetText("");
        if (catcherController != null) catcherController.StopControl();
        gameOver = true;
        Time.StopTimer();
        MissionInfo.SetGameOver();
    }

    /// <summary>
    /// When the game was successful
    /// </summary>
    private void GameDone()
    {
        //TuText.SetText("Super!\n6 Bälle gehalten");
        if (TuText != null) TuText.SetText("");
        if (catcherController != null) catcherController.StopControl();
        gameDone = true;
        Time.StopTimer();
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
        Time.StartCountdown(6);
        yield return new WaitForSeconds(6.0f);
        Time.StartTimer();

        while (!gameOver && !gameDone)
        {
            if (!showed2)
            {
                TuText.SetText("Die Bälle durch fangen\noder abprallen abwehren");
                showed2 = true;
            }

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
    /// Gets or sets the volume
    /// </summary>
    /// <value>The volume</value>
    private float volume
    {
        get { return 0; }
        set { value = 0; }
        //get{return AudioListener.volume;}
        //set{AudioListener.volume=value;}
    }
}