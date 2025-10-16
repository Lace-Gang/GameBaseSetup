//using log4net.Util;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameBase
{
    public class PlayerController : MonoBehaviour
    {
        //private PlayerCharacter m_playerCharacter;
        //private float m_movementSpeed = 0f;
        //private float m_gravity = -9.8f;
        //Vector2 m_movementInput = Vector2.zero;
        //Vector3 m_velocity = Vector3.zero;
        //private bool m_isSprinting = false;
        //private bool m_onGround = true;
        //
        ////InputActions
        //
        //[Header("Move Action")]
        //[Tooltip("Player Input for moving the character. " + 
        //    "This input MUST be an Up/Down/Left/Right Composit that evaluates to a Vector2!")]
        //[SerializeField] InputAction moveAction;
        //[SerializeField] float m_baseSpeed = 2.5f;
        //[SerializeField] float m_acceleration = 20.0f;
        //[SerializeField] float m_turnRate = 5f;
        //[SerializeField] float m_pushForce = 1f;
        //
        //[Header("Jump Action")]
        //[Tooltip("Player Input to jump. " +
        //    "This input should be a binding.")]
        //[SerializeField] InputAction jumpAction;
        //[SerializeField] float m_jumpheight = 2f;
        //
        //[Header("Sprint Action")]
        //[Tooltip("Player Input to start/stop sprinting. " +
        //    "This input should be a binding.")]
        //[SerializeField] InputAction sprintAction;
        //[SerializeField] float m_sprintSpeed = 6f;
        //
        //
        ////[Header("Save and Load Conditions")]
        //
        //
        //
        //private void Awake()
        //{
        //    //bind input actions
        //    moveAction.performed += OnMove;
        //    moveAction.canceled += OnMove;
        //    jumpAction.performed += OnJump;
        //    sprintAction.started += OnSprintStart;
        //    sprintAction.canceled += OnSprintEnd;
        //
        //    //set movement speed
        //    //movementSpeed = baseSpeed;
        //}
        //
        //private void OnEnable()
        //{
        //    //enable input actions
        //    moveAction.Enable();
        //    jumpAction.Enable();
        //    sprintAction.Enable();
        //}
        //
        //private void OnDisable()
        //{
        //    //disable input actions
        //    moveAction.Disable();
        //    jumpAction.Disable();
        //    sprintAction.Disable();
        //}
        //
        //
        //public void setPlayerReference(PlayerCharacter playerRef)
        //{
        //    m_playerCharacter = playerRef;
        //}
        //
        //
        //// Start is called once before the first execution of Update after the MonoBehaviour is created
        //void Start()
        //{
        //    
        //}
        //
        //// Update is called once per frame
        //void Update()
        //{
        //    ////Player State
        //
        //
        //
        //
        //    ////Player movement
        //    ///
        //    // Reset vertical velocity when grounded to prevent accumulating downward force
        //    if (m_onGround && m_velocity.y < 0)
        //    {
        //        m_velocity.y = -1; // Small downward force to keep player grounded
        //                           //animator.SetBool("OnGround", onGround);
        //    }
        //    
        //    //Camera Dependent
        //    //// Convert movement input into a world-space direction based on the player's view rotation
        //    Vector3 movement = new Vector3(m_movementInput.x, 0, m_movementInput.y);
        //    //movement = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up) * movement;
        //    
        //    // Initialize acceleration vector for movement calculations
        //    Vector3 acceleration = Vector3.zero;
        //    acceleration.x = movement.x * m_acceleration;
        //    acceleration.z = movement.z * m_acceleration;
        //    
        //    //// Reduce acceleration while in the air for smoother movement control
        //    //if (!onGround) acceleration *= 0.4f;
        //    
        //    // Extract horizontal velocity (ignoring vertical movement)
        //    Vector3 vectorXZ = new Vector3(m_velocity.x, 0, m_velocity.z);
        //    
        //    // Apply acceleration to velocity while limiting max speed
        //    vectorXZ += acceleration * Time.deltaTime;
        //    vectorXZ = Vector3.ClampMagnitude(vectorXZ, (m_isSprinting) ? m_sprintSpeed : m_baseSpeed);
        //    
        //    // Assign updated velocity values
        //    m_velocity.x = vectorXZ.x;
        //    m_velocity.z = vectorXZ.z;
        //
        //
        //    // Apply drag to slow the player down when there is no input or when airborne
        //    if (movement.sqrMagnitude <= 0 || !m_onGround)
        //    {
        //        float drag = (m_onGround) ? 10 : 4;
        //        m_velocity.x = Mathf.MoveTowards(m_velocity.x, 0, drag * Time.deltaTime);
        //        m_velocity.z = Mathf.MoveTowards(m_velocity.z, 0, drag * Time.deltaTime);
        //    }
        //
        //    // Smoothly rotate the player towards the movement direction
        //    if (movement.sqrMagnitude > 0)
        //    {
        //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * m_turnRate);
        //    }
        //    
        //    // Apply gravity
        //    m_velocity.y += m_gravity * Time.deltaTime;
        //
        //    //Apply Movment
        //    //transform.Translate(-Vector3.forward * Time.deltaTime * m_movementInput.x);
        //    //transform.Translate(Vector3.right * Time.deltaTime * m_movementInput.y);
        //    transform.Translate(-Vector3.forward * Time.deltaTime * m_velocity.x);
        //    transform.Translate(Vector3.up * Time.deltaTime * m_velocity.y);
        //    transform.Translate(Vector3.right * Time.deltaTime * m_velocity.z);
        //    
        //}
        //
        //
        //
        //
        //private void OnMove(InputAction.CallbackContext ctx)
        //{
        //    //Get movement vector from Input System
        //    m_movementInput = ctx.ReadValue<Vector2>();
        //    //Vector2 movementVector = ctx.ReadValue<Vector2>();
        //
        //    //Normalize movement vector
        //    //movementVector.Normalize();
        //    //m_movementInput.Normalize();
        //
        //    //adjust movement vector to account for character speed and frame rate
        //    //movementVector *= m_movementSpeed * Time.deltaTime;
        //    //m_movementInput *= m_movementSpeed * Time.deltaTime;
        //
        //    //Debug.Log("Move: " + ctx.ReadValue<Vector2>());
        //    Debug.Log("Move: ");
        //}
        //
        //
        //private void OnJump(InputAction.CallbackContext ctx)
        //{
        //    Debug.Log("Jump");
        //}
        //
        //
        //private void OnSprintStart(InputAction.CallbackContext ctx)
        //{
        //    m_isSprinting = true;
        //    Debug.Log("Sprint");
        //    //m_movementSpeed = m_sprintSpeed;
        //}
        //
        //private void OnSprintEnd(InputAction.CallbackContext ctx)
        //{
        //    m_isSprinting = false;
        //    Debug.Log("Stop Sprint");
        //    //m_movementSpeed = m_baseSpeed;
        //}
        //
        //private void OnAttack(InputAction.CallbackContext ctx)
        //{
        //    Debug.Log("Attack");
        //}
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