using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

namespace GameBase
{
    public class UserInterface : MonoBehaviour
    {
        //Hidden variables
        //Coroutine m_fadeCoroutine = null;
        


        //Exposed varaibles
        [Header("Screens")]
        [SerializeField] public GameObject m_titleScreen;
        [SerializeField] public GameObject m_mainMenuScreen;
        [SerializeField] public GameObject m_winScreen;
        [SerializeField] public GameObject m_loseScreen;
        [SerializeField] public GameObject m_HUD;
        [SerializeField] public GameObject m_pauseScreen;
        [SerializeField] public GameObject m_fadeScreen;

        [Header("Components")]
        [SerializeField] private Slider m_healthBar;
        [Tooltip("Save Game button. Only visible if 'Save From Pause Menu' is set to true in the Game Instance")]
        [SerializeField] public GameObject m_saveButton;


        




        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
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
        /// Loads game level and begins game
        /// </summary>
        public void PlayClicked()
        {
            GameInstance.Instance.m_gameState = GameState.STARTGAME;
        }

        /// <summary>
        /// Unpauses game
        /// </summary>
        public void UnpauseClicked()
        {
            GameInstance.Instance.UnpauseGame();
        }


        /// <summary>
        /// Returns to main menu
        /// </summary>
        public void MainMenuClicked()
        {
            GameInstance.Instance.m_gameState = GameState.LOADMAINMENU;
        }



        public void SaveClicked()
        {
            DataPersistenceManager.Instance.SaveGame();
        }



        public void LoadAndPlayClicked()
        {
            GameInstance.Instance.LoadOnPlay();
            GameInstance.Instance.m_gameState = GameState.STARTGAME;
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


        /// <summary>
        /// Update player health bar
        /// </summary>
        /// <param name="currentHealth">Current player character health</param>
        /// <param name="maxHealth">Player character max health</param>
        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            m_healthBar.maxValue = maxHealth;
            m_healthBar.value = currentHealth;
        }
    }
}
