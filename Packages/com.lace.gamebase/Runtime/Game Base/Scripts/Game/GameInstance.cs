using System.Collections;
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
        [SerializeField] GameObject m_playerCharacter;

        [Tooltip("Time between player death event and transition")]
        [SerializeField] float m_deathTransitionTimer = 4f;


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
                    //load level and UI
                    StartCoroutine(LoadTitle());

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
                    //load level and UI
                    StartCoroutine(LoadMainMenu());

                    //transitions to "main menu screen" game state
                    m_gameState = GameState.MAINMENUSCREEN;
                    break;

                case GameState.MAINMENUSCREEN:
                    break;

                case GameState.STARTGAME:
                    //load level and UI 
                    StartCoroutine(LoadGame());

                    //transition to "play game" GameState
                    m_gameState = GameState.PLAYGAME;

                    break;

                case GameState.PLAYGAME:
                    break;

                case GameState.WINGAME:
                    //load level and UI
                    StartCoroutine(LoadWinScreen());

                    //transition to "win screen" GameState
                    m_gameState = GameState.WINSCREEN;
                    break;

                case GameState.LOSEGAME:
                    //load level and UI
                    StartCoroutine(LoadLooseScreen());

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

        #region Load and Unload Scenes

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
        #endregion Load and Unload Scenes



        #region Save and Load Data

        public void LoadData(GameData data)
        {
            //throw new System.NotImplementedException();
        }

        public void SaveData(ref GameData data)
        {
            //throw new System.NotImplementedException();
        }
        #endregion Save and Load Data



        /// <summary>
        /// Updates player health in the UI
        /// </summary>
        /// <param name="currentHealth">Current player character health</param>
        /// <param name="maxHealth">Player character max health</param>
        public void UpdatePlayerHealth(float currentHealth, float maxHealth)
        {
            m_userInterface.UpdateHealthBar(currentHealth, maxHealth);
        }


        /// <summary>
        /// Transitions to next stage of game (afer player death)
        /// </summary>
        /// <returns>Yield return for Coroutine</returns>
        public IEnumerator OnPLayerDeath()
        {
            yield return new WaitForSeconds(m_deathTransitionTimer); //Wait so that death animation can finish
            m_gameState = GameState.LOSEGAME;   //transition to lose game state
        }




        #region Scene Loading

        /// <summary>
        /// Transitions to Title Screen
        /// </summary>
        /// <returns>Yield return for a Coroutine</returns>
        private IEnumerator LoadTitle()
        {
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

            //Fade Screen In
            //m_userInterface.FadeScreen();
            yield return StartCoroutine(m_userInterface.FadeIn());

            //Unlock Cursor and make cursor visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        /// <summary>
        /// Transitions to Main Menu
        /// </summary>
        /// <returns>Yield return for a Coroutine</returns>
        private IEnumerator LoadMainMenu()
        {
            //Fade Screen Out
            yield return StartCoroutine(m_userInterface.FadeOut());


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

            //yield return new WaitForSeconds(1);

            //m_userInterface.FadeScreen();
            yield return StartCoroutine(m_userInterface.FadeIn());

            //Unlock Cursor and make cursor visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        /// <summary>
        /// Transitions to Gameplay
        /// </summary>
        /// <returns>Yield return for a Coroutine</returns>
        private IEnumerator LoadGame()
        {
            //Fade Out
            yield return StartCoroutine (m_userInterface.FadeOut());

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

            //Fade In
            yield return StartCoroutine (m_userInterface.FadeIn());

            //Lock Cursor and make cursor invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        /// <summary>
        /// Transitions to Win Screen
        /// </summary>
        /// <returns>Yield return for a Coroutine</returns>
        private IEnumerator LoadWinScreen()
        {
            //Fade Out
            yield return StartCoroutine(m_userInterface.FadeOut());

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

            //Fade In
            yield return StartCoroutine(m_userInterface.FadeIn());
        }

        /// <summary>
        /// Transitions to Loose Screen
        /// </summary>
        /// <returns>Yield return for a Coroutine</returns>
        private IEnumerator LoadLooseScreen()
        {
            //Fade Out
            yield return StartCoroutine(m_userInterface.FadeOut());

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

            //Fade In
            yield return StartCoroutine(m_userInterface.FadeIn());
        }

        #endregion Scene Loading


    }
}
