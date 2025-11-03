using UnityEngine;

namespace GameBase
{
    //Health component for Player Character. Allows for more streamlined saving and loading of health data

    [RequireComponent(typeof(PlayerCharacter))]
    public class PlayerHealth : Health, IDataPersistence
    {
        //Hidden Variables
        private PlayerCharacter m_playerCharacter;      //Reference to player character component.          //For use later, but please remove if this is not used later



        /// <summary>
        /// Gets player component and sets current health equal to starting health.
        /// </summary>    
        private void Awake()
        {
            m_playerCharacter = GetComponent<PlayerCharacter>();
            OnAwake(); //calls parent function to execute appropriate commands at awake

        }

        /// <summary>
        /// Directly set player health
        /// </summary>
        /// <param name="health">Desired player health</param>
        public void SetHealth(float health)
        {
            m_health = health;
        }

        #region Save and Load

        /// <summary>
        /// Loads Player Health data
        /// </summary>
        /// <param name="data">Game Data object to load data from</param>
        public void LoadData(GameData data)
        {
            m_health = data.playerHealth;

            //Notify HUD that player health has changed
            GameInstance.Instance.UpdatePlayerHealth(m_health, m_maxHealth);
        }


        /// <summary>
        /// Saves Player Health data
        /// </summary>
        /// <param name="data">Reference to Game Data object to save data to</param>
        public void SaveData(ref GameData data)
        {
            data.playerHealth = m_health;
        }

        #endregion Save and Load
    }
}
