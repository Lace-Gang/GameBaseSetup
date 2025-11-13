using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
    public class InventoryItemBox : MonoBehaviour, IDataPersistence
    {
        //Hidden Variables
        private string m_boxID = "ItemBox";

        private InventoryItem m_item = null;
        private string m_itemName = "";
        private int m_numItems = 0;
        private Sprite m_itemSprite;
        //private List<InventoryItem> m_items = new List<InventoryItem>();    //Stores all scripts of the item stored in this box


        [Header("Important Components")]
        [SerializeField] Image m_image;
        [SerializeField] RectTransform m_rectTransform;
        [SerializeField] public Button m_button;
        [SerializeField] TextMeshProUGUI m_nameText;
        [SerializeField] TextMeshProUGUI m_numberText;


        #region Getters and Setters
        public string GetItemName() { return m_itemName; }  //Allows other scripts to get the name of the item(s) being stored in this box
        public InventoryItem GetItemScript() { return m_item; }   //Allows other scripts to get the script of this item being held in this box
        public int GetNumberOfItems() { return m_numItems; }        //Allows other scripts to get how many items this box currently contains


        public void SetNumberOfItems(int numItems) { m_numItems = numItems; UpdateBox(); }          //Allows other scripts to get how many items this box currently contains
        public void SetBoxID(string boxID) { m_boxID = boxID; }          //Allows other scripts to get how many items this box currently contains

        /// <summary>
        /// Sets the position and dimensions of the inventory box
        /// </summary>
        /// <param name="positionX">Desired X position of this box on the canvas</param>
        /// <param name="positionY">Desired Y position of this box on the canvas</param>
        /// <param name="width">Desired width of this box</param>
        /// <param name="height">Desired height of this box</param>
        public void SetRectTransform(float positionX, float positionY, float width, float height)
        {
            m_rectTransform.sizeDelta = new Vector2(width, height); //Set dimensions of box

            m_rectTransform.anchoredPosition = new Vector2(positionX, positionY);   //set location of box
        }

        /// <summary>
        /// Sets this box's image
        /// </summary>
        /// <param name="image">The desired image</param>
        public void SetImage(Image image)
        {
            m_image.sprite = m_itemSprite;
        }

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

            if(m_numItems <= 0)
            {
                EmptyBox();
                return true;
            }

            UpdateBox();
            return false;
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
            m_nameText.text = (m_numItems > 0)? m_itemName : string.Empty;
            m_numberText.text = (m_numItems > 1)? m_numItems.ToString() : string.Empty;
        }


        /// <summary>
        /// Notifies Inventory that this inventory item box has been selected
        /// </summary>
        public void OnItemBoxSelected()
        {
            Inventory.Instance.SelectItemInInventory(this);
        }


        public void SaveData(ref GameData data)
        {
            if (m_numItems > 0)
            {
                //Check stringData for key. If key exists, change value to current value, else add key with current value
                if (data.stringData.ContainsKey( m_boxID + ".ItemScript"))
                {
                    data.stringData[ m_boxID + ".ItemScript"] = m_item.GetType().Name;
                }
                else
                {
                    data.stringData.Add( m_boxID + ".ItemScript", m_item.GetType().Name);
                }

                //Check intData for key. If key exists, change value to current value, else add key with current value
                if (data.intData.ContainsKey( m_boxID + ".NumberOfItems"))
                {
                    data.intData[ m_boxID + ".NumberOfItems"] = m_numItems;
                }
                else
                {
                    data.intData.Add( m_boxID + ".NumberOfItems", m_numItems);
                }

                m_item.SaveForInventory(ref data, m_boxID + "Item");
            }
        }

        public void LoadData(GameData data)
        {
            //Check if key exists in intData, if so continue loading if not then do nothing
            if (data.intData.ContainsKey( m_boxID + ".NumberOfItems"))
            {
                m_numItems = data.intData[ m_boxID + ".NumberOfItems"];

                //only continue loading if Equipped item box had any items to load
                if (m_numItems > 0)
                {
                    if (data.stringData.ContainsKey( m_boxID + ".ItemScript"))
                    {
                        string itemName = data.stringData[ m_boxID + ".ItemScript"];

                        InventoryItem itemScript = Inventory.Instance.FindItemByName(itemName);

                        if (itemScript != null)
                        {
                            InventoryItem newItem = GameObject.Instantiate(itemScript);

                            newItem.LoadForInventory(data, m_boxID + "Item");

                            m_numItems--;
                            AddItem(newItem);
                        }



                        //newItem.LoadForInventory(data, m_boxID + "Item");
                        //
                        //m_numItems--;
                        //AddItem(newItem);
                    }
                }
            }
        }
    }
}
