using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace supergoalkeeper
{

    public class CatcherController : MonoBehaviour
    {
        /**
		 * VARIABLES
		 * */
        private float maxWidth;// MAX WIDTH GAME SCENE

        private float movementX = 0;
        public FootballInput footballInput;
        private Rigidbody2D gloves;
        public Camera cam;

        void Awake()
        {
            footballInput = new FootballInput();
            footballInput.Gloves.Move.performed += ctx => MoveX(ctx.ReadValue<float>());
            footballInput.Gloves.Move.canceled += ctx => MoveX(0);
        }

        public static Bounds GetCombinedBoundingBoxOfChildren(Transform root)
        {
            if (root == null)
            {
                throw new System.Exception("The supplied transform was null");
            }

            var colliders = root.GetComponentsInChildren<Collider>();
            if (colliders.Length == 0)
            {
                throw new System.Exception("The supplied transform " + root?.name + " does not have any children with colliders");
            }

            Bounds totalBBox = colliders[0].bounds;
            foreach (var collider in colliders)
            {
                totalBBox.Encapsulate(collider.bounds);
            }
            return totalBBox;
        }

        // Use this for initialization
        void Start()
        {
            gloves = GetComponent<Rigidbody2D>();
            if (!cam)
            {
                cam = Camera.main;
            }

            //DEFINE THE PLAYABLE ZONE
            Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
            Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);

            //float catcherwidth = gloveBounds.bounds.extents.x;
            //float catcherwidth = GetCombinedBoundingBoxOfChildren(gloves.transform).extents.x;
            //float catcher = gloves;
            Collider2D collider = gloves.GetComponent<Collider2D>();
            Bounds totalBBox = collider.bounds;
            float catcherwidth = totalBBox.extents.x;


            maxWidth = targetWidth.x - catcherwidth;
        }

        // Update is called once per  physics timestep
        void FixedUpdate()
        {
            //TRANSLATE THE CLICK/TOUCH POS TO GAME POINT
            //Vector3 rawPosition	= cam.ScreenToWorldPoint(movementX);

            //SET THE NEW POS OF CATCHER
            //Vector3 targetPosition = new Vector3(rawPosition.x,this.transform.position.y,0.0f);

            Vector3 targetPosition = new Vector3(gloves.position.x + movementX, this.transform.position.y, 0.0f);



            Vector2 position = new Vector2();
            position.y = 0f;
            position.x = movementX;
            //gloves.velocity = position * Time.fixedDeltaTime;


            //DEFINE THE PLAYABLE ZONE
            float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidth, maxWidth);

            //SET THE NEW POS OF CATCHER 
            targetPosition = new Vector3(targetWidth, targetPosition.y, targetPosition.z);

            //MOVE CATCHER
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