using UnityEngine;
using UnityEngine.InputSystem;



//NOTE: This probably won't work in this manner because these scripts would have to be attached to an object to execute.
//These will all have to be functions in the controller
namespace GameBase
{
    public class PlayerAction : MonoBehaviour
    {
        private GameObject playerCharacter;
        public InputAction action;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void performAction()
        {

        }
    }
}
