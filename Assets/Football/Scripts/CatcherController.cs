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

    void Awake()
    {
        footballInput = new FootballInput();
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
    }

    // Update is called once per physics timestep
    void FixedUpdate()
    {
        if (Timer.countdownOver && !MissionInfo.gameOver && !MissionInfo.gameDone)
        {
            // Inactive!
            #region Move using the position of the mouse
            //Vector3 rawPosition = cam.ScreenToWorldPoint(new Vector3(footballInput.Gloves.Position.ReadValue<float>(), this.transform.position.y));
            //Vector3 targetPosition = new Vector3(rawPosition.x, this.transform.position.y, 0.0f);
            #endregion

            // Active!
            #region Move using the delta of the mouse
            float movementX = footballInput.Gloves.Move.ReadValue<float>() * Time.fixedDeltaTime;
            Vector3 targetPosition = new Vector3(gloves.position.x + movementX, this.transform.position.y, 0.0f);
            #endregion

            float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidth, maxWidth);
            targetPosition = new Vector3(targetWidth, targetPosition.y, targetPosition.z);
            transform.position = targetPosition;
        }
    }

    /// <summary>
    /// Enables the Input System
    /// </summary>
    private void OnEnable()
    {
        footballInput.Enable();
    }

    /// <summary>
    /// Disables the Input System
    /// </summary>
    private void OnDisable()
    {
        footballInput.Disable();
    }
}