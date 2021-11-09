using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinkler : MonoBehaviour
{
    private SprinkleMover Mover;

    public float mouseX = 0f;
    public float mouseY = 0f;
    public float RotationSpeed = 10f;
    public Transform target;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        Mover = new SprinkleMover();
        /*Mover.Move.deltaX.performed += ctx => MoveX(ctx.ReadValue<float>());
        Mover.Move.deltaX.canceled += ctx => MoveX(0);
        Mover.Move.deltaY.performed += ctx => MoveY(ctx.ReadValue<float>());
        Mover.Move.deltaY.canceled += ctx => MoveY(0);*/
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        /*Transform current = transform;
        transform.Rotate(mouseY * Time.deltaTime, mouseX * Time.deltaTime, 0);
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
        transform.rotation = q;*/
        //transform.Rotate(Mover.Move.deltaY.ReadValue<float>() * RotationSpeed * Time.deltaTime, Mover.Move.deltaX.ReadValue<float>() * RotationSpeed * Time.deltaTime, 0f);//, Space.Self);
        /* Example for using an accerelometer
        accelx = Input.acceleration.x;
        accely = Input.acceleration.y;
        accelz = Input.acceleration.z;
        transform.Rotate (accelx * Time.deltaTime, accely * Time.deltaTime, accelz * Time.deltaTime); 
        */


        // the second argument, upwards, defaults to Vector3.up



        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }

    private void FixedUpdate()
    {

    }

    /// <summary>
    /// Sets the rotation value for the Y-Axis
    /// </summary>
    /// <param name="deltaY">The delta to the last position of the Input</param>
    void MoveY(float deltaY)
    {
        if (deltaY >= 1 || deltaY <= -1)
        {
            //Debug.Log("Y-movement: " + deltaY);
            mouseY = deltaY * RotationSpeed;
        }
        else mouseY = 0;
    }

    /// <summary>
    /// Sets the rotation value for the X-Axis
    /// </summary>
    /// <param name="deltaX">The delta to the last position of the Input</param>
    void MoveX(float deltaX)
    {
        if (deltaX >= 1 || deltaX <= -1)
        {
            //Debug.Log("Y-movement: " + deltaY);
            mouseX = deltaX * RotationSpeed;
        }
        else mouseX = 0;
    }

    /// <summary>
    /// Enables the SprinklerMover
    /// </summary>
    private void OnEnable()
    {
        Mover.Enable();
    }

    /// <summary>
    /// Disables the SprinklerMover
    /// </summary>
    private void OnDisable()
    {
        Mover.Disable();
    }
}
