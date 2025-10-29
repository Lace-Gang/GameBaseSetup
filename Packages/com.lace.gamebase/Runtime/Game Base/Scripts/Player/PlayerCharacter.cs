using UnityEngine;
using UnityEngine.InputSystem; //allows for checking user inputs

namespace GameBase{

    //Required Components
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent (typeof(Animator))]
    [RequireComponent (typeof(PlayerHealth))]
    public class PlayerCharacter : MonoBehaviour, IDataPersistence, IDamagableInterface
    {
        //Hidden Variables
        private int counter = 0;    //Test Code (will be removed later)

        //Hidden Components
        PlayerController m_playerController;    //player controller component
        Rigidbody m_rigidbody;                  //rigidbody component
        PlayerHealth m_playerHealth;            //health component

        //Player Info
        [Header("General Player Information")]
        [Tooltip("Transform of the PLAYER CHARACTER object.")]
        [SerializeField] Transform m_transform;
        [Tooltip("Unique ID")]
        [SerializeField] string m_id;   //player unique ID
        [Tooltip("Whether the player can receive damage through the Game Base damage system")]
        [SerializeField] bool m_isDamagable = true;

        



        //[Header("Player Model Info")]
        //[Tooltip ("Player Character 3D Model")]
        //[SerializeField] GameObject m_playerModel;
        //[Tooltip("Adjust if alignment between collision and model right/left is incorrect")]
        //[SerializeField] float m_modelXAdjustment = 0f; 
        //[Tooltip("Adjust if alignment between collision and model up/down is incorrect")]
        //[SerializeField] float m_modelYAdjustment = 0f;
        //[Tooltip("Adjust if alignment between collision and model front/back is incorrect")]
        //[SerializeField] float m_modelZAdjustment = 0f;




        public void SetPlayerTransform(Vector3 position, Quaternion rotation)
        {
            m_playerController.GetComponent<CharacterController>().enabled = false;

            m_transform.position = position;
            m_transform.rotation = rotation;

            m_playerController.GetComponent<CharacterController>().enabled = true;
        }



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
            m_playerHealth = GetComponent<PlayerHealth>();

            //Sets important values in references
            m_rigidbody.isKinematic = true; //Rigidbody must be kinematic for character movement to function



            //Set HUD values
            GameInstance.Instance.UpdatePlayerHealth(m_playerHealth.GetHealth(), m_playerHealth.GetMaxHealth());    //Sets health bar
        }



        private void Start()
        {
            //Notifies Game Manager of current health state
            GameInstance.Instance.UpdatePlayerHealth(m_playerHealth.GetHealth(), m_playerHealth.GetMaxHealth());

            ////Set HUD values
            //GameInstance.Instance.UpdatePlayerHealth(m_playerHealth.GetHealth(), m_playerHealth.GetMaxHealth());    //Sets health bar
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
            data.playerPosition = m_transform.position;
            data.playerRotation = m_transform.rotation;
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
                //GetComponent<Transform>().position = data.playerPosition;
                SetPlayerTransform(data.playerPosition, data.playerRotation);

            }
        }



        /// <summary>
        /// Takes damage from a damage source and passes it along to the Player Health component to apply damage to the player. If current health 
        /// is at zero after damage is dealt, executes player "death" state. Otherwise, executes player "hit" state.
        /// </summary>
        /// <param name="damage">Amount of damage being taken</param>
        /// <param name="owner">Owning object of the damage being taken</param>
        public void TakeDamage(float damage, GameObject owner)
        {
            if(!m_isDamagable) return; //only execute damage if the player is set to damagable

            Debug.Log("Damage Dealt: " + damage);               /////Test line. To be removed later
            //Passes damage to health component
            bool isDead = m_playerHealth.AddToHealth(-damage);

            //Notifies Game Manager of health change
            GameInstance.Instance.UpdatePlayerHealth(m_playerHealth.GetHealth(), m_playerHealth.GetMaxHealth());

            //Checks if player health above zero
            //if (m_playerHealth.AddToHealth(-damage))
            if (isDead)
            {
                //If player health equal to zero, executes player death
                OnDeath();
            } else
            {
                //If player health greater than zero, executes player hit state
                m_playerController.OnTakeHit();
            }
            Debug.Log("Player New Health: " + m_playerHealth.GetHealth());              //Test line. To be removed later
        }

        /// <summary>
        /// Passes healing to the Player Health component to heal the player.
        /// </summary>
        /// <param name="amount">Amount of damage to heal</param>
        public void HealDamage(float amount)
        {
            //Passes healing amount to the health component to apply healing
            m_playerHealth.AddToHealth(amount);

            //Notifies Game Manager of health change
            GameInstance.Instance.UpdatePlayerHealth(m_playerHealth.GetHealth(), m_playerHealth.GetMaxHealth());

            Debug.Log("Player New Health: " + m_playerHealth.GetHealth());              //Test line. To be removed later
        }

        /// <summary>
        /// Executes all functions necessary for the player death event
        /// </summary>
        public void OnDeath()
        {
            m_playerController.OnDeath();   //Tells PlayerController to trigger the player death state

            StartCoroutine(GameInstance.Instance.OnPLayerDeath());  //Notify Game Instance of Player Death
        }
    }
}