using UnityEngine;
using UnityEngine.InputSystem; //allows for checking user inputs

namespace GameBase{

    //Required Components
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent (typeof(Animator))]
    public class PlayerCharacter : MonoBehaviour, IDataPersistence
    {
        //Hidden Variables
        private int counter = 0;    //Test Code (will be removed later)

        //Hidden Components
        PlayerController m_playerController;    //player controller component
        Rigidbody m_rigidbody;                  //rigidbody component

        //Player Info
        [Header("General Player Information")]
        [Tooltip("Unique ID")]
        [SerializeField] string m_id;   //player unique ID


        //[Header("Player Model Info")]
        //[Tooltip ("Player Character 3D Model")]
        //[SerializeField] GameObject m_playerModel;
        //[Tooltip("Adjust if alignment between collision and model right/left is incorrect")]
        //[SerializeField] float m_modelXAdjustment = 0f; 
        //[Tooltip("Adjust if alignment between collision and model up/down is incorrect")]
        //[SerializeField] float m_modelYAdjustment = 0f;
        //[Tooltip("Adjust if alignment between collision and model front/back is incorrect")]
        //[SerializeField] float m_modelZAdjustment = 0f;



        /// <summary>
        /// Creates important references and sets important data that must be set at awake time
        /// </summary>
        void Awake()
        {
            ////sets up important components

            //Player Model
            //Vector3 modelTransform = new Vector3(transform.position.x + m_modelXAdjustment, transform.position.y + m_modelYAdjustment, transform.position.z + m_modelZAdjustment); //adjusts model position (if necessary)
            //GameObject m_model = GameObject.Instantiate(m_playerModel, modelTransform, transform.rotation); //creates instance of the model
            //m_model.transform.SetParent(transform, true); //so that the model follows the player
            

            //Creates important references
            m_playerController = GetComponent<PlayerController>();
            m_rigidbody = GetComponent<Rigidbody>();

            //Sets important values in references
            m_rigidbody.isKinematic = true; //Rigidbody must be kinematic for character movement to function
        }

        // Update is called once per frame
        //Currently contains only tester and temporary code
        void Update()
        {
            //increments counter for Save System testing
            if(Input.GetKeyDown(KeyCode.Q))
            {
                counter++;
                Debug.Log("Count = " + counter);
            }

            //Temporary code to reset save file
            if(Input.GetKeyDown(KeyCode.R))
            {
                DataPersistenceManager.Instance.ResetOnNextSaveLoad();
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
            //If data is new (this is a new save) do not load data
            if(!data.isNewSave)
            {
                //Updates player data to match save file data
                this.counter = data.deathcount; //Tester line
                GetComponent<Transform>().position = data.playerPosition;
            }
        }
    }
}