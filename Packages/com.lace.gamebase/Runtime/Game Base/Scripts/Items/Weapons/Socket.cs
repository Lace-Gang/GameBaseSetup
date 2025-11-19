using UnityEngine;

namespace GameBase
{
    public class Socket : MonoBehaviour
    {
        //Hidden Variable


        //Exposed Variable
        [SerializeField] string socketID = string.Empty;



        private void Start()
        {
            Debug.Assert(socketID != string.Empty, "Weapon Socket has no socketID!");
        }

        public string GetSocketID() {  return socketID; }
    }
}
