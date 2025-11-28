using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
    public class EquippedItemBox : MonoBehaviour, IDataPersistence
    {
        //Hidden Variables
        private string m_itemName = "";         //Script of the item currently being stored in this Equipped Item Box
        private int m_numItems = 0;             //Name of the item currently being stored in this Equipped Item Box
        private InventoryItem m_item = null;    //Number of items currently being stored in this Equipped Item Box
        private Sprite m_itemSprite;            //The sprite of the item burrently being stored in this Equipped Item Box

        [Header("Important Components")]
        [Tooltip("Image component to display item sprite")]
        [SerializeField] Image m_image;
        [Tooltip("Rect transform component of this Equipped Item Box")]
        [SerializeField] RectTransform m_rectTransform;
        [Tooltip("Text box to display item name")]
        [SerializeField] TextMeshProUGUI m_nameText;
        [Tooltip("Text box to display number of items")]
        [SerializeField] TextMeshProUGUI m_numberText;
        [Tooltip("Text used to tell user what key to use")]
        [SerializeField] TextMeshProUGUI m_keyIndicatorText;
        [Tooltip("What key activates/uses the item")]
        [SerializeField] KeyCode m_useKey = KeyCode.E;




        #region Awake Start and Update

        /// <summary>
        /// Hides empty image
        /// </summary>
        private void Awake()
        {
            m_image.enabled = false;
        }

        /// <summary>
        /// Adjusts the UI to properly indicate which key must be pressed to use the Equipped Item
        /// </summary>
        private void Start()
        {
            m_keyIndicatorText.text = m_useKey.ToString();
        }

        /// <summary>
        /// Checks if player uses equipped item, uses item if so
        /// </summary>
        private void Update()
        {
            //only uses item if correct key is down, there is an equipped item, and the player is alive
            if (Input.GetKeyDown(m_useKey) && m_item != null && GameInstance.Instance.getPlayerAlive())
            {
                m_item.Use();

                if (m_item.GetConsumeAfterUse()) //consumes item (reduces number of item by one) if item is marked to be consumed.
                {
                    if (removeInstanceOfItem())    //If all instances of the equipped item were used up, empties and updates equipped item box
                    {
                        //empties box and shows that box is empty
                        EmptyBox();

                        //notifies Inventory that the equipped item box is now empty
                        Inventory.Instance.OnEquippedItemEmpty();
                    }
                }
            }
        }

        #endregion Awake Start and Update



        #region Getters and Setters
        public string GetItemName() { return m_itemName; }          //Allows other scripts to get the name of the item(s) being stored in this box
        public InventoryItem GetItemScript() { return m_item; }     //Allows other scripts to get the script of this item being held in this box
        public int GetNumberOfItems() { return m_numItems; }        //Allows other scripts to get how many items this box currently contains


        public void SetNumberOfItems(int numItems) { m_numItems = numItems; UpdateBox(); }          //Allows other scripts to get how many items this box currently contains

        /// <summary>
        /// Sets this box's image
        /// </summary>
        /// <param name="image">The desired image</param>
        public void SetImage(Image image)
        {
            m_image.sprite = m_itemSprite;
        }
        #endregion Getters and Setters


        #region Equipped Item Box Functionality


        /// <summary>
        /// Removes all item instances from this box, and resets all values in this box
        /// </summary>
        public void EmptyBox()
        {
            m_item?.SetEquipped(false);

            m_item = null;
            m_itemName = string.Empty;
            m_numItems = 0;
            m_itemSprite = null;

            UpdateBox();
        }

        /// <summary>
        /// Removes one instance of this item box's item. 
        /// </summary>
        /// <returns>If this box is empty</returns>
        public bool removeInstanceOfItem()
        {
            m_numItems--;

            if (m_numItems <= 0)
            {
                EmptyBox();
                return true;
            }

            UpdateBox();
            return false;
        }


        /// <summary>
        /// Adds item to this item box
        /// </summary>
        /// <param name="item">Item being added</param>
        public void AddItem(InventoryItem item)
        {
            m_item = item;
            m_itemName = item.GetItemName();
            m_itemSprite = item.GetInventorySprite();

            m_item.SetEquipped(true);

            m_numItems++;   //increment number of items stored in the box

            UpdateBox();    //update box to propery display item
        }



        /// <summary>
        /// Updates the equipped item box appearance in the UI
        /// </summary>
        public void UpdateBox()
        {
            m_image.sprite = m_itemSprite;
            m_nameText.text = (m_numItems > 0) ? m_itemName : string.Empty;
            m_numberText.text = (m_numItems > 1) ? m_numItems.ToString() : string.Empty;

            if (m_itemSprite == null)
            {
                //Hides empty image if image is empty
                m_image.enabled = false;
            }
            else
            {
                //Unhides image if image is not empty
                m_image.enabled = true;
            }
        }

        #endregion Equipped Item Box Functionality


        #region Save and Load

        /// <summary>
        /// Saves this item, as well as all inventory related data for the item in this equipped item box (if this box currently contains at least one item)
        /// </summary>
        /// <param name="data">Reference to the GameData object with the data being saved</param>
        public void SaveData(ref GameData data)
        {
            //Saves number of items currently stored in this Equipped Item Box
            if (data.intData.ContainsKey("EquippedItemBox.NumberOfItems"))
            {
                data.intData["EquippedItemBox.NumberOfItems"] = m_numItems;
            }
            else
            {
                data.intData.Add("EquippedItemBox.NumberOfItems", m_numItems);
            }


            if (m_numItems > 0)
            {
                //Save name of item's item script (if this Equipped Item Box currently has an item)
                if (data.stringData.ContainsKey("EquippedItemBox.ItemScript"))
                {
                    data.stringData["EquippedItemBox.ItemScript"] = m_item.GetType().Name;
                }
                else
                {
                    data.stringData.Add("EquippedItemBox.ItemScript", m_item.GetType().Name);
                }

                //Saves the inventory related information for the item in this Equipped Item Box
                m_item.SaveForInventory(ref data, "EquippedItem");
            }
        }

        /// <summary>
        /// Loads this equipped item box, and if this box contains an item, creates and loads that item
        /// </summary>
        /// <param name="data">GameData object with the data being loaded</param>
        public void LoadData(GameData data)
        {
            //Check if key exists in intData, if so continue loading if not then do nothing
            if (data.intData.ContainsKey("EquippedItemBox.NumberOfItems"))
            {
                m_numItems = data.intData["EquippedItemBox.NumberOfItems"];

                //only continue loading if Equipped item box had any items to load
                if(m_numItems > 0)
                {
                    //Loads the name of the specific Inventory Item script for the item that was stored in this box
                    if (data.stringData.ContainsKey("EquippedItemBox.ItemScript"))
                    {
                        string scriptName = data.stringData["EquippedItemBox.ItemScript"];

                        string itemName = "";
                        
                        if (data.stringData.ContainsKey("EquippedItem.ItemName"))
                        {
                            itemName = data.stringData["EquippedItem.ItemName"];
                        }
                        
                        //Obtains stored item script from the Inventory
                        InventoryItem itemScript = Inventory.Instance.FindItemByScriptNameAndItemName(scriptName, itemName);

                        //If a script was found, creates a new instance of that item to be stored in this Equipped Item Box
                        if (itemScript != null)
                        {
                            InventoryItem newItem = GameObject.Instantiate(itemScript); //Creates item

                            newItem.LoadForInventory(data, "EquippedItem"); //Loads data into item

                            //Properly adds item to box
                            m_item = newItem;
                            m_itemName = newItem.GetItemName();
                            m_itemSprite = newItem.GetInventorySprite();

                            //Updates Box to properly reflect the current item
                            UpdateBox();

                            //Notifies Inventory of the new equipped item
                            Inventory.Instance.SetEquippedItem(newItem);
                        }
                        else
                        {
                            m_numItems = 0; //if no item script was found, then no item can be loaded, and the number of items in this Inventory Item Box must be returned to zero
                        }
                    }
                }                     
            }
        }
        #endregion Save and Load

    }
}
