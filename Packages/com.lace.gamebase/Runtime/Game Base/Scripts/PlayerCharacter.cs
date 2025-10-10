using UnityEngine;
using UnityEngine.InputSystem;

namespace GameBase{

    public class PlayerCharacter : MonoBehaviour
    {
        //Variables

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //If key down, walk
            if (Input.GetKeyDown(KeyCode.W))
            {
                this.GetComponent<Transform>().position += new Vector3(0, 0, 1);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                this.GetComponent<Transform>().position += new Vector3(0, 0, -1);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                this.GetComponent<Transform>().position += new Vector3(-1, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                this.GetComponent<Transform>().position += new Vector3(1, 0, 0);
            }
        }
    }

}