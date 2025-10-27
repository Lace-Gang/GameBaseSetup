using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

namespace GameBase
{
    public class UserInterface : MonoBehaviour
    {
        //Hidden variables



        //Exposed varaibles
        [Header("Screens")]
        [SerializeField] public GameObject m_titleScreen;
        [SerializeField] public GameObject m_mainMenuScreen;
        [SerializeField] public GameObject m_winScreen;
        [SerializeField] public GameObject m_loseScreen;
        [SerializeField] public GameObject m_HUD;
        [SerializeField] public GameObject m_pauseScreen;

        [Header("Components")]
        [SerializeField] private Slider m_healthBar;


        




        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }







        //// Button Click Functions
        


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
            Debug.Log("Play Game Clicked!");        //Test Line

            GameInstance.Instance.m_gameState = GameState.STARTGAME;


            //m_titleScreen.SetActive(false);

            //GameInstance.Instance.LoadScene("SampleScene");

        }

        /// <summary>
        /// Unpauses game
        /// </summary>
        public void ContinueClicked()
        {
            Debug.Log("Continue Clicked");          //Test Line
        }


        /// <summary>
        /// Returns to main menu
        /// </summary>
        public void MainMenuClicked()
        {
            GameInstance.Instance.m_gameState = GameState.LOADMAINMENU;

            Debug.Log("Main Clicked");              //Test Line
        }





        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            m_healthBar.maxValue = maxHealth;
            m_healthBar.value = currentHealth;
        }
    }
}
