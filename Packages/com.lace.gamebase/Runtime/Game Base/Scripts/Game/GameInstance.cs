using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameBase
{
    public enum GameState
    {
        LOADTITLE,
        TITLESCREEN,
        LOADMAINMENU,
        MAINMENUSCREEN,
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
        public GameState m_gameState = GameState.LOADTITLE;     // What State is the game in


        //Exposed Variables
        [SerializeField] UserInterface m_userInterface;
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
            //m_userInterface.m_titleScreen.SetActive(true);
            //LoadScene("UIDisplayScene");
            //m_gameState = GameState.TITLESCREEN;
        }

        // Update is called once per frame
        void Update()
        {
            switch (m_gameState)
            {
                case GameState.LOADTITLE:
                    //turn on title screen
                    m_userInterface.m_titleScreen.SetActive(true);

                    //turn off other UI screens and HUD
                    m_userInterface.m_mainMenuScreen.SetActive(false);
                    m_userInterface.m_HUD.SetActive(false);
                    m_userInterface.m_winScreen.SetActive(false);
                    m_userInterface.m_loseScreen.SetActive(false);
                    m_userInterface.m_pauseScreen.SetActive(false);

                    //loads UIDisplayScene
                    LoadScene("UIDisplayScene");

                    //unloads game screen
                    UnloadScene("samplescene");

                    //Unlock Cursor and make cursor visible
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    //transitions to "title" game state
                    m_gameState = GameState.TITLESCREEN;
                    break;
                case GameState.TITLESCREEN:
                    if(Input.anyKey)
                    {
                        m_gameState = GameState.LOADMAINMENU;
                    }
                    break;
                case GameState.LOADMAINMENU:
                    //turn on main menu screen
                    m_userInterface.m_mainMenuScreen.SetActive(true);

                    //turn off other UI screens and HUD
                    m_userInterface.m_titleScreen.SetActive(false);
                    m_userInterface.m_HUD.SetActive(false);
                    m_userInterface.m_winScreen.SetActive(false);
                    m_userInterface.m_loseScreen.SetActive(false);
                    m_userInterface.m_pauseScreen.SetActive(false);

                    //loads UIDisplayScene
                    LoadScene("UIDisplayScene");

                    //unloads game screen
                    UnloadScene("samplescene");

                    //Unlock Cursor and make cursor visible
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    //transitions to "main menu screen" game state
                    m_gameState = GameState.MAINMENUSCREEN;
                    break;
                case GameState.MAINMENUSCREEN:
                    break;
                case GameState.STARTGAME:
                    //turns off User Interface screens
                    m_userInterface.m_titleScreen.SetActive(false);
                    m_userInterface.m_mainMenuScreen.SetActive(false);
                    m_userInterface.m_winScreen.SetActive(false);
                    m_userInterface.m_loseScreen.SetActive(false);
                    m_userInterface.m_pauseScreen.SetActive(false);

                    //Turns on HUD
                    m_userInterface.m_HUD.SetActive(true);

                    //Loads Game scene
                    LoadScene("SampleScene");

                    //Unloads UIDisplayScene
                    UnloadScene("UIDisplayScene");

                    //Lock Cursor and make cursor invisible
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                    //transition to "play game" GameState
                    m_gameState = GameState.PLAYGAME;

                    break;
                case GameState.PLAYGAME:
                    break;
                case GameState.WINGAME:
                    //turns off other UI screens and HUD
                    m_userInterface.m_titleScreen.SetActive(false);
                    m_userInterface.m_mainMenuScreen.SetActive(false);
                    m_userInterface.m_HUD.SetActive(false);
                    m_userInterface.m_loseScreen.SetActive(false);
                    m_userInterface.m_pauseScreen.SetActive(false);

                    //Turns on win screen
                    m_userInterface.m_winScreen.SetActive(true);

                    //loads UIDisplayScene
                    LoadScene("UIDisplayScene");

                    //unloads Game screen
                    UnloadScene("samplescene");

                    //Unlock Cursor and make cursor visible
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    //transition to "win screen" GameState
                    m_gameState = GameState.WINSCREEN;
                    break;
                case GameState.LOSEGAME:
                    //turns off other UI screens and HUD
                    m_userInterface.m_titleScreen.SetActive(false);
                    m_userInterface.m_mainMenuScreen.SetActive(false);
                    m_userInterface.m_HUD.SetActive(false);
                    m_userInterface.m_winScreen.SetActive(false);
                    m_userInterface.m_pauseScreen.SetActive(false);

                    //Turns on win screen
                    m_userInterface.m_loseScreen.SetActive(true);

                    //loads UIDisplayScene
                    LoadScene("UIDisplayScene");

                    //unloads Game screen
                    UnloadScene("samplescene");

                    //Unlock Cursor and make cursor visible
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    //transition to "lose screen" GameState
                    m_gameState = GameState.LOSESCREEN;
                    break;
                case GameState.WINSCREEN:
                    break;
                case GameState.LOSESCREEN:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Loads a scene asynchronously
        /// </summary>
        /// <param name="sceneName">The name of the scene being loaded (MUST be the exact name of the scene (case insensitve) or the file path name if two scenes of the same name exist)</param>
        public void LoadScene(string sceneName)
        {
            //Only loads scene if scene is not already loaded
            if(!SceneManager.GetSceneByName(sceneName).IsValid())
            {
                //Loads specified scene Async (for better performance) and additive so that the base scene remains present
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
        }

        /// <summary>
        /// Unloads a scene asynchronously
        /// </summary>
        /// <param name="sceneName">The name of the scene being loaded (MUST be the exact name of the scene (case insensitve) or the file path name if two scenes of the same name exist)</param>
        public void UnloadScene(string sceneName)
        {
            //Only unloads scene if scene is already loaded
            if (SceneManager.GetSceneByName(sceneName).IsValid())
            {
                //Unloads specified scene Async (for better performance)
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }



        public void LoadData(GameData data)
        {
            //throw new System.NotImplementedException();
        }

        public void SaveData(ref GameData data)
        {
            //throw new System.NotImplementedException();
        }





        public void UpdatePlayerHealth(float currentHealth, float maxHealth)
        {
            m_userInterface.UpdateHealthBar(currentHealth, maxHealth);
        }


        public void OnPLayerDeath()
        {
            m_gameState = GameState.LOSEGAME;
        }

    }
}
