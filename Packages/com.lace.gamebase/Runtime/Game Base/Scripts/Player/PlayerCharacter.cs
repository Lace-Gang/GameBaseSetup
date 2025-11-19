using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //allows for checking user inputs

namespace GameBase{
    //Required Components
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent (typeof(Animator))]
    [RequireComponent (typeof(Health))]
    public class PlayerCharacter : MonoBehaviour, IDataPersistence, IDamagableInterface
    {
        //Hidden Values
        private bool m_alive = true;                //Is the player alive
        private float m_invincibleTime = 0f;        //How long the player will be invincible for (player is invincible as long as timer is greater than 0)
        float m_respawnHealthPercentage = 100f;     //What percentage of health the player will have after respawning (Defaults to full health)

        //Hidden Variables
        private int m_lives;        //Number of lives player currently has
        private WeaponBase m_weapon;


        //Hidden Lists
        //private List<Socket> m_sockets = new List<Socket>();
        private Socket[] m_sockets;

        //Hidden Components
        PlayerController m_playerController;    //player controller component
        Rigidbody m_rigidbody;                  //rigidbody component
        Health m_playerHealth;                  //health component

        //Player Info
        [Header("General Player Information")]
        [Tooltip("Transform of the PLAYER CHARACTER object.")]
        [SerializeField] Transform m_transform;
        [Tooltip("Unique ID")]
        [SerializeField] string m_ID;   //player unique ID
        [Tooltip("Whether the player can receive damage through the Game Base damage system")]
        [SerializeField] bool m_isDamagable = true;
        [Tooltip("Number of lives player starts with")]
        [SerializeField] int m_baseLives = 1;

        [Header("Player Save Informition")]
        [SerializeField] bool m_saves = false;
        [SerializeField] bool m_savePosition = false;
        [SerializeField] bool m_saveRotation = false;
        [SerializeField] bool m_saveLives = false;
        [SerializeField] bool m_saveCurrentHealth = false;
        [SerializeField] bool m_saveMaxHealth = false;





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
            m_playerHealth = GetComponent<Health>();

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

            m_sockets = GetComponentsInChildren<Socket>();
            //m_sockets = sockets.to
        }


        /// <summary>
        ///  Updates timers and other frame by frame checks and data
        /// </summary>
        void Update()
        {
            //Temporary code to reset save file (will be removed soon)
            if(Input.GetKeyDown(KeyCode.R))
            {
                DataPersistenceManager.Instance.Reset();
            }


            ////Updates timers and time based values
            //Invincibility
            if (m_invincibleTime > 0)
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
            if(!m_saves) return;    //only load if indicated to do so

            //Save Position (if indicated to do so)
            if (m_savePosition)
            {
                //Check vector3Data for key. If key exists, change value to current value, else add key with current value
                if (data.vector3Data.ContainsKey("Player.Position"))
                //if(data.vector3Data.ContainsKey(m_ID + ".Position"))
                {
                    //data.vector3Data[m_ID + ".Position"] = m_transform.position;
                    data.vector3Data["Player.Position"] = m_transform.position;
                }
                else
                {
                    //data.vector3Data.Add(m_ID + ".Position", m_transform.position);
                    data.vector3Data.Add("Player.Position", m_transform.position);
                }
            }

            //Save Rotation (if indicated to do so)
            if (m_saveRotation)
            {
                //Check quaterionData for key. If key exists, change value to current value, else add key with current value
                if (data.quaternionData.ContainsKey("Player.Rotation"))
                //if(data.quaternionData.ContainsKey(m_ID + ".Rotation"))
                {
                    //data.quaternionData[m_ID + ".Rotation"] = m_transform.rotation;
                    data.quaternionData["Player.Rotation"] = m_transform.rotation;
                }
                //else
                else
                {
                    data.quaternionData.Add("Player.Rotation", m_transform.rotation);
                }
            }

            //Save Lives (if indicated to do so)
            if (m_saveLives)
            {
                //Check intData for key. If key exists, change value to current value, else add key with current value
                if (data.intData.ContainsKey("Player.Lives"))
                {
                    data.intData["Player.Lives"] = m_lives;
                }
                else
                {
                    data.intData.Add("Player.Lives", m_lives);
                }
            }

            //Save Current Health (if indicated to do so)
            if (m_saveCurrentHealth)
            {
                //Check floatData for key. If key exists, change value to current value, else add key with current value
                if (data.floatData.ContainsKey("Player.CurrentHealth"))
                {
                    data.floatData["Player.CurrentHealth"] = m_playerHealth.GetHealth();
                }
                else
                {
                    data.floatData.Add("Player.CurrentHealth", m_playerHealth.GetHealth());
                }
            }

            //Save Max Health (if indicated to do so)
            if (m_saveMaxHealth)
            {
                //Check floatData for key. If key exists, change value to current value, else add key with current value
                if (data.floatData.ContainsKey("Player.MaxHealth"))
                {
                    data.floatData["Player.MaxHealth"] = m_playerHealth.GetMaxHealth();
                }
                else
                {
                    data.floatData.Add("Player.MaxHealth", m_playerHealth.GetMaxHealth());
                }
            }
        }


        /// <summary>
        /// Loads the Player Character from the persistent data stored in the GameData object.
        /// IMPORTANT: Any user defined variables that the user would like to save must be added to GameData.cs AND must be
        /// updated here!
        /// </summary>
        /// <param name="data">Takes in the GameData object</param>
        public void LoadData(GameData data)
        {
            if(!m_saves) return;    //only load if indicated to do so

            Vector3 position = m_transform.position;
            Quaternion rotation = m_transform.rotation;
            
            if(m_savePosition)
            {
                if(data.vector3Data.ContainsKey(m_ID + ".Position"))
                {
                    position = data.vector3Data[m_ID + ".Position"];
                }
            }
            
            if(m_saveRotation)
            {
                if(data.quaternionData.ContainsKey(m_ID + ".Rotation"))
                {
                    rotation = data.quaternionData[m_ID + ".Rotation"];
                }
            }

            //Load Lives (if indicated to do so)
            if (m_saveLives)
            {
                //Check intData if key exists, if so load Lives, if not then do nothing
                if (data.intData.ContainsKey("Player.Lives"))
                {
                    m_lives = data.intData["Player.Lives"];
                }
            }

            //Load Max Lives (if indicated to do so)
            if (m_saveMaxHealth)
            {
                //Check floatData if key exists, if so load Max Health, if not then do nothing
                if (data.floatData.ContainsKey("Player.MaxHealth"))
                {
                    m_playerHealth.SetMaxHealth(data.floatData["Player.MaxHealth"]);
                }
            }

            //Load Current Health (if indicated to do so)
            if (m_saveCurrentHealth)
            {
                //Check floatData if key exists, if so load Current Health, if not then do nothing
                if (data.floatData.ContainsKey("Player.CurrentHealth"))
                {
                    m_playerHealth.SetHealth(data.floatData["Player.CurrentHealth"]);
                }
            }



            //Update other necessary things
            if(m_savePosition || m_saveRotation)
            {
                SetPlayerTransform(position, rotation);
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




        public bool EquipWeapon(WeaponBase weapon)
        {
            foreach(Socket socket in m_sockets)
            {
                if(socket.GetSocketID() == weapon.GetSocketName())
                {
                    m_weapon = weapon;
                    m_weapon.transform.parent = socket.transform;
                    m_weapon.transform.position = socket.transform.position;
                    m_weapon.transform.rotation = socket.transform.rotation;
                    //m_weapon.transform.position = Vector3.zero;
                    //m_weapon.transform.rotation = Quaternion.identity;
                    m_weapon.ShowWeapon();
                }
            }


            return false;
        }

    }
}