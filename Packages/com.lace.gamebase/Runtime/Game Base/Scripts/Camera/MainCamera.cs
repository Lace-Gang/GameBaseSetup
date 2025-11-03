using GameBase;
using UnityEngine;

namespace GameBase
{
    //All pre-established camera types
    //Any new types made by the user should go here.
    public enum CameraType
    {
        FIRSTPERSON,
        THIRDPERSON
    }

    public class MainCamera : MonoBehaviour
    {
        //Hidden Variables
        private float m_orbitRadius = 5f;   //Radius from the camera to the target
        private float m_mouseX = 0f;        //Mouse X position
        private float m_mouseY = 0f;        //Mouse Y position
        private float m_yaw = 0f;           //Tracks accumulative Camera Yaw (Rotation about the Y-axis)
        private float m_pitch = 0f;         //Tracks accumulative Camera Pitch (Rotaion about the X-axis)

        //Exposed Variables
        [Header("Universal Camera Settings")]
        [Tooltip("The transform of the object you want the camera to target")]
        [SerializeField] private Transform m_target;
        [Tooltip("Which camera is being used")]
        [SerializeField] private CameraType m_cameraType = CameraType.FIRSTPERSON;
        [Tooltip("How sensitive the camera is to mouse inputs")]
        [SerializeField] private float m_mouseSensitivity = 2f;
        [Tooltip("(In Degrees)")] 
        [SerializeField] private float m_minPitch = -45f;
        [Tooltip("(In Degrees)")] 
        [SerializeField] private float m_maxPitch = 80f;

        [Header("First Person Camera Settings")]
        [Tooltip("This property ONLY applies to the First Person Camera Type")]
        [SerializeField] private float m_heightOffset = 0.6f;

        [Header("Third Person Camera Settings")]
        [Tooltip("This property ONLY applies to the Third Person Camera Type")]
        [SerializeField] private float m_maxTransformDistance = 10f;
        [Tooltip("This property ONLY applies to the Third Person Camera Type")]
        [SerializeField] private float m_minTransformDistance = 2f;



        /// <summary>
        /// Sets up critical starting information
        /// </summary>
        void Start()
        {
            //Set up innitial yaw and pitch trackers
            m_yaw = transform.eulerAngles.y;
            m_pitch = transform.eulerAngles.x;
        }

        /// <summary>
        /// Updates camera facing and position
        /// </summary>
        void Update()
        {
            //Check for paused game
            if(GameInstance.Instance.getPaused()) return;

            //convert mouse axis to rotation
            m_mouseX = Input.GetAxis("Mouse X") * m_mouseSensitivity;
            m_mouseY = Input.GetAxis("Mouse Y") * m_mouseSensitivity;

            //track pitch and yaw accumulation
            m_yaw += m_mouseX;
            m_pitch -= m_mouseY;

            //Clamp pitch to be within specified constraints
            m_pitch = Mathf.Clamp(m_pitch, m_minPitch, m_maxPitch);


            //Camera behaves differently depending on which type of camera it is
            switch (m_cameraType)
            {
                case CameraType.FIRSTPERSON:
                    //Update camera facing
                    transform.eulerAngles = new Vector3(m_pitch, m_yaw, 0);

                    //Calculate and apply position based on target location
                    transform.position = new Vector3(m_target.position.x, m_target.position.y + m_heightOffset, m_target.position.z);
                    break;

                case CameraType.THIRDPERSON:
                    //look at target
                    transform.LookAt(m_target.position);

                    //Update camera facing
                    transform.eulerAngles = new Vector3(m_pitch, m_yaw, 0);         
                
                    //calculate and clamp orbital radius 
                    m_orbitRadius -= Input.mouseScrollDelta.y;
                    m_orbitRadius = Mathf.Clamp(m_orbitRadius, m_minTransformDistance, m_maxTransformDistance);

                    //calculate and apply camera position
                    transform.position = m_target.position - transform.rotation * Vector3.forward * m_orbitRadius;

                    ////Adjust camera position to prevent other objects from blocking line of sight to target
                    //casts ray from target to camera to check for objects that would block the camera view
                    RaycastHit hit;
                    Vector3 cameraDirection = transform.position - m_target.position; 

                    //If an object is in the way, adjusts camera position to keep target in view.
                    if(Physics.Raycast(m_target.position, cameraDirection, out hit))
                    {
                        //Moves camera to nearest point in view of target within max orbit radius
                        float distance = Mathf.Min(hit.distance, m_orbitRadius);
                        transform.position = m_target.position - (transform.rotation * Vector3.forward * distance);

                        //Prevent Camera view from clipping through solid objects (prevents players from seeing through those objects)
                        transform.Translate(hit.normal * 0.15f);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}