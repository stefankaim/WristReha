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

    private void Awake()
    {
        camera = Camera.main;
        Mover = new SprinkleMover();
        //Mover.Move.deltaX.performed += ctx => MoveX(ctx.ReadValue<float>());
        //Mover.Move.deltaX.canceled += ctx => MoveX(0);
        //Mover.Move.deltaY.performed += ctx => MoveY(ctx.ReadValue<float>());
        //Mover.Move.deltaY.canceled += ctx => MoveY(0);
    }

    // Start is called before the first frame update
    void Start()
    {

        //transform.Rotate(Mover.Move.deltaY.ReadValue<float>() * RotationSpeed * Time.deltaTime, Mover.Move.deltaX.ReadValue<float>() * RotationSpeed * Time.deltaTime, 0f);//, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.countdownOver)
        {
            Vector3 right = camera.transform.right;

            right.y = 0;
            right = right.normalized;

            Vector2 input = new Vector2(Mover.Move.deltaX.ReadValue<float>(), Mover.Move.deltaY.ReadValue<float>());
            transform.position += (Vector3.up * input.y + right * input.x) * Time.deltaTime;
        }
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
