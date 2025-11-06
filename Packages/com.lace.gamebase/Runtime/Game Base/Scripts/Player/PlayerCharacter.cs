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
        //Hidden Values
        private bool m_alive = true;                //Is the player alive
        private float m_invincibleTime = 0f;        //How long the player will be invincible for (player is invincible as long as timer is greater than 0)
        float m_respawnHealthPercentage = 100f;     //What percentage of health the player will have after respawning (Defaults to full health)

        //Hidden Variables
        private int counter = 0;    //Test Code (will be removed later)
        private int m_lives;        //Number of lives player currently has

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
        [Tooltip("Number of lives player starts with")]
        [SerializeField] int m_baseLives = 1;




        #region Getters and Setters

        /// <summary>
        /// Allows other scripts to know how many lives the player has left
        /// </summary>
        /// <returns>Current number of player lives</returns>
        public int GetLives()
        {
            return m_lives;
        }

        /// <summary>
        /// Sets respawn health percentage
        /// </summary>
        /// <param name="healthPercentage">The percentage of health the player will have after a respawn</param>
        public void SetRespawnHealthType(float healthPercentage)
        {
            //Sets the respawn health percentage 
            m_respawnHealthPercentage = healthPercentage;
        }

        /// <summary>
        /// Adds or reduces current number of player lives.
        /// </summary>
        /// <param name="lives"> Number of lives to add or reduce. Positive to add, negative to reduce.</param>
        public void AddOrReduceLives(int lives)
        {
            m_lives += lives;   //Add/reduce current number of player lives
            GameInstance.Instance.UpdatePlayerLives(m_lives);   //Notifies UI/HUD of the change and updates it
        }

        /// <summary>
        /// Dirrectly sets the player position and rotation, essentially allowing the player to "teleport"
        /// </summary>
        /// <param name="position">Desired player positioin</param>
        /// <param name="rotation">Desired player rotation</param>
        public void SetPlayerTransform(Vector3 position, Quaternion rotation)
        {
            m_playerController.GetComponent<CharacterController>().enabled = false;     //Disables the Character Controller to allow position and rotation overrides

            //Dirrectly set position and rotation of player
            m_transform.position = position;
            m_transform.rotation = rotation;

            m_playerController.GetComponent<CharacterController>().enabled = true;      //Reenables the Character Controller once completed
        }

        #endregion Getters and Setters


        #region Awake, Start, and Update

        /// <summary>
        /// Creates important references and sets important data that must be set at awake time
        /// </summary>
        void Awake()
        {
            ////sets up important components, data, values, etc

            //Creates important references
            m_playerController = GetComponent<PlayerController>();
            m_rigidbody = GetComponent<Rigidbody>();
            m_playerHealth = GetComponent<PlayerHealth>();

            //Sets important values in references
            m_rigidbody.isKinematic = true; //Rigidbody must be kinematic for character movement to function

            //Set Other Values
            m_lives = m_baseLives;


            //Set HUD values
            GameInstance.Instance.UpdatePlayerHealth(m_playerHealth.GetHealth(), m_playerHealth.GetMaxHealth());    //Sets health bar
            GameInstance.Instance.UpdatePlayerLives(m_lives);       //Sets player lives

        }


        /// <summary>
        /// Updates important values on Start, such as current health
        /// </summary>
        private void Start()
        {
            //Notifies Game Manager of current health state
            GameInstance.Instance.UpdatePlayerHealth(m_playerHealth.GetHealth(), m_playerHealth.GetMaxHealth());
        }


        /// <summary>
        ///  Updates timers and other frame by frame checks and data
        /// </summary>
        void Update()
        {
            //increments counter for Save System testing (will be removed soon)
            if(Input.GetKeyDown(KeyCode.Q))
            {
                counter++;
                Debug.Log("Count = " + counter);
            }

            //Temporary code to reset save file (will be removed soon)
            if(Input.GetKeyDown(KeyCode.R))
            {
                DataPersistenceManager.Instance.ResetOnNextSaveLoad();
            }

            ////Updates timers and time based values
            //Invincibility
            if(m_invincibleTime > 0)
            {
                m_invincibleTime -= Time.deltaTime;
            }
        }

        #endregion Awake, Start, and Update


        #region Save and Load

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
            data.playerLives = m_lives;
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
                SetPlayerTransform(data.playerPosition, data.playerRotation);
                m_lives = data.playerLives;


                //Upate HUD to match new values
                GameInstance.Instance.UpdatePlayerLives(m_lives);   //player lives
            }
        }

        #endregion Save and Load

        #region Health, Damage, Death, and Respawning

        /// <summary>
        /// Takes damage from a damage source and passes it along to the Player Health component to apply damage to the player. If current health 
        /// is at zero after damage is dealt, executes player "death" state. Otherwise, executes player "hit" state.
        /// </summary>
        /// <param name="damage">Amount of damage being taken</param>
        /// <param name="owner">Owning object of the damage being taken</param>
        public void TakeDamage(float damage, GameObject owner)
        {
            if(!m_isDamagable || !m_alive || m_invincibleTime > 0) return; //only execute damage if the player is set to damagable, is alive, and is not currently invincible

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
        }

        /// <summary>
        /// Executes all functions necessary for the player death event
        /// </summary>
        public void OnDeath()
        {
            m_alive = false;    //Sets that player is not alive
            m_playerController.OnDeath();   //Tells PlayerController to trigger the player death state

            StartCoroutine(GameInstance.Instance.OnPLayerDeath());  //Notify Game Instance of Player Death
        }

        /// <summary>
        ///Executes all functions necessary from the player end for the player respawn event 
        /// </summary>
        public void OnRespawn(float invinsibilityTime)
        {
            //Sets player invincibility timer
            m_invincibleTime = invinsibilityTime;

            //Notifies Player Controller of the respawn
            m_playerController.OnRespawn();

            //Sets respawn health
            m_playerHealth.SetHealth((m_respawnHealthPercentage / m_playerHealth.GetMaxHealth()) * 100);

            //Updates Health in the UI
            GameInstance.Instance.UpdatePlayerHealth(m_playerHealth.GetHealth(), m_playerHealth.GetMaxHealth());

            //Sets that the player is alive
            m_alive = true;
        }

        #endregion Health, Damage, Death, and Respawning


        #region Upgrades and Powerups

        public void UpgradeHealth(float upgradeAmount, bool healToFull)
        {
            //Adds additional MaxHealth amount to current MaxHealth
            m_playerHealth.SetMaxHealth(m_playerHealth.GetMaxHealth() +  upgradeAmount);

            //If "Heal To Full" is true, sets current HP to Max HP
            if(healToFull) m_playerHealth.SetHealth(m_playerHealth.GetMaxHealth());
        }


        #endregion Upgrades and Powerups


    }
}