using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameBase
{
    public class GameInstance : MonoBehaviour, IDataPersistence
    {
        //Hidden Variables
        private bool m_paused = false;                          //Is the game paused
        private bool m_playerAlive = true;                      //Is the player alive
        private bool m_validSaveFile = false;
        private bool m_loadOnPlay = false;                      //Should save file load when game loads
        private bool m_restartingGame = false;                  //Has player indicated from pause menu to restart since the last update

        private bool m_saveHasAlreadyLoaded = false;            //Tracks if the save file has been loaded so that it isn't loaded repeatedly

        private GameObject m_playerCharacter;                   //Reference to the Player Character
        private PlayerCharacter m_playerScript;                 //Reference to the Player Character Script
        private float m_score;                                  //Current score

        private List<IPrompter> m_activePrompters = new List<IPrompter>();
        private IPrompter m_currentDisplayPrompter;


        //Exposed Variables
        [Header("Critical Information and References")]
        [Tooltip("What Game State the game will be in when the game is first opened")]
        [SerializeField] public  GameState m_gameState = GameState.LOADTITLE;     //What State is the game in
        [Tooltip("Reference to the 'Player' prefab")]
        [SerializeField] GameObject m_playerPrefab;
        [Tooltip("Name of the Scene where the game will execute")]
        [SerializeField] string m_gameSceneName = "SampleScene";    //Name of the game scene. Used to load game scene

        [Header("Game Flow")]
        [Tooltip("After loosing the game, where does the player restart from")]
        [SerializeField] RestartMode m_restartMode = RestartMode.RESTARTFROMBEGINNING;
        [Tooltip("Can the game be paused")]
        [SerializeField] bool m_gamePauses = true;
        [Tooltip("Which key on the keyboard can be used to open the pause menu")]
        [SerializeField] KeyCode m_pauseKey = KeyCode.X;

        [Header("General Player Info")]
        [Tooltip("Time between player death event and transition")]
        [SerializeField] float m_deathTransitionTimer = 4f;

        [Header ("Player Respawn")]
        [Tooltip("How should the player respawn")]
        [SerializeField] RespawnType m_respawnType = RespawnType.RESPAWNINPLACE;
        [Tooltip("What should player health be at respawn")]
        [SerializeField] float m_respawnHealthPercentage = 100;
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


        #region Getters and Setters

        public static GameInstance Instance { get; private set; }  //Allows other scripts to get the singleton instance of the GameInstance

        public bool getPaused() {  return m_paused; }   //Allows other scripts to know if the game is currently paused

        public PlayerCharacter GetPlayerScript() { return m_playerScript; }  //Allows other scripts to access the current player character script

        /// <summary>
        /// Sets whether the save file should load next time the game loads
        /// </summary>
        /// <param name="loadSaveFile">Should save file load next time game loads</param>
        public void SetLoadOnPlay(bool loadSaveFile)
        {
            m_loadOnPlay = (loadSaveFile) ? true : false;
        }

        /// <summary>
        /// Set if there is a valid save file (for if/when that changes)
        /// </summary>
        /// <param name="validSaveFile"></param>
        public void SetValidSaveFile(bool validSaveFile)
        {
            m_validSaveFile = validSaveFile;
        }



        #endregion Getters and Setters



        #region Awake and Update

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


        /// <summary>
        /// Executes Game Cycle
        /// </summary>
        void Update()
        {
            //Behavior depended on which state the game is currently in
            switch (m_gameState)
            {
                case GameState.LOADTITLE:
                    //load level and UI
                    StartCoroutine(LoadTitle());

                    //transitions to "title" game state
                    m_gameState = GameState.TITLESCREEN;
                    break;

                case GameState.TITLESCREEN:
                    //Transitions to main menu if any key is pressed
                    if(Input.anyKey)
                    {
                        m_gameState = GameState.LOADMAINMENU;   //transition to "load main menu" state
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
                    //load level and HUD, spawns player (if applicable), and optionally loads from save file
                    StartCoroutine(LoadGame());

                    break;

                case GameState.LOADSAVE:
                    //LoadSaveTransition Coroutine CANNOT be started in this section of the switch, or else the coroutine will be started too many times!
                    break;

                case GameState.PLAYGAME:
                    //Pauses game if game is supposed to pause, and the player character is alive, and the player hits the pause key
                    if (m_gamePauses && m_playerAlive && Input.GetKeyDown(m_pauseKey))
                    {
                        UserInterface.Instance.m_saveButton.SetActive(m_saveFromPauseMenu);    //Only display Save Button if save button is supposed to be visible in the pause menu
                        OpenPauseMenu();
                    }

                    //Update prompt display, evaluate if prompt is being triggered, and execute prompt if so
                    PromptUpdate();

                    //load game if load hot key is enabled and pressed and there is a valid save file to load
                    if(m_loadHotKeyEnabled && Input.GetKeyDown(m_loadHotKey) && m_validSaveFile)
                    {
                        StartCoroutine(UserInterface.Instance.FadeOut());  //fade out screen for more visually smooth transition
                        m_gameState = GameState.LOADSAVE;
                        StartCoroutine(LoadSaveTransition());   //Load save file
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

        #endregion Awake and Update



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

        /// <summary>
        /// Loads data from the save file
        /// </summary>
        /// <param name="data">GameData object that the loaded save file information</param>
        public void LoadData(GameData data)
        {
            //load data
            m_validSaveFile = !data.isNewSave;
            m_score = data.score;

            //update UI
            UserInterface.Instance.UpdateScore(m_score);
        }

        /// <summary>
        /// Saves data to the save file
        /// </summary>
        /// <param name="data">GameData object that information is being saved to</param>
        public void SaveData(ref GameData data)
        {
            data.score = m_score;
        }
        #endregion Save and Load Data



        #region Pause and Unpause

        /// <summary>
        /// Pauses game and opens pause menu
        /// </summary>
        private void OpenPauseMenu()
        {
            //Display pause menu
            UserInterface.Instance.m_pauseScreen.SetActive(true);
            if (m_saveFromPauseMenu) UserInterface.Instance.m_saveButton.SetActive(true);

            //pause game
            PauseGame();
        }

        /// <summary>
        /// Pauses game but does not open pause menu
        /// </summary>
        private void PauseGame()
        {
            //Pause Game
            Time.timeScale = 0;

            //Unlock Cursor and make cursor visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        /// <summary>
        /// Unpauses game and exits pause menu
        /// </summary>
        public void UnpauseGame()
        {
            //Exits pause menu
            UserInterface.Instance.m_pauseScreen.SetActive(false);

            //Unpause game
            Time.timeScale = 1;     

            //Lock Cursor and make cursor invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        #endregion Pause and Unpause



        #region UI Updates

        /// <summary>
        /// Updates player health in the UI
        /// </summary>
        /// <param name="currentHealth">Current player character health</param>
        /// <param name="maxHealth">Player character max health</param>
        public void UpdatePlayerHealth(float currentHealth, float maxHealth)
        {
            UserInterface.Instance.UpdateHealthBar(currentHealth, maxHealth);
        }

        /// <summary>
        /// Updates player lives in the UI
        /// </summary>
        /// <param name="lives">Current number of player lives</param>
        public void UpdatePlayerLives(int lives)
        {
            UserInterface.Instance.UpdatePlayerLives(lives);
        }


        /// <summary>
        /// Adds an prompter to the list of active prompters
        /// </summary>
        /// <param name="prompter">Prompter being added</param>
        public void AddToActivePrompts(IPrompter prompter)
        {
            m_activePrompters.Add(prompter);
        }

        /// <summary>
        /// Removes a prompter from the list of active prompters
        /// </summary>
        /// <param name="prompter">Prompter being removed</param>
        public void RemoveFromActivePrompts(IPrompter prompter)
        {
            m_activePrompters.Remove(prompter);
        }


        /// <summary>
        /// Displays highest priority prompt to the screen and checks if the prompt has been interacted with
        /// </summary>
        private void PromptUpdate()
        {
            //Hide the prompt box from the user interface if there are no prompts to display
            if(m_activePrompters.Count == 0)
            {
                UserInterface.Instance.HidePromptBox();
                return;
            }

            //find the prompt with the highest priority
            IPrompter highestPriority = m_activePrompters[0];
            foreach(IPrompter prompter in m_activePrompters)
            {
                if(prompter.GetPromptPriority() > highestPriority.GetPromptPriority()) highestPriority = prompter;
            }

            //display highest priotiry prompt
            UserInterface.Instance.DisplayPromptBox(highestPriority.GetPrompt());

            //Check if prompt is being interacted with, and notify prompter if so
            if(Input.GetKeyDown(highestPriority.GetPromptInteractionKey())) highestPriority.ExecutePrompt();
        }


        #endregion UI Updates


        #region Score


        /// <summary>
        /// Changes the score. Positive inputs will add to the score, negative inputs will detract from it.
        /// </summary>
        /// <param name="score">How much to add to the score. Negative integers will subtract.</param>
        public void AddOrRemoveScore(float score)
        {
            //Add to or subtract from score and clamp score (score cannot be negative)
            m_score = (m_score + score > 0)? m_score + score : 0;


            //Notify UI of the change
            UserInterface.Instance.UpdateScore(m_score);
        }

        #endregion Score



        #region Player Death, Respawn and Restart

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

        /// <summary>
        /// Respawns Player
        /// </summary>
        /// <returns>Yield return for coroutine</returns>
        public IEnumerator OnPlayerRespawn()
        {
            //Behavior dependent on respawn type
            switch (m_respawnType)
            {
                case RespawnType.RESPAWNINPLACE:
                    //Respawns player character in the same location as where they died
                    m_playerScript.OnRespawn(m_respawnInvincibilityTimer);
                    m_playerAlive = true;
                    break;

                case RespawnType.LOADLASTSAVE:
                    //Respawns player by loading the save file. This will, however, still decrease the number of lives the player has.
                    yield return StartCoroutine(UserInterface.Instance.FadeOut());
                    StartCoroutine(LoadSaveTransition());
                    m_gameState = GameState.LOADSAVE;
                    m_playerScript.OnRespawn(m_respawnInvincibilityTimer);
                    break;

                case RespawnType.RESPAWNATSAVELOCATION:
                    //Respawns player character at location of last loaded save, without loading any other data from that save.

                    //Find player spawn point, and relocate player character to that point.
                    PlayerSpawnPoint spawnPoint = FindFirstObjectByType<PlayerSpawnPoint>();
                    if (spawnPoint != null)
                    {
                        m_playerScript.SetPlayerTransform(spawnPoint.transform.position, spawnPoint.transform.rotation);
                        m_playerScript.OnRespawn(m_respawnInvincibilityTimer);
                        m_playerAlive = true;
                    }
                    else
                    {
                        //Notify user if there is no player spawn point at which to spawn the player
                        Debug.LogError("No PlayerSpawnPoint was located in the scene when respawning! Player cannot respawn!");
                    }

                    break;

                case RespawnType.RESPAWNATSTATICLOCATION:
                    //Respawns player at a static location
                    bool spawnPointFound = false;
                    StaticSpawnPoint[] spawns = FindObjectsByType<StaticSpawnPoint>(FindObjectsSortMode.None); //find all static spawn points in the loaded scene

                    //search for the first static spawn point configured for the player
                    foreach (StaticSpawnPoint spawn in spawns)
                    {
                        if (spawn.spawnTag == "Player")
                        {
                            //respawn player at that static spawn point
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

            m_playerScript.AddOrReduceLives(-1);    //Reduce player lives by one (executed here so that the "load last save" respawn type will still evaluate number of lives left correctly
        }


        /// <summary>
        /// Restarts game. Intended for use from the LoseScreen of the UI
        /// </summary>
        public void RestartFromScreen()
        {
            //Behavior varies depending on Restart Mode
            switch (m_restartMode)
            {
                case RestartMode.RESTARTFROMLASTSAVE:
                    //Restarts game from the last save by loading the last save
                    m_loadOnPlay = true;
                    m_gameState = GameState.STARTGAME;
                    break;

                case RestartMode.RESTARTFROMBEGINNING:
                    //Restarts game from the beginning without loading any saved data
                    m_loadOnPlay = false;
                    m_gameState = GameState.STARTGAME;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Restarts game. Intended for use from the PauseMenu of the UI
        /// </summary>
        public void RestartFromGame()
        {
            //Reloads game scene, and restarts game from the beginning.
            //Effectively does the same thing as starting a "New Game" from the main menu.
            m_restartingGame = true;
            m_loadOnPlay = false;
            m_gameState = GameState.STARTGAME;
        }


        #endregion Player Death, Respawn and Restart



        #region State Transitioning

        /// <summary>
        /// Transitions to Title Screen
        /// </summary>
        /// <returns>Yield return for a Coroutine</returns>
        private IEnumerator LoadTitle()
        {
            //turn on title screen
            UserInterface.Instance.m_titleScreen.SetActive(true);

            //turn off other UI screens and HUD
            UserInterface.Instance.m_mainMenuScreen.SetActive(false);
            UserInterface.Instance.m_HUD.SetActive(false);
            UserInterface.Instance.m_winScreen.SetActive(false);
            UserInterface.Instance.m_loseScreen.SetActive(false);
            UserInterface.Instance.m_pauseScreen.SetActive(false);

            //loads UIDisplayScene
            yield return StartCoroutine(LoadScene("UIDisplayScene"));

            //unloads game screen
            StartCoroutine(UnloadScene(m_gameSceneName));

            //Fade Screen In
            //m_userInterface.FadeScreen();
            yield return StartCoroutine(UserInterface.Instance.FadeIn());

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
            yield return StartCoroutine(UserInterface.Instance.FadeOut());


            //turn on main menu screen
            UserInterface.Instance.m_mainMenuScreen.SetActive(true);

            //Only displays "Load" button if it is indicated that that button should be present
            if(m_loadFromMainMenu)
            {
                UserInterface.Instance.m_loadButtonObject.SetActive(true);

                //If there is no valid save to load, makes button non-interactable
                if (!m_validSaveFile) UserInterface.Instance.m_loadButton.interactable = false;
                else UserInterface.Instance.m_loadButton.interactable = true;
            }

            //turn off other UI screens and HUD
            UserInterface.Instance.m_titleScreen.SetActive(false);
            UserInterface.Instance.m_HUD.SetActive(false);
            UserInterface.Instance.m_winScreen.SetActive(false);
            UserInterface.Instance.m_loseScreen.SetActive(false);
            UserInterface.Instance.m_pauseScreen.SetActive(false);

            //loads UIDisplayScene
            yield return StartCoroutine(LoadScene("UIDisplayScene"));

            //unloads game screen
            StartCoroutine(UnloadScene(m_gameSceneName));

            //yield return new WaitForSeconds(1);

            //m_userInterface.FadeScreen();
            yield return StartCoroutine(UserInterface.Instance.FadeIn());

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
            yield return StartCoroutine (UserInterface.Instance.FadeOut());

            //turns off User Interface screens
            UserInterface.Instance.m_titleScreen.SetActive(false);
            UserInterface.Instance.m_mainMenuScreen.SetActive(false);
            UserInterface.Instance.m_winScreen.SetActive(false);
            UserInterface.Instance.m_loseScreen.SetActive(false);
            UserInterface.Instance.m_pauseScreen.SetActive(false);

            //Turns on HUD
            UserInterface.Instance.m_HUD.SetActive(true);

            //Restarts Game if applicable
            if(m_restartingGame)
            {
                yield return StartCoroutine(UnloadScene(m_gameSceneName));
                m_restartingGame = false;
            }

            //Loads Game scene
            yield return StartCoroutine(LoadScene(m_gameSceneName));

            //Unloads UIDisplayScene
            StartCoroutine(UnloadScene("UIDisplayScene"));


            //Spawns player character
            PlayerSpawnPoint spawnPoint = FindFirstObjectByType<PlayerSpawnPoint>();
            if(spawnPoint != null)
            {
                //Destroy previous Player if such a Player exists
                if (m_playerCharacter != null)
                {
                    GameObject.Destroy(m_playerCharacter);
                    m_playerScript = null;
                }
                m_playerCharacter = GameObject.Instantiate(m_playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);   //Create player
                m_playerScript = m_playerCharacter.GetComponentInChildren<PlayerCharacter>();   //Get reference to PlayerCharacter component
                m_playerScript.SetRespawnHealthType(m_respawnHealthPercentage);   //Set health percentage on respawn
                m_playerCharacter.SetActive(true);      //Activate Player
            } 
            else
            {
                //Notify user if there is no spawn point. Without a spawn point, a player cannot be spawned.
                Debug.LogError("No PlayerSpawnPoint was located in the scene! Player will not be spawned.");
            }

            //Resets Score
            m_score = 0;
            UserInterface.Instance.UpdateScore(m_score);

            //Lock Cursor and make cursor invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            //Load save file if applicable. If not, transition to "Play Game" game state
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
                yield return StartCoroutine (UserInterface.Instance.FadeIn());
            }


            //Indicate player is alive and unpause game (in the event the game was paused)
            m_playerAlive = true;
            UnpauseGame();
        }

        /// <summary>
        /// Transitions from a new game to a game loaded from a save file
        /// </summary>
        /// <returns>Yield return for coroutine</returns>
        private IEnumerator LoadSaveTransition()
        {
            //Can't load a save file that doesn't exist, and we don't want to load everytime the
            //coroutine executes. Only the first time after it is started
            if (m_validSaveFile && !m_saveHasAlreadyLoaded) 
            {
                //Loads game from Data Manager
                DataPersistenceManager.Instance.LoadGame();


                //Once game is loaded, moves player to the current transform of the Player Spawn Point
                PlayerSpawnPoint spawnPoint = FindFirstObjectByType<PlayerSpawnPoint>();
                if (spawnPoint != null)
                {
                    m_playerScript.SetPlayerTransform(spawnPoint.transform.position, spawnPoint.transform.rotation);
                }
                else
                {
                    //Notifies user if there is no Player Spawn Point in the scene after loading the data
                    Debug.LogError("No PlayerSpawnPoint was located in the scene when loading! Player will not be moved correctly!");
                }

                m_saveHasAlreadyLoaded = true;  //prevents this code from executing more than once while the coroutine executes
            }

            //Fade in
            yield return StartCoroutine(UserInterface.Instance.FadeIn());

           
            //With the coroutine finished executing, set this back to false so
            //that the coroutine executes all code the next time it is started
            m_saveHasAlreadyLoaded = false;   
            
            m_playerAlive = true;   //Set player to alive
            m_gameState = GameState.PLAYGAME;   //Transition to "Play Game" game state
        }

        /// <summary>
        /// Transitions to Win Screen
        /// </summary>
        /// <returns>Yield return for a Coroutine</returns>
        private IEnumerator LoadWinScreen()
        {
            //Fade Out
            yield return StartCoroutine(UserInterface.Instance.FadeOut());

            //turns off other UI screens and HUD
            UserInterface.Instance.m_titleScreen.SetActive(false);
            UserInterface.Instance.m_mainMenuScreen.SetActive(false);
            UserInterface.Instance.m_HUD.SetActive(false);
            UserInterface.Instance.m_loseScreen.SetActive(false);
            UserInterface.Instance.m_pauseScreen.SetActive(false);

            //Turns on win screen
            UserInterface.Instance.m_winScreen.SetActive(true);

            //loads UIDisplayScene
            yield return StartCoroutine(LoadScene("UIDisplayScene"));

            //unloads Game screen
            StartCoroutine(UnloadScene(m_gameSceneName));

            //Unlock Cursor and make cursor visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //Fade In
            yield return StartCoroutine(UserInterface.Instance.FadeIn());
        }

        /// <summary>
        /// Transitions to Loose Screen
        /// </summary>
        /// <returns>Yield return for a Coroutine</returns>
        private IEnumerator LoadLooseScreen()
        {
            //Fade Out
            yield return StartCoroutine(UserInterface.Instance.FadeOut());

            //turns off other UI screens and HUD
            UserInterface.Instance.m_titleScreen.SetActive(false);
            UserInterface.Instance.m_mainMenuScreen.SetActive(false);
            UserInterface.Instance.m_HUD.SetActive(false);
            UserInterface.Instance.m_winScreen.SetActive(false);
            UserInterface.Instance.m_pauseScreen.SetActive(false);

            //Turns on win screen
            UserInterface.Instance.m_loseScreen.SetActive(true);

            //loads UIDisplayScene
            yield return StartCoroutine(LoadScene("UIDisplayScene"));

            //unloads Game screen
            StartCoroutine(UnloadScene(m_gameSceneName));

            //Unlock Cursor and make cursor visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //Fade In
            yield return StartCoroutine(UserInterface.Instance.FadeIn());
        }

        #endregion State Transitioning
    }
}
