using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UIElements;

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
        [SerializeField] public GameObject m_inventoryScreen;
        [SerializeField] public GameObject m_fadeScreen;
        [SerializeField] private GameObject m_InteractionPromptScreen;

        [Header("HUD Components")]
        [SerializeField] private UnityEngine.UI.Slider m_healthBar;
        [SerializeField] private TextMeshProUGUI m_playerLivesText;
        [SerializeField] private TextMeshProUGUI m_HUDScoreText;
        [SerializeField] private TextMeshProUGUI m_winScreenScoreText;
        [SerializeField] private TextMeshProUGUI m_looseScreenScoreText;
        [SerializeField] private TextMeshProUGUI m_InteractionPromptText;

        [Header("Main Menu Components")]
        [Tooltip("'Load Game Button' object. Only visible if 'Load From Main Menu' is set to true in the Game Instance")]
        [SerializeField] public GameObject m_loadButtonObject;
        [Tooltip("'Load Game Button' button. Only enabled if there is a valid save file to load")]
        [SerializeField] public UnityEngine.UI.Button m_loadButton;

        [Header("Inventory Screen")]
        [SerializeField] RectTransform m_inventoryScreenRect;
        [SerializeField] RectTransform m_inventoryBoxRect;
        //[SerializeField] InventoryItemBox m_inventoryItemBoxScript;
        [SerializeField] GameObject m_inventoryItemBox;

        //[SerializeField] float m_inventoryBoxWidthRatio = 0.75f;
        //[SerializeField] float m_inventoryBoxHeightRatio = 0.75f;

        [SerializeField] int imageBoxWidth = 100;
        [SerializeField] int imageBoxHeight = 100;

        [SerializeField] int m_rows = 4;
        [SerializeField] int m_columns = 5;
        [SerializeField] float m_margin = 20f;
        [SerializeField] float m_padding = 30f;


        [Header("Other Componenets")]
        [Tooltip("'Save Game button' Object. Only visible if 'Save From Pause Menu' is set to true in the Game Instance")]
        [SerializeField] public GameObject m_saveButton;





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


            GenerateInventoryBox();
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
            Color c = m_fadeScreen.GetComponent<UnityEngine.UI.Image>().color;

            //Reduce alpha channel
            for (float alpha = 1.0f; alpha >= 0; alpha -= 0.01f)
            {
                c.a = alpha;
                m_fadeScreen.GetComponent<UnityEngine.UI.Image>().color = c;   //Apply new alpha
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
            Color c = m_fadeScreen.GetComponent<UnityEngine.UI.Image>().color;

            //Increase alpha channel
            for (float alpha = 0; alpha <= 1; alpha += 0.01f)
            {
                c.a = alpha;
                m_fadeScreen.GetComponent<UnityEngine.UI.Image>().color = c;   //Apply new alpha
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
        /// Updates score text in HUD and final score text on the win and loose screens
        /// </summary>
        /// <param name="score">Current score</param>
        public void UpdateScore(float score)
        {
            m_HUDScoreText.text = "Score: " + score;   //updates score text for HUD
            m_winScreenScoreText.text = "Final Score: " + score;    //updates score text for win screen
            m_looseScreenScoreText.text = "Final Score: " + score;    //updates score text for loose screen
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





        #region UI Layout Adjustments

        /// <summary>
        /// Generates the Inventory Box based on user specifications
        /// </summary>
        public void GenerateInventoryBox()
        {
            
            Debug.Log("Generated Inventory Screen");

            //find the current size of the screen
            float screenWidth = m_inventoryScreenRect.rect.width;
            float screenHeight = m_inventoryScreenRect.rect.height;

            //calculate and set size of inventory box
            //float boxWidth = screenWidth * m_inventoryBoxWidthRatio;
            float boxWidth = (m_padding * 2) + (m_columns * (imageBoxWidth + m_margin)) - m_margin;
            //float boxHeight = screenHeight * m_inventoryBoxHeightRatio;
            //float boxHeight = screenHeight * m_inventoryBoxHeightRatio;
            float boxHeight = (m_padding * 2) + (m_rows * (imageBoxHeight + m_margin)) - m_margin;

            m_inventoryBoxRect.sizeDelta = new Vector2(boxWidth, boxHeight);


            //calculate locations and sizes of item boxes
            //sizes
            // newBoxWidth = (screenWidth - 2 padding) / (columns (boxwidth + margin) - 2 margin)
            // newBoxHeight = (screenHeight - 2 padding) / (rows (boxHeight + margin) - 2 margin)


            //position X
            //LeftBoxEdge = -1/2 BoxWidth
            //start at: X = LeftBoxEdge + padding + 1/2boxWidth
            //next: X = prevX + margin + boxWidth

            //position Y
            //TopBoxEdge = 1/2 BoxHeight
            //start at: Y = TopBoxEdge + padding + 1/2boxHeight
            //next: Y = prevY - margin - boxHeight


            //float leftBoxEdge = 1 * boxWidth;
            float leftBoxEdge = -0.5f * boxWidth;
            //float topBoxEdge = 1 * boxHeight;
            float topBoxEdge = 0.5f * boxHeight;



            //float itemBoxWidth = (screenWidth - (m_padding * 2)) / (m_columns * ()


            List<InventoryItemBox> itemBoxes = new List<InventoryItemBox>();

            for(int i=0; i < (m_rows * m_columns); i++)
            {
                //calculate position x
                float boxX = ((leftBoxEdge - (0.5f * imageBoxWidth) - m_margin) + m_padding) + ((i % m_columns + 1) * (imageBoxWidth + m_margin));
                //float boxX = ((0 - (0.5f * imageBoxWidth) - m_margin) + m_padding) + ((i % m_columns) * (imageBoxWidth + m_margin));
            
                //calculare position y
                float boxY = ((topBoxEdge + (0.5f * imageBoxHeight) + m_margin) - m_padding) - ((i % m_rows + 1) * (imageBoxHeight + m_margin));
                //float boxY = ((0 + (0.5f * imageBoxHeight) + m_margin) + m_padding) + ((i % m_rows) * (imageBoxHeight + m_margin));
            
            
                //GameObject box = GameObject.Instantiate(m_inventoryItemBox, m_inventoryBoxRect, m_inventoryBoxRect);
                GameObject box = GameObject.Instantiate(m_inventoryItemBox, m_inventoryBoxRect);
                //box.GetComponent<RectTransform>().SetParent(m_inventoryBoxRect, false);
                InventoryItemBox boxScript = box.GetComponent<InventoryItemBox>();
            
            
                boxScript.SetRectTransform(boxX, boxY, 100, 100);
                //boxScript.SetRectTransform(boxX + (0.5f * boxWidth), boxY + (0.5f * boxHeight), 100, 100);
            }

            ////m_inventoryItemBox.SetRectTransform(100, 100, 100, 200);
            //GameObject box1 = GameObject.Instantiate(m_inventoryItemBox, m_inventoryBoxRect);
            //InventoryItemBox boxScript1 = box1.GetComponent<InventoryItemBox>();
            //
            //boxScript1.SetRectTransform(500, 200, 100, 100);




        }


        #endregion UI Layout Adjustments


    }
}
