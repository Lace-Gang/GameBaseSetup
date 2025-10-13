using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic; //Allows for use lists
using System.Linq;//Allows for finding data persistence objects

namespace GameBase
{

    //The DataPersistenceManager is intended to be a Singleton class
    public class DataPersistenceManager : MonoBehaviour
    {
        [Header("File Storage Config")]
        [SerializeField] private string m_fileName;                                 //name of the file that persistent (save) data will be stored in
        [SerializeField] private bool m_useEncryption;                              //indicates whether persistent (save) data should be encrypted
        [SerializeField] private string m_encryptionCodeWord = "DefaultCodeWord";   //codeword used for encryption/decryption

        [Header("Save and Load Conditions")]
        
        [Tooltip("NOTE: Not all available save and load conditions can be found here! Only those activated by the DataPersistenceManager!\n" +
            "AT MINIMUM one save condition AND one load condition must be enabled in order for the Save System to work, but those save " +
            "and load conditions DO NOT have to be among the options listed on this component!")]
        [SerializeField] private bool m_loadOnStart;    //indicates whether saved data should be loaded when the game starts
        [Tooltip("NOTE: Not all available save and load conditions can be found here! Only those activated by the DataPersistenceManager!\n" +
            "AT MINIMUM one save condition AND one load condition must be enabled in order for the Save System to work, but those save " +
            "and load conditions DO NOT have to be among the options listed on this component!")]
        [SerializeField] private bool m_saveOnQuit;     //indicates whether data should be saved when the game quits

        private GameData m_gameData;                                //stores all persistent (save) data
        private List<IDataPersistence> m_dataPersistenceObjects;    //all objects with data to save/load
        private FileDataHandler m_dataHandler;                      //used to read/write files as well as serialize/deserialize and encrypt/decrypt data
        private bool m_reset = false;                               //used to indicate when save file is to be discarded

        public static DataPersistenceManager Instance { get; private set; }     //Singleton instance of the DataPersistenceManager

        /// <summary>
        /// Checks that only this instance of the DataPersistenceManager exists at this time and notifies the user if this is not true.
        /// Only one instance of the DataPersistenceManager should exist at any one time.
        /// </summary>
        private void Awake()
        {
            if(Instance != null)
            {
                Debug.LogError("Found more than one Data Persistence Manager in the scene.");
            }
            Instance = this;
        }

        /// <summary>
        /// Creates FileDataHandler and obtains list of objects with data that needs to persist. Optionally, loads the game.
        /// </summary>
        private void Start()
        {
            //Application.persistentDataPath gives the OS standard directory for persisting data in a Unity project. Change
            //this if you want data to be saved elsewhere on your machine, but it's advised to only do so with good reason.
            m_dataHandler = new FileDataHandler(Application.persistentDataPath, m_fileName, m_useEncryption, m_encryptionCodeWord);

            //Find all objects that save through the DataPersistenceManager
            m_dataPersistenceObjects = FindAllDataPersistenceObjects();
            
            //if user has indicated to load data when game starts, loads data
            if(m_loadOnStart) LoadGame();
        }

        /// <summary>
        /// Optionally, saves the game.
        /// </summary>
        private void OnApplicationQuit()
        {
            //if user has indicated to save data when game quits, saves data
            if (m_saveOnQuit) SaveGame();
        }


        /// <summary>
        /// Tells DataPersistenceManager to discard current save file and load a new game the next time the game loads
        /// </summary>
        public void ResetOnNextSaveLoad()
        {
            m_reset = true;
        }



        /// <summary>
        /// Creates new instance of GameData to effectively create a new "save file".
        /// </summary>
        public void NewGame()
        {
            //Create new instance of GameData
            m_gameData = new GameData();
        }


        /// <summary>
        /// Promts all persistent data objects to update the GameData object with the proper information, notifies GameData object that
        /// it is no longer a new save, then passes the GameData object to the FileDataHandler to be saved.
        /// </summary>
        public void SaveGame()
        {
            //Pass the data to other scripts so they can update it
            foreach(IDataPersistence dataPersistenceObj in m_dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(ref m_gameData);
            }

            //If reset is false, sets isNewSave to false to indicate there is now saved data to load. Otherwise sets isNewSave to-
            //true to reset the save file and game the next time the game is loaded.
            m_gameData.isNewSave = m_reset;

            //Save that data to a file using the file data handler
            m_dataHandler.Save(m_gameData);
        }

        /// <summary>
        /// Promtps the FileDataHandler to load saved data from the file. If no data was found, creates new GameData (save file) and
        /// prompts all persistent data objects to load from the information in the GameData object.
        /// </summary>
        public void LoadGame()
        {


            //Load any saved data from a file using the data handler
            m_gameData = m_dataHandler.Load();

            //If no data can be loaded, initialize to a new game
            if(this.m_gameData == null)
            {
                Debug.Log("No data was found. Initializing to defaults.");
                NewGame();
            }

            //Push the loaded data to all other scripts that need it
            foreach(IDataPersistence dataPersistenceObj in this.m_dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(m_gameData);
            }
        }

        /// <summary>
        /// Creates a list of all objects that will be saved through the DataPersistenceManager. Only-
        /// objects of type "Monobehavior" can be found, and by extension, saved.
        /// </summary>
        /// <returns>A list of all Monobehavior objects with the IDataPersitence Interface</returns>
        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>();
            return new List<IDataPersistence>(dataPersistenceObjects);
        }
    }
}
