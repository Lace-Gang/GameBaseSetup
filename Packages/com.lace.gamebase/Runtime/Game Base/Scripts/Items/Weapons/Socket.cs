using UnityEngine;

namespace GameBase
{
    public class Socket : MonoBehaviour
    {
        //Exposed Variable
        [Tooltip("ID of this socket")]
        [SerializeField] string socketID = string.Empty;


        /// <summary>
        /// Validates socket. (Socket cannot work without an ID)
        /// </summary>
        private void Start()
        {
            Debug.Assert(socketID != string.Empty, "Weapon Socket has no socketID!");
        }

        public string GetSocketID() {  return socketID; }   //Allows other scripts to see this socket's ID
    }
}
