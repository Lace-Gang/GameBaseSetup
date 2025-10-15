using UnityEngine;
using UnityEngine.InputSystem; //allows for checking user inputs

namespace GameBase{

    //Required Components
    [RequireComponent(typeof(PlayerController))]
    public class PlayerCharacter : MonoBehaviour, IDataPersistence
    {
        //Hidden Variables
        private int counter = 0;    //Test Code (will be removed later)

        //Hidden Components
        PlayerController m_playerController;  //player controller component
        Rigidbody m_rigidbody; //rigidbody component (if applicable)

        //Player Info
        [Header("Player Information")]
        [SerializeField] private string m_id;   //player unique ID
        //[Tooltip("If Use Physics is true and no rigidbody component is present on the player, a rigidbody will be generated with the values " +
        //    "outlined in the physics section of this script. It is advised to use this generated rigidbody.")]
        //[SerializeField] private bool m_usePhysics; //whether or not the player uses the unity physics engine (i.e. gravity, collisions, etc.)



        //Components
        //[Header("Player Components")]
        //[Tooltip("The CharacterController component responsible for player movement")]
        //[SerializeField] PlayerController m_playerController;
        //[Tooltip("This field is optional. Enter a Rigidbody component if player uses physics and you do not want the +" +
        //    "player's assigned default values to be used.")]
        //[SerializeField] Rigidbody m_rigidbody;



        //physics settings
        [Header("Default Player Physics Settings")]
        [SerializeField] float phy_mass = 1;
        [SerializeField] bool phy_useGravity = true;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ////sets up important components

            ////Player Controller
            //Creates reference to player controller and passes reference to self to controller
            m_playerController = GetComponent<PlayerController>();
            m_playerController.setPlayerReference(this);

            ////Rigidbody
            //Creates rigid body component if the player uses physics and does not already have one
            //Also destroys rigid body component if the player does not use physics and has one (to prevent conflicts)
            //If the player uses physics and a Rigidbody is already present, assigns RigidBody to m_rigidBody
            //if (m_usePhysics && this.GetComponent<Rigidbody>() == null)
            //{
            //    m_rigidbody = this.gameObject.AddComponent<Rigidbody>();
            //
            //    m_rigidbody.mass = phy_mass;
            //    m_rigidbody.useGravity = phy_useGravity;
            //    m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
            //    m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
            //}
            //else if ((!m_usePhysics) && this.GetComponent<Rigidbody>() != null)
            //{
            //    Destroy(this.GetComponent<Rigidbody>());
            //}
            //else if (m_usePhysics && this.GetComponent<Rigidbody>() != null)
            //{
            //    m_rigidbody = this.GetComponent<Rigidbody>();
            //}
        }

        // Update is called once per frame
        void Update()
        {
            //All of the contents of this function (as of right now) are intended for testing purposes only and will later be removed/changed
            //if (Input.GetKeyDown(KeyCode.W))
            //{
            //    this.GetComponent<Transform>().position += new Vector3(0, 0, 1);
            //}

//            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    this.GetComponent<Transform>().position += new Vector3(0, 0, -1);
            //}

//            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    this.GetComponent<Transform>().position += new Vector3(-1, 0, 0);
            //}

//            //if (Input.GetKeyDown(KeyCode.D))
            //{
            //    this.GetComponent<Transform>().position += new Vector3(1, 0, 0);
            //}

            if(Input.GetKeyDown(KeyCode.Q))
            {
                counter++;
                Debug.Log("Count = " + counter);
            }

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