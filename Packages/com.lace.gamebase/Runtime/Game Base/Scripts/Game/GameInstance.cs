using System.Collections;
using System.Linq;
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
        LOADSAVE,
        PLAYGAME,
        WINGAME,
        LOSEGAME,
        WINSCREEN,
        LOSESCREEN
    }

    public enum RespawnType
    {
        RESPAWNINPLACE,                 //respawns player in place, and enables invincibility frames for a short time
        LOADLASTSAVE,                   //reloads save file, and respawns player in the process
        RESPAWNATSAVELOCATION,          //respawns player at location of last loaded save without reloading game or save
        RESPAWNATSTATICLOCATION
    }

    public enum RespawnHealth
    {
        FULLHEALTH,
        HALFHEALTH
    }

    public enum RestartMode
    {
        RESTARTFROMLASTSAVE,
        RESTARTATBEGINNING
    }


    public class GameInstance : MonoBehaviour, IDataPersistence
    {
        //Hidden Variables
        private bool m_paused = false;                          //Is the game paused
        private bool m_playerAlive = true;                      //Is the player alive
        private bool m_loadOnPlay = false;
        private bool m_restartingGame = false;

        public GameState m_gameState = GameState.LOADTITLE;     //What State is the game in
        private GameObject m_playerCharacter;                   //Player Character
        private PlayerCharacter m_playerScript;                 //Player Script


        //Exposed Variables
        [SerializeField] UserInterface m_userInterface;
        [SerializeField] GameObject m_playerPrefab;

        [Tooltip("After loosing the game, where does the player restart from")]
        [SerializeField] RestartMode m_restartMode = RestartMode.RESTARTATBEGINNING;


        [SerializeField] bool m_gamePauses = true;

        [Header("General Player Info")]
        [Tooltip("Time between player death event and transition")]
        [SerializeField] float m_deathTransitionTimer = 4f;
        [Tooltip("Is the GameInstance responsible for spawning the player? NOTE: Requires a PlayerSpawnPoint to be present in gameplay related scenes!")]
        [SerializeField] bool m_spawnPlayer = true;

        [Header ("Player Respawn")]
        [Tooltip("How should the player respawn")]
        [SerializeField] RespawnType m_respawnType = RespawnType.RESPAWNINPLACE;
        [Tooltip("What should player health be at respawn")]
        [SerializeField] RespawnHealth m_respawnHealthType = RespawnHealth.FULLHEALTH;
        [Tooltip("Time (in seconds) after the player respawns when they cannot be hurt")]
        [SerializeField] float m_respawnInvincibilityTimer = 2f;



        [Header("Save and Load Conditions")]
        [Tooltip("Displays a 'Save Game' option in the pause menu")]
        [SerializeField] bool m_saveFromPauseMenu = true;
        [Tooltip("Displays a 'Load Game' option in the main menu")]
        [SerializeField] bool m_loadFromMainMenu = true;
        [Tooltip("Player can save game by hitting a specific key on their keyboard")]
        [SerializeField] bool m_saveHotKeyEnabled = false;
        [Tooltip("Which key on the keyboard can be used to save the game")]
        [SerializeField] KeyCode m_saveHotKey = KeyCode.J;
        [Tooltip("Player can load game by hitting a specific key on their keyboard")]
        [SerializeField] bool m_loadHotKeyEnabled = false;
        [Tooltip("Which key on the keyboard can be used to load the game")]
        [SerializeField] KeyCode m_loadHotKey = KeyCode.L;


        public static GameInstance Instance { get; private set; }  //Allows other scripts to get the singleton instance of the GameInstance

        public bool getPaused() {  return m_paused; }


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

                    

                    break;

                case GameState.LOADSAVE:
                    //LoadSaveTransition Coroutine CANNOT be started in this section of the switch, or else the coroutine will be started too many times!
                    break;

                case GameState.PLAYGAME:
                    if (m_gamePauses && m_playerAlive && Input.GetKeyDown(KeyCode.X))
                    {
                        if(Time.timeScale > 0)
                        {
                            m_userInterface.m_pauseScreen.SetActive(true);
                            if(m_saveFromPauseMenu) m_userInterface.m_saveButton.SetActive(true);
                            Time.timeScale = 0;

                            //Unlock Cursor and make cursor visible
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                        }
                        //else
                        //{
                        //    m_userInterface.m_pauseScreen.SetActive(false);
                        //    Time.timeScale = 1;
                        //}
                    }

                    //load game if load hot key is enabled and pressed
                    if(m_loadHotKeyEnabled && Input.GetKeyDown(m_loadHotKey))
                    {
                        StartCoroutine(m_userInterface.FadeOut());
                        m_gameState = GameState.LOADSAVE;
                        StartCoroutine(LoadSaveTransition());
                    }

                    //save game if save hot key is enabled and pressed
                    if(m_saveHotKeyEnabled && Input.GetKeyDown(m_saveHotKey))
                    {
                        DataPersistenceManager.Instance.SaveGame();
                    }


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
        /// <returns>Yield return for Coroutine</returns>
        private IEnumerator LoadScene(string sceneName)
        {
            //Only loads scene if scene is not already loaded
            if(!SceneManager.GetSceneByName(sceneName).IsValid())
            {
                //Loads specified scene Async (for better performance) and additive so that the base scene remains present
                yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
        }

        /// <summary>
        /// Unloads a scene asynchronously
        /// </summary>
        /// <param name="sceneName">The name of the scene being loaded (MUST be the exact name of the scene (case insensitve) or the file path name if two scenes of the same name exist)</param>
        /// <returns>Yield return for Coroutine</returns>
        private IEnumerator UnloadScene(string sceneName)
        {
            //Only unloads scene if scene is already loaded
            if (SceneManager.GetSceneByName(sceneName).IsValid())
            {
                //Unloads specified scene Async (for better performance)
                yield return SceneManager.UnloadSceneAsync(sceneName);
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


        public void UnpauseGame()
        {
            m_userInterface.m_pauseScreen.SetActive(false);
            Time.timeScale = 1;

            //Lock Cursor and make cursor invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        #region UI Updates

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
        /// Updates player lives in the UI
        /// </summary>
        /// <param name="lives">Current number of player lives</param>
        public void UpdatePlayerLives(int lives)
        {
            m_userInterface.UpdatePlayerLives(lives);
        }

        #endregion UI Updates



        public void SetLoadOnPlay(bool loadSaveFile)
        {
            m_loadOnPlay = (loadSaveFile) ? true : false;
        }



        /// <summary>
        /// Transitions to next stage of game (afer player death)
        /// </summary>
        /// <returns>Yield return for Coroutine</returns>
        public IEnumerator OnPLayerDeath()
        {
            m_playerAlive = false;  //Indicate player is no longer alive
            yield return new WaitForSeconds(m_deathTransitionTimer); //Wait so that death animation can finish

            //Account for player lives

            if(m_playerScript.GetLives() > 1)
            {
                //Respawns player
                StartCoroutine(OnPlayerRespawn());
            }
            else
            {
                m_gameState = GameState.LOSEGAME;   //transition to lose game state
            }
        }

        public IEnumerator OnPlayerRespawn()
        {
            switch (m_respawnType)
            {
                case RespawnType.RESPAWNINPLACE:
                    m_playerScript.OnRespawn(m_respawnInvincibilityTimer);
                    m_playerAlive = true;
                    break;

                case RespawnType.LOADLASTSAVE:
                    yield return StartCoroutine(m_userInterface.FadeOut());
                    StartCoroutine(LoadSaveTransition());
                    m_gameState = GameState.LOADSAVE;
                    m_playerScript.OnRespawn(m_respawnInvincibilityTimer);
                    break;

                case RespawnType.RESPAWNATSAVELOCATION:
                    //Respawns player character at location of last loaded save

                    PlayerSpawnPoint spawnPoint = FindFirstObjectByType<PlayerSpawnPoint>();
                    if (spawnPoint != null)
                    {
                        m_playerScript.SetPlayerTransform(spawnPoint.transform.position, spawnPoint.transform.rotation);
                        m_playerScript.OnRespawn(m_respawnInvincibilityTimer);
                        m_playerAlive = true;
                    }
                    else
                    {
                        Debug.LogError("No PlayerSpawnPoint was located in the scene when respawning! Player cannot respawn!");
                    }

                    break;

                case RespawnType.RESPAWNATSTATICLOCATION:
                    //Respawns player at static location
                    bool spawnPointFound = false;
                    StaticSpawnPoint[] spawns = FindObjectsByType<StaticSpawnPoint>(FindObjectsSortMode.None);

                    foreach (StaticSpawnPoint spawn in spawns)
                    {
                        if (spawn.spawnTag == "Player")
                        {
                            spawnPointFound = true;
                            m_playerScript.SetPlayerTransform(spawn.transform.position, spawn.transform.rotation);
                            m_playerScript.OnRespawn(m_respawnInvincibilityTimer);
                            m_playerAlive = true;

                            break;
                        }
                    }

                    //Notify user if static spawn point configued for player has not been found
                    if (!spawnPointFound) Debug.LogError("No StaticSpawnPoint was located in the scene with tag 'Player'! Player cannot respawn!");

                    break;

                default:
                    break;
            }

            m_playerScript.AddOrReduceLives(-1);    //Reduce player lives by one

        }



        public void RestartFromScreen()
        {
            switch (m_restartMode)
            {
                case RestartMode.RESTARTFROMLASTSAVE:
                    m_loadOnPlay = true;
                    m_gameState = GameState.STARTGAME;
                    break;

                case RestartMode.RESTARTATBEGINNING:
                    m_loadOnPlay = false;
                    m_gameState = GameState.STARTGAME;
                    break;

                default:
                    break;
            }
        }

        public void RestartFromGame()
        {
            m_restartingGame = true;
            m_loadOnPlay = false;
            m_gameState = GameState.STARTGAME;
        }



        #region State Transitioning

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
            yield return StartCoroutine(LoadScene("UIDisplayScene"));

            //unloads game screen
            StartCoroutine(UnloadScene("samplescene"));

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

            if(m_loadFromMainMenu)
            {
                m_userInterface.m_loadButton.SetActive(true);
            }

            //turn off other UI screens and HUD
            m_userInterface.m_titleScreen.SetActive(false);
            m_userInterface.m_HUD.SetActive(false);
            m_userInterface.m_winScreen.SetActive(false);
            m_userInterface.m_loseScreen.SetActive(false);
            m_userInterface.m_pauseScreen.SetActive(false);

            //loads UIDisplayScene
            yield return StartCoroutine(LoadScene("UIDisplayScene"));

            //unloads game screen
            StartCoroutine(UnloadScene("samplescene"));

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

            //Restarts Game if applicable
            if(m_restartingGame)
            {
                yield return StartCoroutine(UnloadScene("SampleScene"));
                m_restartingGame = false;
            }

            //Loads Game scene
            yield return StartCoroutine(LoadScene("SampleScene"));

            //Unloads UIDisplayScene
            StartCoroutine(UnloadScene("UIDisplayScene"));


            //Spawns player character
            if(m_spawnPlayer)
            {
                PlayerSpawnPoint spawnPoint = FindFirstObjectByType<PlayerSpawnPoint>();
                if(spawnPoint != null)
                {
                    //if (m_playerCharacter == null)
                    if (m_playerCharacter != null)
                    {
                        GameObject.Destroy(m_playerCharacter);
                        m_playerScript = null;
                    }
                    m_playerCharacter = GameObject.Instantiate(m_playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    m_playerScript = m_playerCharacter.GetComponentInChildren<PlayerCharacter>();
                    m_playerScript.SetRespawnHealthType(m_respawnHealthType);
                    m_playerCharacter.SetActive(true);
                } 
                else
                {
                    Debug.LogError("No PlayerSpawnPoint was located in the scene! Player will not be spawned.");
                }
            }
            


            //Lock Cursor and make cursor invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;


            if (m_loadOnPlay)
            {
                m_gameState = GameState.LOADSAVE;
                StartCoroutine(LoadSaveTransition());
            }
            else
            {
                //transition to "play game" GameState
                m_gameState = GameState.PLAYGAME;

                //Fade In
                yield return StartCoroutine (m_userInterface.FadeIn());
            }


            //Indicate player is alive and unpause game
            m_playerAlive = true;
            UnpauseGame();
        }

        private IEnumerator LoadSaveTransition()
        {
            //yield return StartCoroutine(m_userInterface.FadeOut());
            DataPersistenceManager.Instance.LoadGame();

            //Spawns player character
            if (m_spawnPlayer)
            {
                PlayerSpawnPoint spawnPoint = FindFirstObjectByType<PlayerSpawnPoint>();
                if (spawnPoint != null)
                {
                    //m_playerCharacter.GetComponentInChildren<PlayerCharacter>().SetPlayerTransform(spawnPoint.transform.position, spawnPoint.transform.rotation);
                    m_playerScript.SetPlayerTransform(spawnPoint.transform.position, spawnPoint.transform.rotation);
                }
                else
                {
                    Debug.LogError("No PlayerSpawnPoint was located in the scene when loading! Player will not be moved correctly!");
                }
            }

            yield return StartCoroutine(m_userInterface.FadeIn());

            m_playerAlive = true;
            m_gameState = GameState.PLAYGAME;
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
            yield return StartCoroutine(LoadScene("UIDisplayScene"));

            //unloads Game screen
            StartCoroutine(UnloadScene("samplescene"));

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
            yield return StartCoroutine(LoadScene("UIDisplayScene"));

            //unloads Game screen
            StartCoroutine(UnloadScene("samplescene"));

            //Unlock Cursor and make cursor visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //Fade In
            yield return StartCoroutine(m_userInterface.FadeIn());
        }

        #endregion State Transitioning


    }
}
