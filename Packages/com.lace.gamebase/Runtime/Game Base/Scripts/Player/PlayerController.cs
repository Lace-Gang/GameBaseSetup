using UnityEngine;
//using log4net.Util;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace GameBase
{

    [RequireComponent(typeof(CharacterController))] //A CharacterController is required
    public class PlayerController : MonoBehaviour
    {
        ////Hidden Variables
        //Required Components/References
        private PlayerCharacter m_playerCharacter;
        CharacterController controller;
        private Transform m_view;   //take this out if we don't end up using it

        //Required values at start
        private float m_gravity = -9.8f;

        //Player States
        private bool m_isSprinting = false;
        private bool m_onGround = true;
        private bool m_hasJumped = false;

        //Calculated at runtime
        private float m_timeSinceLastJump = 0f;
        Vector2 m_movementInput = Vector2.zero;
        Vector3 m_velocity = Vector3.zero;


        ////Exposed Variables
        [SerializeField] MainCamera m_camera;

        //InputActions
        [Header("Move Action")]
        [Tooltip("Player Input for moving the character. " + 
            "This input MUST be an Up/Down/Left/Right Composit that evaluates to a Vector2!")]
        [SerializeField] InputAction moveAction;
        [SerializeField] float m_baseSpeed = 2.5f;
        [SerializeField] float m_acceleration = 20.0f;
        [SerializeField] float m_turnRate = 5f;
        //[SerializeField] float m_pushForce = 1f;

        [Header("Jump Action")]
        [Tooltip("Player Input to jump. " +
            "This input should be a binding.")]
        [SerializeField] InputAction jumpAction;
        [SerializeField] bool m_enableDoubleJump = false;
        [SerializeField] float m_jumpHeight = 2f;
        [Tooltip("The amount of time after a jump in which a double jump may be performed")]
        [SerializeField] float m_doubleJumpTimer = 1f;

        [Header("Sprint Action")]
        [Tooltip("Player Input to start/stop sprinting. " +
            "This input should be a binding.")]
        [SerializeField] InputAction sprintAction;
        [SerializeField] float m_sprintSpeed = 6f;

        //public Transform View { get => view; set { view = value; } }


        /// <summary>
        /// Creates necessary references and binds InputActions to action functions
        /// </summary>
        private void Awake()
        {
            //Find and create references to required components
            controller = GetComponent<CharacterController>();
            m_view = m_camera.GetComponent<Transform>();

            //bind input actions
            moveAction.performed += OnMove;
            moveAction.canceled += OnMove;
            jumpAction.performed += OnJump;
            sprintAction.started += OnSprintStart;
            sprintAction.canceled += OnSprintEnd;
        }

        /// <summary>
        /// Enables InputActions when player character is enabled
        /// </summary>
        private void OnEnable()
        {
            //enable input actions
            moveAction.Enable();
            jumpAction.Enable();
            sprintAction.Enable();
        }

        /// <summary>
        /// Disables Input actions when player character is disabled
        /// </summary>
        private void OnDisable()
        {
            //disable input actions
            moveAction.Disable();
            jumpAction.Disable();
            sprintAction.Disable();
        }


        public void setPlayerReference(PlayerCharacter playerRef)
        {
            m_playerCharacter = playerRef;
        }



        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            ////Player State
            // Check if the player is grounded
            m_onGround = controller.isGrounded;
            /////////We'll want to move this into the OnLand function later
            if(m_onGround && m_timeSinceLastJump > 0.1) //the second check here is only necessary because this is being done in the update function
            {
                m_hasJumped = false;
            }


            ////Player movement
            ExecuteMovement();
                      

            ////Update timers
            //Timer for Double Jump
            if(m_hasJumped)
            {
                m_timeSinceLastJump += Time.deltaTime;
            }
        }


        private void ExecuteMovement()
        {
            // Reset vertical velocity when grounded to prevent accumulating downward force
            if (m_onGround && m_velocity.y < 0)
            {
                m_velocity.y = -1; // Small downward force to keep player grounded
            }

            //Camera Dependent
            //// Convert movement input into a world-space direction based on the player's view rotation
            Vector3 movement = new Vector3(m_movementInput.x, 0, m_movementInput.y);
            movement = Quaternion.AngleAxis(m_view.rotation.eulerAngles.y, Vector3.up) * movement;

            // Initialize acceleration vector for movement calculations
            Vector3 acceleration = Vector3.zero;
            acceleration.x = movement.x * m_acceleration;
            acceleration.z = movement.z * m_acceleration;

            //// Reduce acceleration while in the air for smoother movement control
            if (!m_onGround) acceleration *= 0.4f;

            // Extract horizontal velocity (ignoring vertical movement)
            Vector3 vectorXZ = new Vector3(m_velocity.x, 0, m_velocity.z);

            // Apply acceleration to velocity while limiting max speed
            vectorXZ += acceleration * Time.deltaTime;
            vectorXZ = Vector3.ClampMagnitude(vectorXZ, (m_isSprinting) ? m_sprintSpeed : m_baseSpeed);

            // Assign updated velocity values
            m_velocity.x = vectorXZ.x;
            m_velocity.z = vectorXZ.z;


            // Apply drag to slow the player down when there is no input or when airborne
            if (movement.sqrMagnitude <= 0 || !m_onGround)
            {
                float drag = (m_onGround) ? 10 : 4;
                m_velocity.x = Mathf.MoveTowards(m_velocity.x, 0, drag * Time.deltaTime);
                m_velocity.z = Mathf.MoveTowards(m_velocity.z, 0, drag * Time.deltaTime);
            }

            // Smoothly rotate the player towards the movement direction
            if (movement.sqrMagnitude > 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * m_turnRate);
            }

            // Apply gravity
            m_velocity.y += m_gravity * Time.deltaTime;

            //Apply Movment
            controller.Move(m_velocity * Time.deltaTime);
        }


       
        /// <summary>
        /// Updates m_movementInput to match player controls
        /// </summary>
        /// <param name="ctx">The CallbackContext from the InputAction (this is handled by the engine)</param>
        private void OnMove(InputAction.CallbackContext ctx)
        {
            //Get movement vector from Input System
            m_movementInput = ctx.ReadValue<Vector2>();
        }


        private void OnJump(InputAction.CallbackContext ctx)
        {
            //Only jumps if the character is already on the ground or if double jump is enabled and player is within the correct timeframe
            if (ctx.phase == InputActionPhase.Performed && m_onGround) 
            {
                m_velocity.y = Mathf.Sqrt(-2 * m_gravity * m_jumpHeight);
                //Set up variables to track double jump if applicable
                if(m_enableDoubleJump)
                {
                    m_hasJumped = true;
                    m_timeSinceLastJump = 0;
                }
            } else if (m_hasJumped && (m_timeSinceLastJump <= m_doubleJumpTimer))
            {
                m_velocity.y = Mathf.Sqrt(-2 * m_gravity * m_jumpHeight);
                m_hasJumped = false;    //to prevent infinite double jumping
            }
        }


        private void OnSprintStart(InputAction.CallbackContext ctx)
        {
            m_isSprinting = true;
        }

        private void OnSprintEnd(InputAction.CallbackContext ctx)
        {
            m_isSprinting = false;
        }

        private void OnAttack(InputAction.CallbackContext ctx)
        {
            Debug.Log("Attack");
        }
    }
}









