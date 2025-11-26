using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
    public class InventoryItemBox : MonoBehaviour, IDataPersistence, IAmmunitionUser
    {
        //Hidden Variables
        private string m_boxID = "ItemBox";     //Unique ID of this Inventory Item Box

        private InventoryItem m_item = null;    //Script of the item currently being stored in this Inventory Item Box
        private string m_itemName = "";         //Name of the item currently being stored in this Inventory Item Box
        private int m_numItems = 0;             //Number of items currently being stored in this Inventory Item Box
        private Sprite m_itemSprite;            //The sprite of the item burrently being stored in this Inventory Item Box
        private bool m_itemIsWeapon = false;            //Is the current item contained in this box a WeaponItem
        private AmmunitionTracker m_ammoTracker = null;


        [Header("Important Components")]
        [Tooltip("Image component to display item sprite")]
        [SerializeField] Image m_image;
        [Tooltip("Rect transform component of this Inventory Item Box")]
        [SerializeField] RectTransform m_rectTransform;
        [Tooltip("Button component to register when item box has been selected")]
        [SerializeField] public Button m_button;
        [Tooltip("Text box to display item name")]
        [SerializeField] TextMeshProUGUI m_nameText;
        [Tooltip("Text box to display number of items")]
        [SerializeField] TextMeshProUGUI m_numberText;
        [Tooltip("Box containing ammunition text")]
        [SerializeField] GameObject m_ammunitionTextBox;
        [Tooltip("Text used to indicate remaining amount of ammunition (if applicable)")]
        [SerializeField] TextMeshProUGUI m_ammunitionText;


        #region Getters and Setters
        public string GetItemName() { return m_itemName; }  //Allows other scripts to get the name of the item(s) being stored in this box
        public InventoryItem GetItemScript() { return m_item; }   //Allows other scripts to get the script of this item being held in this box
        public int GetNumberOfItems() { return m_numItems; }        //Allows other scripts to get how many items this box currently contains
        public bool GetItemIsWeapon() {  return m_itemIsWeapon; }   //Allows other scripts to see if this box's item is a weapon


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

        #endregion Getters and Setters


        #region Inventory Item Box Functionality

        /// <summary>
        /// Removes all item instances from this box, and resets all values in this box
        /// </summary>
        public void EmptyBox()
        {
            m_item = null;
            m_itemName = string.Empty;
            m_numItems = 0;
            m_itemSprite = null;
            m_itemIsWeapon = false;
            UnsubscribeFromTracker();

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


        /// <summary>
        /// Adds item to this item box
        /// </summary>
        /// <param name="item">Item being added</param>
        public void AddItem(InventoryItem item)
        {
            m_item = item;
            m_itemName = item.GetItemName();
            m_itemSprite = item.GetInventorySprite();
            
            m_numItems++;   //increment number of items stored in the box

            //Check if item is a WeaponItem
            if(m_item.GetComponent<WeaponItem>())
            {
                m_itemIsWeapon = true;
            }

            UpdateBox();    //update box to propery display item
        }


        /// <summary>
        /// Updates the inventory item box appearance in the UI
        /// </summary>
        public void UpdateBox()
        {
            UnsubscribeFromTracker();

            m_image.sprite = m_itemSprite;
            m_nameText.text = (m_numItems > 0)? m_itemName : string.Empty;
            m_numberText.text = (m_numItems > 1)? m_numItems.ToString() : string.Empty;


            WeaponItem weaponScript = m_item?.GetComponent<WeaponItem>();

            if(m_numItems > 0 && m_item != null && weaponScript != null)
            {  
                string weaponName = weaponScript.GetWeaponName();
                m_nameText.text = (weaponName != string.Empty && weaponName != "") ? weaponName : m_itemName;
                

                SubscribeToTracker();
            }
            else
            {
                UnsubscribeFromTracker();
                m_ammunitionText.text= string.Empty;
                m_ammunitionTextBox.SetActive(false);
            }


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


        /// <summary>
        /// Notifies Inventory that this inventory item box has been selected
        /// </summary>
        public void OnItemBoxSelected()
        {
            Inventory.Instance.SelectItemInInventory(this);
        }

        #endregion Inventory Item Box Functionality


        #region Save and Load


        /// <summary>
        /// Saves this item, as well as all inventory related data for the item in this Inventory Item Box (if this box currently contains at least one item)
        /// </summary>
        /// <param name="data">Reference to the GameData object with the data being saved</param>
        public void SaveData(ref GameData data)
        {
            //Saves number of items currently stored in this Inventory Item Box
            if (data.intData.ContainsKey(m_boxID + ".NumberOfItems"))
            {
                data.intData[m_boxID + ".NumberOfItems"] = m_numItems;
            }
            else
            {
                data.intData.Add(m_boxID + ".NumberOfItems", m_numItems);
            }


            if (m_numItems > 0)
            {
                //Save name of item's item script (if this Inventory Item Box currently has an item)
                if (data.stringData.ContainsKey( m_boxID + ".ItemScript"))
                {
                    data.stringData[ m_boxID + ".ItemScript"] = m_item.GetType().Name;
                }
                else
                {
                    data.stringData.Add( m_boxID + ".ItemScript", m_item.GetType().Name);
                }

                //Save if the current item is a WeaponItem
                if (data.boolData.ContainsKey(m_boxID + ".ItemIsWeapon"))
                {
                    data.boolData[m_boxID + ".ItemIsWeapon"] = m_itemIsWeapon;
                }
                else
                {
                    data.boolData.Add(m_boxID + ".ItemIsWeapon", m_itemIsWeapon);
                }



                //Saves the inventory related information for the item in this Inventory Item Box
                m_item.SaveForInventory(ref data, m_boxID + "Item");
            }
        }

        /// <summary>
        /// Loads this Inventory Item Box, and if this box contains an item, creates and loads that item
        /// </summary>
        /// <param name="data">GameData object with the data being loaded</param>
        public void LoadData(GameData data)
        {
            //Check if key exists in intData, if so continue loading if not then do nothing
            if (data.intData.ContainsKey( m_boxID + ".NumberOfItems"))
            {
                m_numItems = data.intData[ m_boxID + ".NumberOfItems"];

                //only continue loading if Equipped item box had any items to load
                if (m_numItems > 0)
                {
                    //Loads the name of the specific Inventory Item script for the item that was stored in this box
                    if (data.stringData.ContainsKey( m_boxID + ".ItemScript"))
                    {
                        string scriptName = data.stringData[ m_boxID + ".ItemScript"];

                        string itemName = "";
                        
                        if (data.stringData.ContainsKey(m_boxID + "Item" + ".ItemName"))
                        {
                            itemName = data.stringData[m_boxID + "Item" + ".ItemName"];
                        }
                        
                        //Obtains stored item script from the Inventory
                        InventoryItem itemScript = Inventory.Instance.FindItemByScriptNameAndItemName(scriptName, itemName);

                        //If a script was found, creates a new instance of that item to be stored in this Inventory Item Box
                        if (itemScript != null)
                        {
                            InventoryItem newItem = GameObject.Instantiate(itemScript); //Creates item

                            newItem.LoadForInventory(data, m_boxID + "Item");   //Loads data into item

                            //Properly adds item to box
                            m_item = newItem;
                            m_itemName = newItem.GetItemName();
                            m_itemSprite = newItem.GetInventorySprite();


                            //Check for if item is a WeaponItem
                            if (data.boolData.ContainsKey(m_boxID + ".ItemIsWeapon"))
                            {
                                m_itemIsWeapon = data.boolData[m_boxID + ".ItemIsWeapon"];


                                //if(m_itemIsWeapon)
                                //{
                                //    Debug.Log("Loaded a weapon!");
                                //}
                            }


                            
                        }
                        else
                        {
                            m_numItems = 0; //if no item script was found, then no item can be loaded, and the number of items in this Inventory Item Box must be returned to zero
                        }
                    }
                }

                //Updates Box to properly reflect the current item
                UpdateBox();
            }
        }

        public void SubscribeToTracker()
        {
            WeaponItem weaponItem = m_item.GetComponent<WeaponItem>();

            m_ammoTracker = weaponItem.GetAmmunitionTracker();
            if (m_ammoTracker != null)
            {
                m_ammoTracker.AddUser(this);
                int ammoAmount = m_ammoTracker.GetAmmunitionAmount();

                m_ammunitionText.text = ammoAmount.ToString();
                m_ammunitionTextBox.SetActive(true);
            }
            else
            {
                m_ammunitionText.text = string.Empty;
                m_ammunitionTextBox.SetActive(false);
            }
        }

        public void UnsubscribeFromTracker()
        {
            if (m_ammoTracker != null)
            {
                m_ammoTracker.RemoveUser(this);
                //int ammoAmount = -1;

                m_ammunitionText.text = string.Empty;
                m_ammunitionTextBox.SetActive(false);
            }
        }

        public void OnAmmunitionChange(int ammount)
        {
            int ammoAmount = m_ammoTracker.GetAmmunitionAmount();

            m_ammunitionText.text = ammoAmount.ToString();
        }

        #endregion Save and Load

    }
}
