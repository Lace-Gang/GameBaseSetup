using UnityEngine;

namespace GameBase
{
    //Health component for Player Character. Allows for more streamlined saving and loading of health data

    [RequireComponent(typeof(PlayerCharacter))]
    public class PlayerHealth : Health, IDataPersistence
    {
        //Hidden Variables
        private PlayerCharacter m_playerCharacter;      //Reference to player character component.          //For use later, but please remove if this is not used later




        private void Awake()
        {
            m_playerCharacter = GetComponent<PlayerCharacter>();
        }


        /// <summary>
        /// Sets current health equal to starting health.
        /// </summary>       
        void Start()
        {
            OnStart(); //calls parent function to execute appropriate commands at start
        }


        /// <summary>
        /// Loads Player Health data
        /// </summary>
        /// <param name="data">Game Data object to load data from</param>
        public void LoadData(GameData data)
        {
            //throw new System.NotImplementedException();

            //TO DO: Load data if data is supposed to be saved and loaded
        }


        /// <summary>
        /// Saves Player Health data
        /// </summary>
        /// <param name="data">Reference to Game Data object to save data to</param>
        public void SaveData(ref GameData data)
        {
            //throw new System.NotImplementedException();

            //TO DO: Save data if data is supposed to be saved and loaded
        }

    }
}
