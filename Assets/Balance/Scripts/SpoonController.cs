using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoonController : MonoBehaviour
{
    private SpoonMover Mover;
    public float rotationSpeed = 5f;
    public TimeCounter Timer;
    public BalanceGameController gameController;

    private void Awake()
    {
        Mover = new SpoonMover();
    }

    private void Update()
    {
        if (Timer.countdownOver && !gameController.gameOver && !gameController.gameDone)
        {
            Vector2 input = new Vector2(Mover.Move.deltaX.ReadValue<float>(), Mover.Move.deltaY.ReadValue<float>());
            transform.Rotate(-1 * input.y * rotationSpeed * Time.deltaTime, 0, -1 * input.x * rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    /// <summary>
    /// Enables the SpoonMover
    /// </summary>
    private void OnEnable()
    {
        Mover.Enable();
    }

    /// <summary>
    /// Disables the SpoonMover
    /// </summary>
    private void OnDisable()
    {
        Mover.Disable();
    }
}
