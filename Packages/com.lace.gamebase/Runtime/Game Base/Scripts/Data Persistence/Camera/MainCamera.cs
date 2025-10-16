using GameBase;
using UnityEngine;

namespace GameBase
{
    public enum CameraType
    {
        FIRSTPERSON,
        THIRDPERSON
    }

    public class MainCamera : MonoBehaviour
    {
        //Hidden Variables
        private float m_orbitRadius = 5f;
        private float m_mouseX = 0f;
        private float m_mouseY = 0f;

        //Exposed Variables
        [Header("Universal Camera Settings")]
        [Tooltip("The transform of the object you want the camera to target")]
        [SerializeField] private Transform m_target;
        [SerializeField] private CameraType m_cameraType = CameraType.FIRSTPERSON;
        [Tooltip("How sensitive the camera is to mouse inputs")]
        [SerializeField] private float m_mouseSensitivity = 2f;

        [Header("First Person Camera Settings")]
        [Tooltip("This property ONLY applies to the First Person Camera Type")]
        [SerializeField] private float m_heightOffset = 0.25f;

        [Header("Third Person Camera Settings")]
        [Tooltip("This property ONLY applies to the Third Person Camera Type")]
        [SerializeField] private float m_maxTransformDistance = 10f;
        [Tooltip("This property ONLY applies to the Third Person Camera Type")]
        [SerializeField] private float m_minTransformDistance = 2f;
        [Tooltip("This property ONLY applies to the Third Person Camera Type")] //take this out unless we figure out how to do the clamp
        [SerializeField] private float m_minPitch = -45f;
        [Tooltip("This property ONLY applies to the Third Person Camera Type")] //take this out unless we figure out how to do the clamp
        [SerializeField] private float m_maxPitch = 80f;



        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //probably eventually move these two to the game instance or manager or something
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
            switch (m_cameraType)
            {
                case CameraType.FIRSTPERSON:



                    //convert mouse axis to rotation
                    m_mouseX = Input.GetAxis("Mouse X") * m_mouseSensitivity;
                    m_mouseY = Input.GetAxis("Mouse Y") * m_mouseSensitivity;
                    transform.eulerAngles += new Vector3(-m_mouseY, m_mouseX, 0);

                    //Calculate and apply position based on character location
                    transform.position = new Vector3(m_target.position.x, m_target.position.y + m_heightOffset, m_target.position.z);

                    //transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                    //transform.SetPositionAndRotation(new Vector3(m_target.position.x, m_target.position.y + m_heightOffset, m_target.position.z), m_target.rotation);
                    break;
                case CameraType.THIRDPERSON:
                    //look at target
                    transform.LookAt(m_target.position);
                    
                    //convert mouse axis to rotation
                    m_mouseX = Input.GetAxis("Mouse X") * m_mouseSensitivity;
                    m_mouseY = Input.GetAxis("Mouse Y") * m_mouseSensitivity;
                    transform.eulerAngles += new Vector3(-m_mouseY, m_mouseX, 0);

                    //If camera rotation is too steep or two shallow, adjust it to be within acceptable bounds
                    //if(transform.rotation.x < m_minPitch || transform.rotation.x > m_maxPitch)
                    //{
                    //    Vector3 currentEulerAngle = transform.rotation.eulerAngles;
                    //    currentEulerAngle.x = Mathf.Clamp(transform.rotation.x, m_minPitch, m_maxPitch);
                    //    transform.eulerAngles = currentEulerAngle;
                    //}

                    //transform.rotation.y = Mathf.Clamp(m_mouseY, m_minPitch, m_maxPitch);
                    
                    
                    //calculate and clamp orbital radius and apply it to camera
                    m_orbitRadius -= Input.mouseScrollDelta.y;
                    m_orbitRadius = Mathf.Clamp(m_orbitRadius, m_minTransformDistance, m_maxTransformDistance);

                    //calculate camera position
                    Vector3 idealPosition = m_target.position - transform.rotation * Vector3.forward * m_orbitRadius;

                    //apply camera position
                    transform.position = idealPosition;

                    //Adjust camera position to prevent other objects from blocking line of sight to target
                    //casts ray from target to camera to check for objects that would block the camera view
                    RaycastHit hit;
                    Vector3 cameraDirection = transform.position - m_target.position; 
                    //If an object is in the way, adjusts camera position to keep target in view.
                    if(Physics.Raycast(m_target.position, cameraDirection, out hit))
                    {
                        //Calculates distance between camera and the object blocking the target from view.
                        Vector3 adjustedPosition = hit.point - transform.position;
                        float distance = adjustedPosition.magnitude;

                        //Moves camera to nearest point in view of target
                        transform.position = hit.point;
                    }


                    break;
                default:
                    break;
            }

        }

        public CameraType GetCameraType() { return m_cameraType; }

    }
}


//switch (m_cameraType)
//{
//    case CameraType.FIRSTPERSON:
//        break;
//    case CameraType.THIRDPERSONSTATIC:
//        break;
//    case CameraType.THIRDPERSONORBITAL:
//        break;
//    default:
//        break;
//}