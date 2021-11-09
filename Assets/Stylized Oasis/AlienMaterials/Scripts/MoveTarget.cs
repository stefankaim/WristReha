using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public TimeCounter Timer;

    private SprinkleMover Mover;
    private Vector3 direction;
    private Camera camera;
    private Vector3 moveDirection = Vector3.zero;
    public AlienGameController gameController;

    private List<Joycon> joycons;
    private Joycon activeJoycon;
    public float speed = 3f;
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public int jc_ind = 0;
    public Quaternion orientation;


    private void Awake()
    {
        camera = Camera.main;
        #region MouseInput
        //Mover = new SprinkleMover();
        //Mover.Move.deltaX.performed += ctx => MoveX(ctx.ReadValue<float>());
        //Mover.Move.deltaX.canceled += ctx => MoveX(0);
        //Mover.Move.deltaY.performed += ctx => MoveY(ctx.ReadValue<float>());
        //Mover.Move.deltaY.canceled += ctx => MoveY(0);
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
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
        }
        //transform.Rotate(Mover.Move.deltaY.ReadValue<float>() * RotationSpeed * Time.deltaTime, Mover.Move.deltaX.ReadValue<float>() * RotationSpeed * Time.deltaTime, 0f);//, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.countdownOver && !gameController.gameDone && !gameController.gameOver)
        {
            Vector3 right = camera.transform.right;

            right.y = 0;
            right = right.normalized;

            #region MouseInput
            //Vector2 input = new Vector2(Mover.Move.deltaX.ReadValue<float>(), Mover.Move.deltaY.ReadValue<float>());
            //transform.position += (Vector3.up * input.y + right * input.x) * Time.deltaTime;
            #endregion

            gyro = activeJoycon.GetGyro();
            Vector2 input = new Vector2(gyro.z, gyro.y);
            transform.position += (Vector3.up * input.y + right * input.x) * Time.deltaTime * speed;
        }
    }

    /// <summary>
    /// Rumble the controller
    /// Low: 160Hz, High: 320Hz
    /// Amplitude: 0.6
    /// </summary>
    public void RumbleJoyCon()
    {
        activeJoycon.SetRumble(160, 320, 0.6f, 200);
    }

    /// <summary>
    /// Stops reading from the controller without disconnecting
    /// </summary>
    public IEnumerator StopPolling()
    {
        activeJoycon.SetRumble(160, 320, 0, 0);
        activeJoycon.Detach();
        yield return joyconWaiter();
    }

    /// <summary>
    /// Waits until the JoyCon is disconnected
    /// </summary>
    IEnumerator joyconWaiter()
    {
        yield return new WaitWhile(() => activeJoycon.state == Joycon.state_.NOT_ATTACHED);
    }

    #region MouseInput
    /// <summary>
    /// Enables the SprinklerMover
    /// </summary>
    private void OnEnable()
    {
        //Mover.Enable();
    }

    /// <summary>
    /// Disables the SprinklerMover
    /// </summary>
    private void OnDisable()
    {
        //Mover.Disable();
    }
    #endregion
}
