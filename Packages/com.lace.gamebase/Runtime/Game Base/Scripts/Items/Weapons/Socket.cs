using UnityEngine;

namespace GameBase
{
    public class Socket : MonoBehaviour
    {
        //Exposed Variable
        [Header("Socket Basic Information")]
        [Tooltip("ID of this socket")]
        [SerializeField] protected string socketID = string.Empty;


        public string GetSocketID() {  return socketID; }   //Allows other scripts to see this socket's ID

        /// <summary>
        /// Validates socket. (Socket cannot work without an ID)
        /// </summary>
        private void Start()
        {
            //Warns user if a valid socket ID has not been assigned
            Debug.Assert(socketID != string.Empty, "Weapon Socket has no socketID!");
        }
    }
}
