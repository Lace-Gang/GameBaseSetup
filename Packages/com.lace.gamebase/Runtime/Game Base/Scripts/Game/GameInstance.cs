using UnityEngine;

namespace GameBase
{
    public enum GameState
    {
        TITLE,
        MAINMENU,
        STARTGAME,
        PLAYGAME,
        WINGAME,
        LOSEGAME,
        WINSCREEN,
        LOSESCREEN
    }


    public class GameInstance : MonoBehaviour, IDataPersistence
    {
        //Hidden Variables


        //Exposed Variables
        [SerializeField] GameObject playerCharacter;


        public static GameInstance Instance { get; private set; }  //Allows other scripts to get the singleton instance of the GameInstance


        /// <summary>
        /// Checks that only this instance of the GameInstance exists at this time and notifies the user if this is not true.
        /// Only one instance of the GameInstance should exist at any one time.
        /// </summary>
        private void Awake()
        {
            //Sets up the GameInstance as a singleton
            if (Instance != null)
            {
                Debug.LogError("Found more than one Game Instance in the scene.");
            }
            Instance = this;
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }



        public void LoadData(GameData data)
        {
            //throw new System.NotImplementedException();
        }

        public void SaveData(ref GameData data)
        {
            //throw new System.NotImplementedException();
        }


    }
}
