using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace supergoalkeeper
{

    public class CatcherController : MonoBehaviour
    {
        private float maxWidth;

        private float movementX = 0f;
        public FootballInput footballInput;
        private Rigidbody2D gloves;
        public Camera cam;

        void Awake()
        {
            footballInput = new FootballInput();
            footballInput.Gloves.Move.performed += ctx => MoveX(ctx.ReadValue<float>());
            footballInput.Gloves.Move.canceled += ctx => MoveX(0);
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

        // Update is called once per  physics timestep
        void FixedUpdate()
        {
            // Position using the actual mouse position
            // INACTIVE!
            #region MousePosition
            //Vector3 rawPosition = cam.ScreenToWorldPoint(new Vector3(footballInput.Gloves.Position.ReadValue<float>(), this.transform.position.y));
            //Vector3 targetPosition = new Vector3(rawPosition.x, this.transform.position.y, 0.0f);
            #endregion

            // Position using the delta of the mouse position
            // ACTIVE!
            #region DeltaMouse
            Vector3 targetPosition = new Vector3(gloves.position.x + movementX, this.transform.position.y, 0.0f);
            #endregion

            float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidth, maxWidth);
            targetPosition = new Vector3(targetWidth, targetPosition.y, targetPosition.z);
            transform.position = targetPosition;
        }

        /// <summary>
        /// Moves the gloves left and right
        /// </summary>
        /// <param name="deltaX">The delta to the last position of the Input</param>
        private void MoveX(float deltaX)
        {
            if (deltaX >= 1 || deltaX <= -1)
            {
                //Debug.Log("X-Movement: " + deltaX);
                movementX = deltaX / 64;
            }
            else movementX = 0;
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

}