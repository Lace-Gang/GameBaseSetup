using UnityEngine;
//using log4net.Util;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;


namespace GameBase
{

    [RequireComponent(typeof(CharacterController))] //A CharacterController is required
    [RequireComponent(typeof(PlayerCharacter))]
    public class PlayerController : MonoBehaviour
    {
        ////Hidden Variables
        //Required Components/References
        private PlayerCharacter m_playerCharacter;      //Player character component
        private CharacterController m_controller;       //Character controller component
        private Animator m_animator;                    //Player animator component
        private Transform m_view;                       //Player focused camera transform

        //Required values at start
        private float m_gravity = -9.8f;                //Gravity scale

        //Player States
        private bool m_isSprinting = false;             //Is the player currently sprinting
        private bool m_onGround = true;                 //Is the player currently standing on something, or are they in the air
        private bool m_hasJumped = false;               //Has the player recently jumped without having hit the ground since
        private bool m_isDead = false;                  //Is the player dead

        //Calculated at runtime
        private float m_timeSinceLastJump = 0f;         //How long since the last time the player jumped (only updated before player touches the ground
        Vector2 m_movementInput = Vector2.zero;         //Current movement input vector (which direction is the player supposed to move)
        Vector3 m_velocity = Vector3.zero;              //Current player velocity


        ////Exposed Variables
        [Tooltip("Camera being used to follow the player and provide steering")]
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
        //[Tooltip("Distance above ground (or other surface) to trigger landing animation")]
        //[SerializeField] float m_landingDistance = 1f;

        [Header("Sprint Action")]
        [Tooltip("Player Input to start/stop sprinting. " +
            "This input should be a binding.")]
        [SerializeField] InputAction sprintAction;
        [Tooltip("Adjusted max player movement speed used when sprinting")]
        [SerializeField] float m_sprintSpeed = 6f;

        [Header("Fall Damage")]
        [SerializeField] bool m_fallDamageEnabled = false;
        [Tooltip("Base damage taken if above fall damage velocity threshold")]
        [SerializeField] float m_baseFallDamage;
        [Tooltip("Please input a positive float")]
        [SerializeField] float m_minFallVelocity;
        [Tooltip("Scale base fall damage by velocity on ipmact")]
        [SerializeField] bool m_scaleFallDamage;
        [Tooltip("Multiplier for scaling fall damage with velocity")]
        [SerializeField] float m_fallDamageScaler;


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
        /// Updates player states, updates and executes player movement, and updates other frame by frame data such as timers. Also checks for and applies fall damage.
        /// </summary>
        void Update()
        {
            EvaluateFallDamage(); //MUST happen before updating m_onGround



            ////Player States

            // Check if the player is grounded
            m_onGround = m_controller.isGrounded;
            //CheckLand();
            m_animator.SetBool("IsGrounded", m_onGround);


            ///////////We'll want to move this into the OnLand function later
            //if(m_hasJumped && m_onGround && m_timeSinceLastJump > 0.1) //the second check here is only necessary because this is being done in the update function
            //{
            //    m_hasJumped = false;
            //    m_animator.SetTrigger("Land");
            //}


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
            //m_animator.SetFloat("Speed", (MathF.Abs(m_controller.velocity.z) > MathF.Abs(m_controller.velocity.x)) ? MathF.Abs(m_controller.velocity.z) : MathF.Abs(m_controller.velocity.x));
            //m_animator.SetFloat("Speed", m_controller.velocity.magnitude);
            m_animator.SetFloat("HorizontalSpeed", Vector3.Magnitude(new Vector3(m_controller.velocity.x, 0, m_controller.velocity.z)));
            m_animator.SetFloat("VerticalSpeed", m_controller.velocity.y);
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


        #region Input Action Functions

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

                //Tell animator component to jump
                m_animator.SetTrigger("Jump");

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

                //Tell animator component to jump
                m_animator.SetTrigger("Jump");
            }
        }

        /// <summary>
        /// Toggles player sprinting
        /// </summary>
        /// <param name="ctx">>The CallbackContext from the InputAction (this is handled by the engine)</param>
        private void OnSprint(InputAction.CallbackContext ctx)
        {
            //toggle is_sprinting
            m_isSprinting = !m_isSprinting;
        }

        /// <summary>
        /// Performs player attack action. Not yet implemented.
        /// </summary>
        /// <param name="ctx">>The CallbackContext from the InputAction (this is handled by the engine)</param>
        /// <exception cref="NotImplementedException">Class has not been implemented yet and should not yet be used</exception>
        private void OnAttack(InputAction.CallbackContext ctx)
        {
            //player cannot attack when dead
            if(m_isDead) return;

            throw new NotImplementedException("Player Attack logic is not yet defined");
        }


        #endregion Input Action Functions



        /// <summary>
        /// Player hit state. Not yet fully implemented.
        /// </summary>
        public void OnTakeHit()
        {
            //player cannot be hit when dead
            if (m_isDead) return;
            //throw new System.NotImplementedException();


            Debug.Log("Hit Taken!");    //Debug line. To be removed later.
        }

        /// <summary>
        /// Player death state. Disables input, updates isDead bool, and tells animator to start the death animation
        /// </summary>
        public void OnDeath()
        {
            //player cannot die if already dead
            if (m_isDead) return;

            Debug.Log("Player is dead!");           //Debug line. To be removed later

            m_isDead = true;                //Sets isDead bool to true so other functions in the Player Controller are aware
            OnDisable();                    //Disable player movement input
            m_movementInput = Vector2.zero; //Sets current player movement input vector to zero

            m_animator.SetTrigger("Die");   //Sets death animation trigger in the animator
        }


        /// <summary>
        /// Evaluates conditions for fall damage. If conditions are met, calculates and applies fall damage.
        /// </summary>
        public void EvaluateFallDamage()
        {
            //Check for and apply fall damage
            if (m_fallDamageEnabled && !m_onGround && m_controller.isGrounded)
            {
                if (m_controller.velocity.y < -m_minFallVelocity)   //If the player's current y velocity is above the threshold for fall damage (technically if it's bellow the threshold due to y velocity being negative when falling)
                {
                    if (!m_scaleFallDamage) m_playerCharacter.TakeDamage(m_baseFallDamage, GetComponent<GameObject>()); //applies base damage if damage should not scale with velocity
                    else
                    {
                        //if damage SHOULD scale with velocity, calculates and applies fall damage
                        float fallDamage = m_baseFallDamage * m_fallDamageScaler * (-m_controller.velocity.y - m_minFallVelocity);  
                        m_playerCharacter.TakeDamage(fallDamage, GetComponent<GameObject>());
                    }
                }
            }
        }


        //These functions marked for likely removal in comming updates

       /// <summary>
       /// Checks if Character is about to land. If so, executes character landing.
       /// </summary>
      //private void CheckLand()
      //{
      //     Vector3 rayPosition = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
      //
      //    //uses raycast to check ground distance
      //    RaycastHit hit;
      //    if(Physics.Raycast(rayPosition, Vector3.down, out hit, m_landingDistance) && hit.collider.gameObject.tag != "Player")
      //    {
      //         Debug.DrawRay(rayPosition, Vector3.down * m_landingDistance, color: Color.red);
      //         Debug.Log(hit.collider.gameObject);
      //         m_onGround = true;
      //        //Land();
      //    }
      //}
       //
       ///// <summary>
       ///// Executes character landing
       ///// </summary>
       //private void Land()
       //{
       //    m_hasJumped = false;
       //    m_animator.SetTrigger("Land"); //notifies animator to trigger landing animation
       //}
    }
}