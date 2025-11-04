using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace GameBase
{
    public class UserInterface : MonoBehaviour
    {
        //Exposed Varaibles
        [Header("Screens")]
        [SerializeField] public GameObject m_titleScreen;
        [SerializeField] public GameObject m_mainMenuScreen;
        [SerializeField] public GameObject m_winScreen;
        [SerializeField] public GameObject m_loseScreen;
        [SerializeField] public GameObject m_HUD;
        [SerializeField] public GameObject m_pauseScreen;
        [SerializeField] public GameObject m_fadeScreen;
        [SerializeField] private GameObject m_InteractionPromptScreen;

        [Header("HUD Components")]
        [SerializeField] private Slider m_healthBar;
        [SerializeField] private TextMeshProUGUI m_playerLivesText;
        [SerializeField] private TextMeshProUGUI m_InteractionPromptText;


        [Header("Other Componenets")]
        [Tooltip("Save Game button. Only visible if 'Save From Pause Menu' is set to true in the Game Instance")]
        [SerializeField] public GameObject m_saveButton;
        [Tooltip("'Load Game Button' object. Only visible if 'Load From Main Menu' is set to true in the Game Instance")]
        [SerializeField] public GameObject m_loadButtonObject;
        [Tooltip("'Load Game Button' button. Only enabled if there is a valid save file to load")]
        [SerializeField] public Button m_loadButton;



        public static UserInterface Instance { get; private set; }  //Allows other scripts to get the singleton instance of the UserInterface

        /// <summary>
        /// Ensures that only one Instance of the User Interface exists and notifies User if there is more than one instance
        /// </summary>
        private void Awake()
        {
            //Sets up the User Interface as a singleton (ensures only one can be present)
            if (Instance != null)
            {
                Debug.LogError("Found more than one User Interface in the scene.");
            }
            Instance = this;
        }


        #region Button Clicks

        /// <summary>
        /// Closes Application
        /// </summary>
        public void QuitClicked()
        {
            Application.Quit();
            Debug.Log("Application has been Quit"); //line exists to validate that application has been quit (because Application.Quit only executes in a .exe application)
        }


        /// <summary>
        /// Loads game level and begins new game
        /// </summary>
        public void NewGameClicked()
        {
            GameInstance.Instance.SetLoadOnPlay(false); //Notifies GameInstance that a New Game should be loaded and not a saved game
            GameInstance.Instance.m_gameState = GameState.STARTGAME;    //Transitions to "Start Game" game state
        }


        /// <summary>
        /// Unpauses game
        /// </summary>
        public void UnpauseClicked()
        {
            GameInstance.Instance.UnpauseGame();    //Tells GameInstance to unpause the game
        }


        /// <summary>
        /// Returns to main menu
        /// </summary>
        public void MainMenuClicked()
        {
            GameInstance.Instance.m_gameState = GameState.LOADMAINMENU;     //Transition to "Load Main Menu" game state
        }


        /// <summary>
        /// Saves Game
        /// </summary>
        public void SaveClicked()
        {
            DataPersistenceManager.Instance.SaveGame(); //Tells DataPersistenceManager to save the game
        }


        /// <summary>
        /// Loads game level, then loads save file, then begins saved game
        /// </summary>
        public void LoadAndPlayClicked()
        {
            GameInstance.Instance.SetLoadOnPlay(true);      //Tells GameInstance to load the saved game
            GameInstance.Instance.m_gameState = GameState.STARTGAME;    //Transitions to "Start Game" game state
        }

        /// <summary>
        /// Calls for a game restart from outside of gameplay
        /// </summary>
        public void RetryClicked()
        {
            GameInstance.Instance.RestartFromScreen();  //Tells GameInstance to restart game, and informs GameInstance that this restart is being called from outside of gameplay
        }

        /// <summary>
        /// Calls for a game restart from within gameplay
        /// </summary>
        public void RestartClicked()
        {
            GameInstance.Instance.RestartFromGame();    //Tells GameInstance to restart the game, and informs GameInstance that this restart is being called for from within gameplay
        }


        #endregion Button Clicks

        #region Screen Fade
        /// <summary>
        /// Fades screen in
        /// </summary>
        /// <returns>Yield return for Coroutine</returns>
        public IEnumerator FadeIn()
        {
            //Get current Fade Screen color
            Color c = m_fadeScreen.GetComponent<Image>().color;

            //Reduce alpha channel
            for (float alpha = 1.0f; alpha >= 0; alpha -= 0.01f)
            {
                c.a = alpha;
                m_fadeScreen.GetComponent<Image>().color = c;   //Apply new alpha
                yield return null;
            }
        }

        /// <summary>
        /// Fades screen out
        /// </summary>
        /// <returns>Yield return for Coroutine</returns>
        public IEnumerator FadeOut()
        {
            //Get current Fade Screen color
            Color c = m_fadeScreen.GetComponent<Image>().color;

            //Increase alpha channel
            for (float alpha = 0; alpha <= 1; alpha += 0.01f)
            {
                c.a = alpha;
                m_fadeScreen.GetComponent<Image>().color = c;   //Apply new alpha
                yield return null;
            }

        }
        #endregion Screen Fade

        #region HUD Updates

        /// <summary>
        /// Update player health bar
        /// </summary>
        /// <param name="currentHealth">Current player character health</param>
        /// <param name="maxHealth">Player character max health</param>
        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            m_healthBar.maxValue = maxHealth;   //Tells health bar what its max value should be
            m_healthBar.value = currentHealth;  //Tells health bar what its value should be
        }


        /// <summary>
        /// Updates Player Lives text
        /// </summary>
        /// <param name="lives">Current number of player lives</param>
        public void UpdatePlayerLives(int lives)
        {
            m_playerLivesText.text = "Lives: " + lives; //Updates Player Lives text
        }


        /// <summary>
        /// Makes interaction prompt visible and sets text
        /// </summary>
        /// <param name="prompt">Prompt to display</param>
        public void DisplayPromptBox(string prompt)
        {
            m_InteractionPromptText.text = prompt;
            m_InteractionPromptScreen.SetActive(true);
        }

        /// <summary>
        /// Hides promp box
        /// </summary>
        public void HidePromptBox()
        {
            m_InteractionPromptScreen.SetActive(false);
        }

        #endregion HUD Updates
    }
}
