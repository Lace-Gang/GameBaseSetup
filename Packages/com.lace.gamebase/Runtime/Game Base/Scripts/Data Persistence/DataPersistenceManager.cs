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
        [SerializeField] private string m_fileName;
        [SerializeField] private bool m_useEncryption;

        private GameData m_gameData;
        private List<IDataPersistence> m_dataPersistenceObjects; //all objects being saved/loaded through the DataPersistenceManager
        private FileDataHandler m_dataHandler;

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


        private void Start()
        {
            //Application.persistentDataPath gives the OS standard directory for persisting data in a Unity project. Change
            //this if you want data to be saved elsewhere on your machine, but it's advised to only do so with good reason.
            m_dataHandler = new FileDataHandler(Application.persistentDataPath, m_fileName, m_useEncryption);

            //Find all objects that save through the DataPersistenceManager
            m_dataPersistenceObjects = FindAllDataPersistenceObjects();


            LoadGame(); //Tester line
        }

        private void OnApplicationQuit()
        {
            SaveGame(); //Tester line
        }




        public void NewGame()
        {
            //Create new instance of GameData
            this.m_gameData = new GameData();
        }


        public void SaveGame()
        {
            //Pass the data to other scripts so they can update it
            foreach(IDataPersistence dataPersistenceObj in m_dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(ref m_gameData);
            }

            //Save that data to a file using the file data handler
            m_dataHandler.Save(m_gameData);
        }


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
            //IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
            //IEnumerable<IDataPersistence> IdataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>();
            return new List<IDataPersistence>(dataPersistenceObjects);
        }
    }
}
