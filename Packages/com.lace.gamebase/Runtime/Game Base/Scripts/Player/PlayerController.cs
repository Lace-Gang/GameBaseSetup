using UnityEngine;
//using log4net.Util;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

namespace GameBase
{

    [RequireComponent(typeof(CharacterController))] //A CharacterController is required
    [RequireComponent(typeof(PlayerCharacter))] //A CharacterController is required
    public class PlayerController : MonoBehaviour
    {
        ////Hidden Variables
        //Required Components/References
        private PlayerCharacter m_playerCharacter;
        private CharacterController m_controller;
        private Animator m_animator;
        private Transform m_view;

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
        [Tooltip("Max character movement speed under normal circumstances (not running, no power-ups etc)")]
        [SerializeField] float m_baseSpeed = 2.5f;
        [Tooltip("Used to calculate movement")]
        [SerializeField] float m_acceleration = 20.0f;
        [Tooltip ("How quickly the player can turn")]
        [SerializeField] float m_turnRate = 5f;
        //[SerializeField] float m_pushForce = 1f;                  /////////Either add functionality later or remove

        [Header("Jump Action")]
        [Tooltip("Player Input to jump. " +
            "This input should be a binding!")]
        [SerializeField] InputAction jumpAction;
        [Tooltip("Allow player to double jump")]
        [SerializeField] bool m_enableDoubleJump = false;
        [Tooltip("Used to calculate player jump height")]
        [SerializeField] float m_jumpHeight = 2f;
        [Tooltip("The amount of time after an innitial jump in which a double jump may be performed")]
        [SerializeField] float m_doubleJumpTimer = 1f;

        [Header("Sprint Action")]
        [Tooltip("Player Input to start/stop sprinting. " +
            "This input should be a binding.")]
        [SerializeField] InputAction sprintAction;
        [Tooltip("Adjusted max player movement speed used when sprinting")]
        [SerializeField] float m_sprintSpeed = 6f;


        /// <summary>
        /// Creates necessary references and binds InputActions to action functions
        /// </summary>
        private void Awake()
        {
            //Find and create references to required components
            m_playerCharacter = GetComponent<PlayerCharacter>();
            m_controller = GetComponent<CharacterController>();
            m_animator = GetComponent<Animator>();
            m_view = m_camera.GetComponent<Transform>();

            //Bind input actions
            moveAction.performed += OnMove;
            moveAction.canceled += OnMove;
            jumpAction.performed += OnJump;
            sprintAction.started += OnSprint;
            sprintAction.canceled += OnSprint;
        }

        /// <summary>
        /// Enables InputActions when player character is enabled
        /// </summary>
        private void OnEnable()
        {
            //Enable input actions
            moveAction.Enable();
            jumpAction.Enable();
            sprintAction.Enable();
        }

