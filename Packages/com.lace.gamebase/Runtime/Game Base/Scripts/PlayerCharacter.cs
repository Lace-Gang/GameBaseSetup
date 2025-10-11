using UnityEngine;
using UnityEngine.InputSystem; //allows for checking user inputs

namespace GameBase{

    public class PlayerCharacter : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private string m_id;   //player unique ID

        //Variables
        private int counter = 0;    //Test Code (will be removed later)

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //All of the contents of this function (as of right now) are intended for testing purposes only and will later be removed/changed
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

            if(Input.GetKeyDown(KeyCode.Space))
            {
                counter++;
                Debug.Log("Count = " + counter);
            }
        }


        /// <summary>
        /// Updates the persistent data values in the GameData (save file) in order to save the Player Character.
        /// IMPORTANT: Any user defined variables that the user would like to save must be added to GameData.cs AND must be
        /// updated here!
        /// </summary>
        /// <param name="data">Takes in a reference to the GameData object</param>
        public void SaveData(ref GameData data)
        {
            //Updates save file data to match player data
            data.deathcount = this.counter; //Tester line
            data.playerPosition = GetComponent<Transform>().position;
        }

        /// <summary>
        /// Loads the Player Character from the persistent data stored in the GameData object.
        /// IMPORTANT: Any user defined variables that the user would like to save must be added to GameData.cs AND must be
        /// updated here!
        /// </summary>
        /// <param name="data">Takes in the GameData object</param>
        public void LoadData(GameData data)
        {
            //Updates player data to match save file data
            this.counter = data.deathcount; //Tester line
            GetComponent<Transform>().position = data.playerPosition;
        }
    }

}