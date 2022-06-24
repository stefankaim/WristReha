using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class CatcherController : MonoBehaviour
{
    private float maxWidth;

    public FootballInput footballInput;
    private Rigidbody2D gloves;
    public Camera cam;
    public TimeCounter Timer;
    public MissionText MissionInfo;
    public PauseMenu PauseMenu;

    public GameObject JoyConManager;
    private List<Joycon> joycons;
    private Joycon activeJoycon;
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public int jc_ind = 0;

    private float pauseCooldownElapsed;
    private float pauseCooldown = 0.25f;

    void Awake()
    {
        #region MouseInput
        //footballInput = new FootballInput();
        #endregion
    }

    // Use this for initialization
    void Start()
    {
        gloves = GetComponent<Rigidbody2D>();
        if (!cam)
        {
            cam = Camera.main;
        }
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);

        Collider2D collider = gloves.GetComponent<Collider2D>();
        Bounds totalBBox = collider.bounds;
        float catcherwidth = totalBBox.extents.x;

        maxWidth = targetWidth.x - catcherwidth;

        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            Debug.LogError("Could not find connected Joy-Con!");
        }
        else
        {
            activeJoycon = joycons[jc_ind];
            //activeJoycon.Begin();
            activeJoycon.Recenter();
        }
    }

    private void Update()
    {
        pauseCooldownElapsed += Time.deltaTime;
        if (pauseCooldown <= pauseCooldownElapsed)
        {
            if (activeJoycon.GetButton(Joycon.Button.PLUS) || activeJoycon.GetButton(Joycon.Button.MINUS))
            {
                PauseMenu.Pause(activeJoycon);
                pauseCooldownElapsed = 0;
            }
        }
    }

    // Update is called once per physics timestep
    void FixedUpdate()
    {
        if (Timer.countdownOver && !MissionInfo.gameOver && !MissionInfo.gameDone)
        {
            #region MouseInput
            // Inactive!
            #region Move using the position of the mouse
            //Vector3 rawPosition = cam.ScreenToWorldPoint(new Vector3(footballInput.Gloves.Position.ReadValue<float>(), this.transform.position.y));
            //Vector3 targetPosition = new Vector3(rawPosition.x, this.transform.position.y, 0.0f);
            #endregion

            // Active!
            #region Move using the delta of the mouse
            //float movementX = footballInput.Gloves.Move.ReadValue<float>() * Time.fixedDeltaTime;
            //Vector3 targetPosition = new Vector3(gloves.position.x + movementX, this.transform.position.y, 0.0f);
            #endregion
            #endregion

            gyro = activeJoycon.GetGyro();
            Vector3 targetPosition = new Vector3(gloves.position.x + gyro.z, this.transform.position.y, 0.0f);

            float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidth, maxWidth);
            targetPosition = new Vector3(targetWidth, targetPosition.y, targetPosition.z);
            transform.position = targetPosition;
        }
    }

    /// <summary>
    /// Stops the JoyCon actions
    /// </summary>
    public void StopControl()
    {
        activeJoycon.Detach();
    }

    /// <summary>
    /// Rumble the controller
    /// Low: 160Hz, High: 320Hz
    /// Amplitude: percent * 0.6
    /// </summary>
    /// <param name="percent">Percent of the force on the ball</param>
    public void RumbleJoyCon(float percent)
    {
        percent = Mathf.Clamp(percent, 0, 1);
        activeJoycon.SetRumble(160, 320, percent * 0.6f, (int)(percent * 200));
    }

    #region MouseInput
    /// <summary>
    /// Enables the Input System
    /// </summary>
    private void OnEnable()
    {
        //footballInput.Enable();
    }

    /// <summary>
    /// Disables the Input System
    /// </summary>
    private void OnDisable()
    {
        //footballInput.Disable();
    }
    #endregion
}