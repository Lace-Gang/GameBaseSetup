using UnityEngine;

namespace GameBase
{

    //The DataPersistenceManager is intended to be a Singleton class
    public class DataPersistenceManager : MonoBehaviour
    {
        private GameData m_gameData;

        public static DataPersistenceManager Instance { get; private set; }

        private void Awake()
        {
            //Only one intance of the DataPersistenceManager should ever exist at any given time
            if(Instance != null)
            {
                Debug.LogError("Found more than one Data Persistence Manager in the scene.");
            }
            Instance = this;
        }

        
        public void NewGame()
        {
            //Create new instance of GameData
            this.m_gameData = new GameData();
        }


        public void SaveGame()
        {
            //Pass the data to other scripts so they can update it
                //(TODO)

            //Save that data to a file using the file data handler
                //(TODO)
        }


        public void LoadGame()
        {
            //Load any saved data from a file using the data handler
                //(TODO)

            //If no data can be loaded, initialize to a new game
            if(this.m_gameData == null)
            {
                Debug.Log("No data was found. Initializing to defaults.");
                NewGame();
            }

            //Push the loaded data to all other scripts that need it
                //(TODO)
        }




        //The following are temporary methods intended for testing purposes only
        private void Start()
        {
            LoadGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }
    }
}