//////Player State
//
//
//
//
//////Player movement
/////
//// Reset vertical velocity when grounded to prevent accumulating downward force
//if (m_onGround && m_velocity.y < 0)
//{
//    m_velocity.y = -1; // Small downward force to keep player grounded
//                       //animator.SetBool("OnGround", onGround);
//}
//
////Camera Dependent
////// Convert movement input into a world-space direction based on the player's view rotation
//Vector3 movement = new Vector3(m_movementInput.x, 0, m_movementInput.y);
////movement = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up) * movement;
//
//// Initialize acceleration vector for movement calculations
//Vector3 acceleration = Vector3.zero;
//acceleration.x = movement.x * m_acceleration;
//acceleration.z = movement.z * m_acceleration;
//
////// Reduce acceleration while in the air for smoother movement control
////if (!onGround) acceleration *= 0.4f;
//
//// Extract horizontal velocity (ignoring vertical movement)
//Vector3 vectorXZ = new Vector3(m_velocity.x, 0, m_velocity.z);
//
//// Apply acceleration to velocity while limiting max speed
//vectorXZ += acceleration * Time.deltaTime;
//vectorXZ = Vector3.ClampMagnitude(vectorXZ, (m_isSprinting) ? m_sprintSpeed : m_baseSpeed);
//
//// Assign updated velocity values
//m_velocity.x = vectorXZ.x;
//m_velocity.z = vectorXZ.z;
//
//// Apply drag to slow the player down when there is no input or when airborne
//if (movement.sqrMagnitude <= 0 || !onGround)
//{
//    float drag = (onGround) ? 10 : 4;
//    velocity.x = Mathf.MoveTowards(velocity.x, 0, drag * Time.deltaTime);
//    velocity.z = Mathf.MoveTowards(velocity.z, 0, drag * Time.deltaTime);
//}
//
//// Smoothly rotate the player towards the movement direction
//if (movement.sqrMagnitude > 0)
//{
//    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * Data.turnRate);
//}
//
//// Apply gravity
//velocity.y += Data.gravity * Time.deltaTime;