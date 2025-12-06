using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic; //Allows for use lists
using System.Linq;//Allows for finding data persistence objects

namespace GameBase
{
    public class DataPersistenceManager : MonoBehaviour
    {
        #region Variables
        //Hidden Variables
        private GameData m_gameData;                                //stores all persistent (save) data
        private List<IDataPersistence> m_dataPersistenceObjects;    //all objects with data to save/load
        private FileDataHandler m_dataHandler;                      //used to read/write files as well as serialize/deserialize and encrypt/decrypt data

        //Exposed Variables
        [Header("File Storage Config")]
        [Tooltip("What should the save file be named")]
        [SerializeField] private string m_fileName;                                 //name of the file that persistent (save) data will be stored in
        [Tooltip("Should the save file be encrypted")]
        [SerializeField] private bool m_useEncryption;                              //indicates whether persistent (save) data should be encrypted
        [Tooltip("String used for the encryption process")]
        [SerializeField] private string m_encryptionCodeWord = "DefaultCodeWord";   //codeword used for encryption/decryption

        [Header("Save and Load Conditions")]                
        [Tooltip("NOTE: Not all available save and load conditions can be found here! Only those activated by the DataPersistenceManager!\n" +
            "AT MINIMUM one save condition AND one load condition must be enabled in order for the Save System to work, but those save " +
            "and load conditions DO NOT have to be among the options listed on this component!")]
        [SerializeField] private bool m_saveOnQuit = false;     //indicates whether data should be saved when the game quits

        #endregion Variables


        public static DataPersistenceManager Instance { get; private set; }     //Singleton instance of the DataPersistenceManager

        /// <summary>
        /// Allows other scripts to see the current game data
        /// </summary>
        /// <returns>Current GameData object</returns>
        public GameData GetData() { return m_gameData; }


        #region Awake, Start, and ApplicationQuit

        /// <summary>
        /// Checks that only this instance of the DataPersistenceManager exists at this time and notifies the user if this is not true.
        /// Only one instance of the DataPersistenceManager should exist at any one time.
        /// </summary>
        private void Awake()
        {
            //Sets up the data persistence manager as a singleton
            if(Instance != null)
            {
                Debug.LogError("Found more than one Data Persistence Manager in the scene.");
            }
            Instance = this;
        }

        /// <summary>
        /// Creates FileDataHandler and obtains list of objects with data that needs to persist, and loads the game.
        /// </summary>
        private void Start()
        {
            //Application.persistentDataPath gives the OS standard directory for persisting data in a Unity project. Change
            //this if you want data to be saved elsewhere on your machine, but it's advised to only do so with good reason.
            m_dataHandler = new FileDataHandler(Application.persistentDataPath, m_fileName, m_useEncryption, m_encryptionCodeWord);

            //Find all objects that save through the DataPersistenceManager
            m_dataPersistenceObjects = FindAllDataPersistenceObjects();
            
            //Loads data, this must happen at the start of the game
            LoadGame();
        }

        /// <summary>
        /// Optionally, saves the game.
        /// </summary>
        private void OnApplicationQuit()
        {
            //if user has indicated to save data when game quits, saves data
            if (m_saveOnQuit) SaveGame();
        }

        #endregion Awake, Start, and ApplicationQuit


        #region Save File Management

        /// <summary>
        /// Creates empty save file
        /// </summary>
        public void Reset()
        {
            m_dataHandler.Save(new GameData());
        }

        /// <summary>
        /// Creates new instance of GameData to effectively create a new "save file".
        /// </summary>
        public void NewGame()
        {
            //Create new instance of GameData
            m_gameData = new GameData();
        }

        #endregion Save File Management


        #region Save and Load

        /// <summary>
        /// Promts all persistent data objects to update the GameData object with the proper information, notifies GameData object that
        /// it is no longer a new save, then passes the GameData object to the FileDataHandler to be saved.
        /// </summary>
        public void SaveGame()
        {
            //Searched for all objects that need to be saved (both objects and scenes present may have changed since the last save or load)
            m_dataPersistenceObjects = FindAllDataPersistenceObjects();

            //Pass the data to other scripts so they can update it
            foreach (IDataPersistence dataPersistenceObj in m_dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(ref m_gameData);
            }

            //Save that data to a file using the file data handler
            m_dataHandler.Save(m_gameData);

            //Notify GameInstance that there is a valid save file
            GameInstance.Instance.SetValidSaveFile(true);
        }

        /// <summary>
        /// Promtps the FileDataHandler to load saved data from the file. If no data was found, creates new GameData (save file) and
        /// prompts all persistent data objects to load from the information in the GameData object.
        /// </summary>
        public void LoadGame()
        {
            //Search for all objects that need to be loaded (both objects and scenes present may have changed since the last save or load)
            m_dataPersistenceObjects = FindAllDataPersistenceObjects();

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

        #endregion Save and Load


        /// <summary>
        /// Creates a list of all objects that will be saved through the DataPersistenceManager. Only-
        /// objects of type "Monobehavior" can be found, and by extension, saved.
        /// </summary>
        /// <returns>A list of all Monobehavior objects with the IDataPersitence Interface</returns>
        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<IDataPersistence>();
            return new List<IDataPersistence>(dataPersistenceObjects);
        }
    }
}
