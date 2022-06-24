using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoonController : MonoBehaviour
{
    public float rotationSpeedUpDown = 90f;
    public float rotationSpeedLeftRight = 20f;
    public TimeCounter Timer;
    public BalanceGameController gameController;
    public PauseMenu PauseMenu;


    private List<Joycon> joycons;
    private Joycon activeJoycon;
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public int jc_ind = 0;
    public Quaternion orientation;

    private float pauseCooldown = 0.25f;
    private float pauseCooldownElapsed = 0;

    private void Awake()
    {

    }

    private void Start()
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

    private void FixedUpdate()
    {
        if (!gameController.gameOver && !gameController.gameDone)
        {
            #region MouseMovement
            //Vector2 input = new Vector2(Mover.Move.deltaX.ReadValue<float>(), Mover.Move.deltaY.ReadValue<float>());
            //transform.Rotate(-1 * input.y * rotationSpeedUpDown * Time.deltaTime, 0, -1 * input.x * rotationSpeedLeftRight * Time.deltaTime, Space.World);
            #endregion

            if (activeJoycon != null)
            {
                // Gyro values: x, y, z axis values (in radians per second)
                gyro = activeJoycon.GetGyro();
                // Accel values:  x, y, z axis values (in Gs)
                accel = activeJoycon.GetAccel();

                #region GyroscopeMovement
                //transform.Rotate(-1 * gyro.y * rotationSpeedUpDown * Time.deltaTime, 0, -1 * gyro.x * rotationSpeedLeftRight * Time.deltaTime);
                #endregion

                transform.rotation = ConvertToGame(activeJoycon.GetVector());
                transform.Rotate(97, 0, 0);
            }
        }
    }

    /// <summary>
    /// Convert from JoyCon to Game Quaternions
    /// </summary>
    /// <param name="input">The Quaternion from the JoyCon</param>
    /// <returns>Quaternion in game perspective</returns>
    private Quaternion ConvertToGame(Quaternion input)
    {
        Quaternion output;

        output.x = input.x;
        output.y = -input.z;
        output.z = input.y;
        output.w = input.w;

        return output;
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

    /// <summary>
    /// Stops reading from the controller without disconnecting
    /// </summary>
    public void StopPolling()
    {
        activeJoycon.Detach();
    }
}
