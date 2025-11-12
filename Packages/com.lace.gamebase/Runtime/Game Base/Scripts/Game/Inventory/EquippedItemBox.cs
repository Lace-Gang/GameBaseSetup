using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
    public class EquippedItemBox : MonoBehaviour
    {
        //Hidden Variables
        private string m_itemName = "";
        private int m_numItems = 0;
        private InventoryItem m_item = null;
        private Sprite m_itemSprite;
        //private List<InventoryItem> m_items = new List<InventoryItem>();    //Stores all scripts of the item stored in this box


        [Header("Important Components")]
        [SerializeField] Image m_image;
        [SerializeField] RectTransform m_rectTransform;
        [SerializeField] TextMeshProUGUI m_nameText;
        [SerializeField] TextMeshProUGUI m_numberText;
        [Tooltip("Text used to tell user what key to use")]
        [SerializeField] TextMeshProUGUI m_keyIndicatorText;
        [Tooltip("What key activates/uses the item")]
        [SerializeField] KeyCode m_useKey = KeyCode.E;




        #region Start and Update

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

        #endregion Start and Update





        #region Getters and Setters
        public string GetItemName() { return m_itemName; }          //Allows other scripts to get the name of the item(s) being stored in this box
        public InventoryItem GetItemScript() { return m_item; }     //Allows other scripts to get the script of this item being held in this box
        public int GetNumberOfItems() { return m_numItems; }        //Allows other scripts to get how many items this box currently contains


        public void SetNumberOfItems(int numItems) { m_numItems = numItems; UpdateBox(); }          //Allows other scripts to get how many items this box currently contains


        /// <summary>
        /// Removes all item instances from this box, and resets all values in this box
        /// </summary>
        public void EmptyBox()
        {
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


        ///// <summary>
        ///// Sets the position and dimensions of the inventory box
        ///// </summary>
        ///// <param name="positionX">Desired X position of this box on the canvas</param>
        ///// <param name="positionY">Desired Y position of this box on the canvas</param>
        ///// <param name="width">Desired width of this box</param>
        ///// <param name="height">Desired height of this box</param>
        //public void SetRectTransform(float positionX, float positionY, float width, float height)
        //{
        //    m_rectTransform.sizeDelta = new Vector2(width, height); //Set dimensions of box
        //
        //    m_rectTransform.anchoredPosition = new Vector2(positionX, positionY);   //set location of box
        //}

        /// <summary>
        /// Sets this box's image
        /// </summary>
        /// <param name="image">The desired image</param>
        public void SetImage(Image image)
        {
            m_image.sprite = m_itemSprite;
        }
        #endregion Getters and Setters

        /// <summary>
        /// Adds item to this item box
        /// </summary>
        /// <param name="item">Item being added</param>
        public void AddItem(InventoryItem item)
        {
            m_item = item;
            m_itemName = item.GetItemName();
            m_itemSprite = item.GetInventorySprite();

            m_numItems++;

            UpdateBox();
        }



        /// <summary>
        /// Updates the inventory item box appearance in the UI
        /// </summary>
        public void UpdateBox()
        {
            m_image.sprite = m_itemSprite;
            m_nameText.text = (m_numItems > 0) ? m_itemName : string.Empty;
            m_numberText.text = (m_numItems > 1) ? m_numItems.ToString() : string.Empty;
        }
    }
}
