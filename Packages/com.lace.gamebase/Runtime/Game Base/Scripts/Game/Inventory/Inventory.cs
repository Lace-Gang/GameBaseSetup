using NUnit.Framework.Internal;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace GameBase
{
    public class Inventory : MonoBehaviour, IDataPersistence
    {
        //private SerializableDictionary<int, int> m_inventory = new SerializableDictionary<int, int>();

        //Hidden Variables
        int m_inventorySpace = 0;
        int m_itemsInInventory = 0;

        InventoryItem m_selectedItem;
        InventoryItemBox m_selectedItemBox = null;
        InventoryItem m_equippedItem;

        //Hidden Variable Lists
        private List<InventoryItemBox> m_inventoryItemBoxes = new List<InventoryItemBox>();     //stores item boxes in the inventory


        //Exposed Variables
        [SerializeField] bool m_useInventory = false;
        [SerializeField] EquippedItemBox m_equippedItemBox = null;

        [SerializeField] List<InventoryItem> m_availableInventoryItems = new List<InventoryItem>();

        //[SerializeField] List<Script> m_allScripts = new List<MonoBehaviour>();



        [Tooltip("If equipped item box is empty, next item added to inventory will be automatically equipped")]
        [SerializeField] bool m_sendFirstItemToEquipped = false;




        public static Inventory Instance { get; private set; }  //Allows other scripts to get the singleton instance of the Inventory

        public bool GetUseInventory() { return m_useInventory; }    //Allows other scripts to see if inventory is being used

        public void SetEquippedItem(InventoryItem i) { m_equippedItem = i; }

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
            //Generate Inventory screen and get references to item boxes
            m_inventoryItemBoxes = UserInterface.Instance.GenerateInventoryBox();

            //track available inventory space
            m_inventorySpace = m_inventoryItemBoxes.Count;
        }



        public InventoryItem FindItemByName(string name)
        {
            foreach(InventoryItem itemPrefab in m_availableInventoryItems)
            {
                if(itemPrefab.GetComponent<InventoryItem>() != null && itemPrefab.GetComponent<InventoryItem>().GetType().Name == name)
                { 
                    return itemPrefab;
                }
            }

            return null;
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

            //Auto-equips item if no item is equipped, and inventory is instructed to do so
            if (m_equippedItem == null && m_sendFirstItemToEquipped)   
            {
                m_equippedItemBox.AddItem(item);    //equip item
                m_equippedItem = item;              //track equipped item
                itemAdded = true;
            }
            else if (m_equippedItem != null && stacking && m_equippedItem == item)  //Checks if new item is the same as the equipped item, and adds to equipped item stack if so
            {
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
                        box.AddItem(item);
                        itemAdded = true;
                        break;
                    }
                    if (box.GetItemScript() == null) //adds item to first box that does not yet have an item
                    {
                        box.AddItem(item);
                        m_itemsInInventory++;
                        itemAdded = true;
                        break;
                    }
                }
            }


            return itemAdded;   //indicate if item was able to be added
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

        /// <summary>
        /// Sets equipped item tracker to null
        /// </summary>
        public void OnEquippedItemEmpty()
        {
            m_equippedItem = null;
        }



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
            //check if anything is equipped
            if (m_equippedItemBox.GetNumberOfItems() > 0)   //if so, swaps the inventory item with the selected item
            {
                //store previous equipped item number
                int numSwapItem = m_equippedItemBox.GetNumberOfItems();


                //move selected item to equipped item box
                m_equippedItemBox.EmptyBox();
                m_equippedItemBox.AddItem(m_selectedItem);
                m_equippedItemBox.SetNumberOfItems(m_selectedItemBox.GetNumberOfItems());

                //return previous inventory item to inventory
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
        /// Removes the selected item from inventory
        /// </summary>
        public void RemoveSelectedItem()
        {
            UserInterface.Instance.m_inventoryMenuScreen.SetActive(false);  //hides inventory menu
            m_selectedItemBox.m_button.interactable = true; //marks new selected item box as selectable again


            m_selectedItemBox.EmptyBox();   //empties selected inventory item box
            m_itemsInInventory--;           //tracks number of items in inventory

            //stops tracking selected item and item box
            m_selectedItem = null;
            m_selectedItemBox = null;
        }


        public void SaveData(ref GameData data)
        {
            //if(m_equippedItemBox.GetNumberOfItems() > 0)
            //{
            //    //Check stringData for key. If key exists, change value to current value, else add key with current value
            //    if (data.stringData.ContainsKey("EquipedItemBox.ItemScript"))
            //    {
            //        data.stringData["EquipedItemBox.ItemScript"] = m_equippedItemBox.GetItemScript().GetType().Name;
            //    }
            //    else
            //    {
            //        data.stringData.Add("EquipedItemBox.ItemScript", m_equippedItemBox.GetItemScript().GetType().Name);
            //    }
            //
            //    //Check intData for key. If key exists, change value to current value, else add key with current value
            //    if (data.intData.ContainsKey("EquipedItemBox.NumberOfItems"))
            //    {
            //        data.intData["EquipedItemBox.NumberOfItems"] = m_equippedItemBox.GetNumberOfItems();
            //    }
            //    else
            //    {
            //        data.intData.Add("EquipedItemBox.NumberOfItems", m_equippedItemBox.GetNumberOfItems());
            //    }
            //}



            //throw new System.NotImplementedException();
        }

        public void LoadData(GameData data)
        {
            //TestItem t = new TestItem();
            //t.SetItemName("Test Item");
            //t.SetUseFromInventory(false);
            //t.SetEquippable(true);
            //t.SetRemovable(true);
            //t.SetConsumeAfterUse(false);
            //t.SetStackInstances(true);
            //
            //m_equippedItemBox.AddItem(t);
            //m_equippedItem = t;
            ////throw new System.NotImplementedException();
        }

    }
}
