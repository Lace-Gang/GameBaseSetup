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
        #region Variables Exposed In Editor

        //Exposed Varaibles

        [Header("Screens")]
        [SerializeField] public GameObject m_titleScreen;
        [SerializeField] public GameObject m_mainMenuScreen;
        [SerializeField] public GameObject m_winScreen;
        [SerializeField] public GameObject m_loseScreen;
        [SerializeField] public GameObject m_HUD;
        [SerializeField] public GameObject m_pauseScreen;
        [SerializeField] public GameObject m_inventoryScreen;
        [SerializeField] public GameObject m_inventoryMenuScreen;
        [SerializeField] public GameObject m_fadeScreen;
        [SerializeField] private GameObject m_interactionPromptScreen;

        [Header("HUD Components")]
        [SerializeField] private UnityEngine.UI.Slider m_healthBar;
        [SerializeField] private TextMeshProUGUI m_playerLivesText;
        [SerializeField] private TextMeshProUGUI m_HUDScoreText;
        [SerializeField] private TextMeshProUGUI m_winScreenScoreText;
        [SerializeField] private TextMeshProUGUI m_looseScreenScoreText;
        [SerializeField] private TextMeshProUGUI m_InteractionPromptText;
        [SerializeField] private EquippedWeaponBox m_weaponBox;

        [Header("Main Menu Components")]
        [Tooltip("'Load Game Button' object. Only visible if 'Load From Main Menu' is set to true in the Game Instance")]
        [SerializeField] public GameObject m_loadButtonObject;
        [Tooltip("'Load Game Button' button. Only enabled if there is a valid save file to load")]
        [SerializeField] public UnityEngine.UI.Button m_loadButton;

        [Header("Inventory Screen")]
        [SerializeField] RectTransform m_inventoryScreenRect;
        [SerializeField] RectTransform m_inventoryBoxRect;
        [SerializeField] GameObject m_inventoryItemBox;
        [SerializeField] public UnityEngine.UI.Button m_useButton;
        [SerializeField] public UnityEngine.UI.Button m_equipButton;
        [SerializeField] public UnityEngine.UI.Button m_discardButton;

        [SerializeField] int m_imageBoxWidth = 100;
        [SerializeField] int m_imageBoxHeight = 100;

        [SerializeField] int m_rows = 4;
        [SerializeField] int m_columns = 5;
        [SerializeField] float m_margin = 20f;
        [SerializeField] float m_padding = 30f;


        [Header("Other Componenets")]
        [Tooltip("'Save Game button' Object. Only visible if 'Save From Pause Menu' is set to true in the Game Instance")]
        [SerializeField] public GameObject m_saveButton;

        [Header("UI Audio")]
        [SerializeField] bool m_useAudio = false;
        [SerializeField] AudioSource m_audioSource;
        [SerializeField] AudioClip m_buttonClickSound;

        #endregion Variables Exposed In Editor



        public static UserInterface Instance { get; private set; }  //Allows other scripts to get the singleton instance of the UserInterface

        /// <summary>
        /// Ensures that only one Instance of the User Interface exists and notifies User if there is more than one instance
        /// </summary>
        private void Awake()
        {
            //Notifies user if User Interface Singleton is being used improperly
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
            //Uses button click audio if indicated to do so
            if(m_useAudio)
            {
                m_audioSource?.PlayOneShot(m_buttonClickSound);
            }

            GameInstance.Instance.SetLoadOnPlay(false); //Notifies GameInstance that a New Game should be loaded and not a saved game
            GameInstance.Instance.m_gameState = GameState.STARTGAME;    //Transitions to "Start Game" game state
        }


        /// <summary>
        /// Unpauses game
        /// </summary>
        public void UnpauseClicked()
        {
            //Uses button click audio if indicated to do so
            if (m_useAudio)
            {
                m_audioSource?.PlayOneShot(m_buttonClickSound);
            }

            GameInstance.Instance.TogglePauseMenu();    //Tells pause menu to close (if it is open)
        }


        /// <summary>
        /// Returns to main menu
        /// </summary>
        public void MainMenuClicked()
        {
            //Uses button click audio if indicated to do so
            if (m_useAudio)
            {
                m_audioSource?.PlayOneShot(m_buttonClickSound);
            }

            if(GameInstance.Instance.m_gameState != GameState.MAINMENUSCREEN)
            {
                GameInstance.Instance.m_gameState = GameState.LOADMAINMENU;     //Transition to "Load Main Menu" game state
            }
        }


        /// <summary>
        /// Saves Game
        /// </summary>
        public void SaveClicked()
        {
            //Uses button click audio if indicated to do so
            if (m_useAudio)
            {
                m_audioSource?.PlayOneShot(m_buttonClickSound);
            }

            DataPersistenceManager.Instance.SaveGame(); //Tells DataPersistenceManager to save the game
        }


        /// <summary>
        /// Loads game level, then loads save file, then begins saved game
        /// </summary>
        public void LoadAndPlayClicked()
        {
            //Uses button click audio if indicated to do so
            if (m_useAudio)
            {
                m_audioSource?.PlayOneShot(m_buttonClickSound);
            }

            GameInstance.Instance.SetLoadOnPlay(true);      //Tells GameInstance to load the saved game
            GameInstance.Instance.m_gameState = GameState.STARTGAME;    //Transitions to "Start Game" game state
        }

        /// <summary>
        /// Calls for a game restart from outside of gameplay
        /// </summary>
        public void RetryClicked()
        {
            //Uses button click audio if indicated to do so
            if (m_useAudio)
            {
                m_audioSource?.PlayOneShot(m_buttonClickSound);
            }

            GameInstance.Instance.RestartFromScreen();  //Tells GameInstance to restart game, and informs GameInstance that this restart is being called from outside of gameplay
        }

        /// <summary>
        /// Calls for a game restart from within gameplay
        /// </summary>
        public void RestartClicked()
        {
            //Uses button click audio if indicated to do so
            if (m_useAudio)
            {
                m_audioSource?.PlayOneShot(m_buttonClickSound);
            }

            GameInstance.Instance.RestartFromGame();    //Tells GameInstance to restart the game, and informs GameInstance that this restart is being called for from within gameplay
        }


        /// <summary>
        /// Tells Inventory to use the selected inventory item
        /// </summary>
        public void UseSelectedItemClicked()
        {
            //Uses button click audio if indicated to do so
            if (m_useAudio)
            {
                m_audioSource?.PlayOneShot(m_buttonClickSound);
            }

            Inventory.Instance.UseSelectedItem();   //Tells inventory to use the selected item
        }

        /// <summary>
        /// Tells inventory to equip selected inventory item
        /// </summary>
        public void EquipSelectedItem()
        {
            //Uses button click audio if indicated to do so
            if (m_useAudio)
            {
                m_audioSource?.PlayOneShot(m_buttonClickSound);
            }

            Inventory.Instance.EqipSelectedItem();  //Tells inventory to equip the selected item
        }

        /// <summary>
        /// Tells inventory to remove selected inventory item from inventory
        /// </summary>
        public void RemoveSelectedItem()
        {
            //Uses button click audio if indicated to do so
            if (m_useAudio)
            {
                m_audioSource?.PlayOneShot(m_buttonClickSound);
            }

            Inventory.Instance.RemoveSelectedItem();    //tells inventory to remove the selected item
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
            m_InteractionPromptText.text = prompt;      //sets proper text to prompt text box
            m_interactionPromptScreen.SetActive(true);  //displays prompt text box
        }

        /// <summary>
        /// Hides promp box
        /// </summary>
        public void HidePromptBox()
        {
            m_interactionPromptScreen.SetActive(false);     //Hides prompt text box
        }

        #endregion HUD Updates


        #region UI Layout Adjustments

        /// <summary>
        /// Generates the Inventory Box based on user specifications
        /// </summary>
        public List<InventoryItemBox> GenerateInventoryBox()
        {
            //Creates list to store item box scripts in
            List<InventoryItemBox> itemBoxes = new List<InventoryItemBox>();

            //find the current size of the screen
            float screenWidth = m_inventoryScreenRect.rect.width;
            float screenHeight = m_inventoryScreenRect.rect.height;

            //calculate and set size of inventory box

            float boxWidth = (m_padding * 2) + (m_columns * (m_imageBoxWidth + m_margin)) - m_margin;
            float boxHeight = (m_padding * 2) + (m_rows * (m_imageBoxHeight + m_margin)) - m_margin;
            m_inventoryBoxRect.sizeDelta = new Vector2(boxWidth, boxHeight);


            ////calculate locations and sizes of item boxes

            //Find our starting points (the left side and top of the inventory box)
            float leftBoxEdge = -0.5f * boxWidth;
            float topBoxEdge = 0.5f * boxHeight;

            //Calculate box locations
            for (int i = 1; i <= m_rows; i++)
            {
                float boxY = ((topBoxEdge + (0.5f * m_imageBoxHeight) + m_margin) - m_padding) - (i * (m_imageBoxHeight + m_margin));    //calculare position y
            
                for (int j = 1; j <= m_columns; j++)
                {
                    float boxX = ((leftBoxEdge - (0.5f * m_imageBoxWidth) - m_margin) + m_padding) + (j * (m_imageBoxWidth + m_margin));  //calculate position x
            
                    GameObject box = GameObject.Instantiate(m_inventoryItemBox, m_inventoryBoxRect);    //Create box
                    InventoryItemBox boxScript = box.GetComponent<InventoryItemBox>();                  //Get reference to that box's script
                    boxScript.SetRectTransform(boxX, boxY, m_imageBoxWidth, m_imageBoxHeight);          //Set dimensions and location of the item box

                    boxScript.SetBoxID("ItemBox" + (((i - 1) * m_columns) + j));                        //Give item box a unique ID
            
                    itemBoxes.Add(boxScript);   //Adds item box script to list
                }
            }

            return itemBoxes;   //returns list of boxes
        }


        #endregion UI Layout Adjustments


    }
}