        /// <summary>
        /// Disables Input actions when player character is disabled
        /// </summary>
        private void OnDisable()
        {
            //Disable input actions
            moveAction.Disable();
            jumpAction.Disable();
            sprintAction.Disable();
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        /// <summary>
        /// Updates player states, updates and executes player movement, and updates other frame by frame data such as timers
        /// </summary>
        void Update()
        {
            ////Player States
            // Check if the player is grounded
            m_onGround = m_controller.isGrounded;
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

            //Update Animator
            //(m_controller.velocity.z > m_controller.velocity.x)? m_animator.SetFloat("Speed", m_controller.velocity.z) : m_animator.SetFloat("Speed", m_controller.velocity.x);
            m_animator.SetFloat("Speed", (MathF.Abs(m_controller.velocity.z) > MathF.Abs(m_controller.velocity.x)) ? MathF.Abs(m_controller.velocity.z) : MathF.Abs(m_controller.velocity.x));
        }

        /// <summary>
        /// Calculates the movement of the player and updates the player accordingly
        /// </summary>
        private void ExecuteMovement()
        {
            // Reset vertical velocity when grounded to prevent accumulating downward force
            if (m_onGround && m_velocity.y < 0)
            {
                m_velocity.y = -1; // Small downward force to keep player grounded
            }

            //Convert movement input into a world-space direction based on the player's view rotation
            Vector3 movement = new Vector3(m_movementInput.x, 0, m_movementInput.y);
            movement = Quaternion.AngleAxis(m_view.rotation.eulerAngles.y, Vector3.up) * movement;

            //Initialize acceleration vector for movement calculations
            Vector3 acceleration = Vector3.zero;
            acceleration.x = movement.x * m_acceleration;
            acceleration.z = movement.z * m_acceleration;

            //Reduce acceleration while in the air for smoother movement control
            if (!m_onGround) acceleration *= 0.4f;

            //Extract horizontal velocity (ignoring vertical movement)
            Vector3 vectorXZ = new Vector3(m_velocity.x, 0, m_velocity.z);

            //Apply acceleration to velocity while limiting max speed
            vectorXZ += acceleration * Time.deltaTime;
            vectorXZ = Vector3.ClampMagnitude(vectorXZ, (m_isSprinting) ? m_sprintSpeed : m_baseSpeed);

            //Assign updated velocity values
            m_velocity.x = vectorXZ.x;
            m_velocity.z = vectorXZ.z;


            //Apply drag to slow the player down when there is no input or when airborne
            if (movement.sqrMagnitude <= 0 || !m_onGround)
            {
                float drag = (m_onGround) ? 10 : 4;
                m_velocity.x = Mathf.MoveTowards(m_velocity.x, 0, drag * Time.deltaTime);
                m_velocity.z = Mathf.MoveTowards(m_velocity.z, 0, drag * Time.deltaTime);
            }

            //Smoothly rotate the player towards the movement direction
            if (movement.sqrMagnitude > 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * m_turnRate);
            }

            //Apply gravity
            m_velocity.y += m_gravity * Time.deltaTime;

            //Apply Movment
            m_controller.Move(m_velocity * Time.deltaTime);
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

        /// <summary>
        /// Causes player to jump and (if allowed) double jump
        /// </summary>
        /// <param name="ctx">>The CallbackContext from the InputAction (this is handled by the engine)</param>
        private void OnJump(InputAction.CallbackContext ctx)
        {
            //Only jumps if the character is already on the ground or if double jump is enabled and player is within the correct timeframe
            if (ctx.phase == InputActionPhase.Performed && m_onGround) 
            {
                //Add jump velocity to player to be applied as movement in next update
                m_velocity.y = Mathf.Sqrt(-2 * m_gravity * m_jumpHeight);
                //Set up variables to track double jump if applicable
                if(m_enableDoubleJump)                                      ////////////// These may want to be moved out of the if statement in case 
                {                                                           //////They can be used elsewhere (ie calculating fall damage)
                    m_hasJumped = true;                                     //////If move, remember to add && m_enableDoubleJump to the else if conditions
                    m_timeSinceLastJump = 0;
                }
            } else if (m_hasJumped && (m_timeSinceLastJump <= m_doubleJumpTimer))
            {
                //Add jump velocity to player to be applied as movement in next update
                m_velocity.y = Mathf.Sqrt(-2 * m_gravity * m_jumpHeight);
                m_hasJumped = false;    //to prevent infinite double jumping
            }
        }

        /// <summary>
        /// Toggles player sprinting
        /// </summary>
        /// <param name="ctx">>The CallbackContext from the InputAction (this is handled by the engine)</param>
        private void OnSprint(InputAction.CallbackContext ctx)
        {
            m_isSprinting = !m_isSprinting;
        }

        /// <summary>
        /// Performs player attack action
        /// </summary>
        /// <param name="ctx">>The CallbackContext from the InputAction (this is handled by the engine)</param>
        /// <exception cref="NotImplementedException">Class has not been implemented yet and should not yet be used</exception>
        private void OnAttack(InputAction.CallbackContext ctx)
        {
            throw new NotImplementedException("Player Attack logic is not yet defined");
        }
    }
}