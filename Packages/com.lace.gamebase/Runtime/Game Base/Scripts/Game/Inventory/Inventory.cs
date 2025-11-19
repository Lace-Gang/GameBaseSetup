using NUnit.Framework.Internal;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace GameBase
{
    public class Inventory : MonoBehaviour
    {
        //Hidden Variables
        InventoryItem m_selectedItem;               //What item is currently selected in the inventory screen (if any)
        InventoryItemBox m_selectedItemBox = null;  //What item box is currently selected in the inventory screen (if any)
        InventoryItem m_equippedItem;               //What item is currently equipped
        InventoryItem m_equippedWeapon;             //What weapon is currently equipped

        //Hidden Variable Lists
        private List<InventoryItemBox> m_inventoryItemBoxes = new List<InventoryItemBox>();     //stores item boxes in the inventory


        //Exposed Variables
        [Header("General Inventory Configuration")]
        [Tooltip("Should Inventory System be used")]
        [SerializeField] bool m_useInventory = false;
        [Tooltip("If equipped item box is empty, next item added to inventory will be automatically equipped")]
        [SerializeField] bool m_sendFirstItemToEquipped = false;

        [Header("Important References")]
        [Tooltip("Reference to the Equipped Item Box in the HUD")]
        [SerializeField] EquippedItemBox m_equippedItemBox = null;
        [Tooltip("Reference to the Equipped Item Box in the HUD")]
        [SerializeField] EquippedWeaponBox m_equippedWeaponBox = null;
        [Tooltip("Any Inventory Item that should be savable must have at least one prefab in this list that contains that item's unique Script. Different" +
            " item Configurations using the same script only need the script to be present once as a whole, not once each UNLESS they have different item sprites!")]
        [SerializeField] List<InventoryItem> m_savableInventoryItems = new List<InventoryItem>();




        public static Inventory Instance { get; private set; }  //Allows other scripts to get the singleton instance of the Inventory

        public bool GetUseInventory() { return m_useInventory; }    //Allows other scripts to see if inventory is being used

        public void SetEquippedItem(InventoryItem equippedItem) { m_equippedItem = equippedItem; }

        #region Awake and Start

        /// <summary>
        /// Checks that only this instance of the Inventory exists at this time and notifies the user if this is not true.
        /// Only one instance of the Inventory should exist at any one time.
        /// </summary>
        private void Awake()
        {
            //Notifies user if Inventory Singleton is being used improperly
            if (Instance != null)
            {
                Debug.LogError("Found more than one Inventory in the scene.");
            }
            Instance = this; 
        }

        /// <summary>
        /// If inventory system is active, generates the Inventory screen
        /// </summary>
        void Start()
        {
            //if inventory is not being used, hides Equipped Item Box
            if(!m_useInventory)
            {
                m_equippedItemBox.gameObject.SetActive(false);
                return;
            }

            //Generate Inventory screen and get references to item boxes (only if 'Use Inventory' is true)
            m_inventoryItemBoxes = UserInterface.Instance.GenerateInventoryBox();
        }

        #endregion Awake and Start


        #region Inventory Functionality

        /// <summary>
        /// Finds the correct InventoryItem script from amoung the prefabs listed in the 'Savable Inventory Items' list
        /// </summary>
        /// <param name="scriptName">Name of script being searched for</param>
        /// <param name="itemName">Item name as it is in the prefab that was added to the Savable Inventory Items</param>
        /// <returns>The InventoryItem script on the first prefab in the list that fits the requirements. Returns null if no prefab is found</returns>
        public InventoryItem FindItemByScriptNameAndItemName(string scriptName, string itemName)
        {
            //Searches through all prefabs in the Savable Item Inventory for a prefab that fits the requirements
            foreach(InventoryItem itemPrefab in m_savableInventoryItems)
            {
                if(itemPrefab.GetComponent<InventoryItem>() != null && itemPrefab.GetComponent<InventoryItem>().GetType().Name == scriptName)   //Checks for correct script name
                { 
                    if(itemPrefab.GetComponent<InventoryItem>().GetItemName() == itemName)  //Checks for correct item name
                    {
                        return itemPrefab;  //If all conditions are met by a prefab, returns the script on that prefab
                    }
                }
            }

            return null;    //If no prefab was found that fit the requirements, returns null
        }


        /// <summary>
        /// Adds item to inventory
        /// </summary>
        /// <param name="item">Item being added</param>
        /// <returns>Item was added successfully</returns>
        public bool AddItemToInventory(InventoryItem item)
        {
            bool stacking = item.GetStackInstances();   //can item be stacked in inventory?
            bool itemAdded = false;                     //Has item been added yet?

            //Debug.Log("ZERO");


            //Auto-equips item if no item is equipped, and inventory is instructed to do so and the item is equippable
            if (item.GetEquippable() && m_equippedItem == null && m_sendFirstItemToEquipped)   
            {
                //Debug.Log("ONE");
                m_equippedItemBox.AddItem(item);    //equip item
                m_equippedItem = item;              //track equipped item
                itemAdded = true;
            }
            else if (m_equippedItem != null && stacking && m_equippedItem == item)  //Checks if new item is the same as the equipped item, and adds to equipped item stack if so
            {
                //Debug.Log("Two");

                m_equippedItemBox.AddItem(item);    //add to eqiupped item
                itemAdded = true;
            }
            else
            {
                //loops through all boxes and checks for the first box that this item can be added to
                foreach (InventoryItemBox box in m_inventoryItemBoxes)
                {
                    //If item can be stacked, stacks item if box contains items of the same name
                    //if (stacking && box.GetItemScript() != null && box.GetItemScript() == item)
                    if (stacking && box.GetItemScript() == item)
                    {
                        //Debug.Log("THREE");

                        box.AddItem(item);
                        itemAdded = true;
                        break;
                    }
                    if (box.GetItemScript() == null) //adds item to first box that does not yet have an item
                    {
                        //Debug.Log("FOUR");
                        box.AddItem(item);
                        itemAdded = true;
                        break;
                    }
                }
            }


            return itemAdded;   //indicate if item was able to be added
        }


        /// <summary>
        /// Sets equipped item tracker to null
        /// </summary>
        public void OnEquippedItemEmpty()
        {
            m_equippedItem = null;
        }

        /// <summary>
        /// Clears entire inventory, including the eqiupped item
        /// </summary>
        public void ClearInventory()
        {
            m_equippedItemBox.EmptyBox();

            m_equippedItem = null;

            foreach(InventoryItemBox box in m_inventoryItemBoxes)
            {
                box.EmptyBox();
            }
        }

        #endregion Inventory Functionality



        #region Select and Deselect Item In Inventory

        /// <summary>
        /// If item box has an item: Shows that item box has been selected, tracks selected item and item box, and shows inventory menu
        /// </summary>
        /// <param name="itemBox">Newly selected inventory item box</param>
        public void SelectItemInInventory(InventoryItemBox itemBox)
        {
            if (m_selectedItem != null) { m_selectedItemBox.m_button.interactable = true; } //if there is a selected item, marks item box as selectable again

            if(itemBox.GetItemScript() != null)
            {
                //tracks new selected item and item box
                m_selectedItemBox = itemBox;
                m_selectedItem = itemBox.GetItemScript();

                m_selectedItemBox.m_button.interactable = false; //marks new selected item box as unselectable

                UserInterface.Instance.m_inventoryMenuScreen.SetActive(true);   //shows inventory menu

                //sets interactability of buttons in accordance with the new selected item
                UserInterface.Instance.m_useButton.interactable = m_selectedItem.GetUseFromInventory();
                UserInterface.Instance.m_equipButton.interactable = m_selectedItem.GetEquippable();
                UserInterface.Instance.m_discardButton.interactable = m_selectedItem.GetRemovable();
            }
            else
            {
                UserInterface.Instance.m_inventoryMenuScreen.SetActive(false);  //hides inventory menu

                //stops tracking selected item and item box
                m_selectedItemBox = null;
                m_selectedItem = null;
            }


        }

        /// <summary>
        /// Deselects the current selected item
        /// </summary>
        public void DeselectSelectedItem()
        {
            if (m_selectedItem != null) { m_selectedItemBox.m_button.interactable = true; } //if there is a selected item, marks item box as selectable again

            //Stops tracking selected item and item box
            m_selectedItemBox = null;
            m_selectedItem = null;
        }

        #endregion Select and Deselect Item In Inventory


        #region Item Menu Functions

        /// <summary>
        /// Uses the selected item, and updates number of item (if applicable)
        /// </summary>
        public void UseSelectedItem()
        {
            m_selectedItem.Use();   //uses item

            if(m_selectedItem.GetConsumeAfterUse()) //consumes item (reduces number of item by one) if item is marked to be consumed.
            {
                if(m_selectedItemBox.removeInstanceOfItem())    //If item box is now empty, deselects item box
                {
                    UserInterface.Instance.m_inventoryMenuScreen.SetActive(false);  //hides inventory menu
                    m_selectedItemBox.m_button.interactable = true; //marks new selected item box as selectable again

                    //stops tracking selected item and item box
                    m_selectedItem = null;
                    m_selectedItemBox = null;
                }
            }
        }

        /// <summary>
        /// Equips selected item, and if an item is already equipped, returns that item to the inventory
        /// </summary>
        public void EqipSelectedItem()
        {
            if(m_selectedItemBox.GetItemIsWeapon())
            {
                EqipSelectedWeapon();
                return;
            }

            //check if anything is equipped
            if (m_equippedItemBox.GetNumberOfItems() > 0)   //if so, swaps the inventory item with the selected item
            {
                //store previous equipped item number
                int numSwapItem = m_equippedItemBox.GetNumberOfItems();


                //move selected item to equipped item box
                m_equippedItemBox.EmptyBox();
                m_equippedItemBox.AddItem(m_selectedItem);
                m_equippedItemBox.SetNumberOfItems(m_selectedItemBox.GetNumberOfItems());

                //return previous equipped item to inventory
                m_selectedItemBox.EmptyBox();
                m_selectedItemBox.AddItem(m_equippedItem);
                m_selectedItemBox.SetNumberOfItems(numSwapItem);

                //track new equippd item
                InventoryItem swapItem = m_equippedItem;    //store equipped item
                m_equippedItem = m_selectedItem;
                m_selectedItem = swapItem;
            }
            else    //if not, moves selected item to equipped item, and empties selected item box
            {
                //move selected item to equipped item box
                m_equippedItemBox.AddItem(m_selectedItem);
                m_equippedItemBox.SetNumberOfItems(m_selectedItemBox.GetNumberOfItems());

                //track new equippd item
                m_equippedItem = m_selectedItem;

                UserInterface.Instance.m_inventoryMenuScreen.SetActive(false);  //hides inventory menu
                m_selectedItemBox.m_button.interactable = true; //marks new selected item box as selectable again

                //tracks that there is no selected item
                m_selectedItemBox.EmptyBox();
                m_selectedItem = null;
                m_selectedItemBox = null;
            }
        }



        /// <summary>
        /// Equips selected weapon, and if a weapon is already equipped, returns that weapon to the inventory
        /// </summary>
        public void EqipSelectedWeapon()
        {
            WeaponItem selectedWeapon = m_selectedItem as WeaponItem;

            //check if anything is equipped
            if (selectedWeapon != null && m_equippedWeaponBox.GetNumberOfWeapons() > 0)   //if so, swaps the inventory item with the selected item
            {
                //store previous equipped item number
                int numSwapWeapon = m_equippedWeaponBox.GetNumberOfWeapons();


                //move selected item to equipped item box
                m_equippedWeaponBox.EmptyBox();
                m_equippedWeaponBox.AddWeapon(selectedWeapon);
                m_equippedWeaponBox.SetNumberOfWeapons(m_selectedItemBox.GetNumberOfItems());

                //return previous equipped weapon to inventory
                m_selectedItemBox.EmptyBox();
                m_selectedItemBox.AddItem(m_equippedWeapon);
                m_selectedItemBox.SetNumberOfItems(numSwapWeapon);

                //track new equippd item
                InventoryItem swapWeapon = m_equippedWeapon;    //store equipped item
                m_equippedWeapon = m_selectedItem;
                m_selectedItem = swapWeapon;
            }
            else    //if not, moves selected item to equipped item, and empties selected item box
            {
                //move selected item to equipped item box
                m_equippedWeaponBox.AddWeapon(selectedWeapon);
                m_equippedWeaponBox.SetNumberOfWeapons(m_selectedItemBox.GetNumberOfItems());

                //track new equippd item
                m_equippedWeapon = selectedWeapon;

                UserInterface.Instance.m_inventoryMenuScreen.SetActive(false);  //hides inventory menu
                m_selectedItemBox.m_button.interactable = true; //marks new selected item box as selectable again

                //tracks that there is no selected item
                m_selectedItemBox.EmptyBox();
                m_selectedItem = null;
                m_selectedItemBox = null;
            }
        }




        /// <summary>
        /// Removes the selected item from inventory
        /// </summary>
        public void RemoveSelectedItem()
        {
            UserInterface.Instance.m_inventoryMenuScreen.SetActive(false);  //hides inventory menu
            m_selectedItemBox.m_button.interactable = true; //marks new selected item box as selectable again


            m_selectedItemBox.EmptyBox();   //empties selected inventory item box

            //stops tracking selected item and item box
            m_selectedItem = null;
            m_selectedItemBox = null;
        }

        #endregion Item Menu Functions

    }
}
