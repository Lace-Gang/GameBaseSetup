using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class Inventory : MonoBehaviour
    {
        //private SerializableDictionary<int, int> m_inventory = new SerializableDictionary<int, int>();

        //Hidden Variables
        int m_inventorySpace = 0;
        int m_itemsInInventory = 0;

        //Hidden Variable Lists
        private List<InventoryItemBox> m_inventoryItemBoxes = new List<InventoryItemBox>();     //stores item boxes in the inventory


        //Exposed Variables
        [SerializeField] bool m_useInventory = false;




        public static Inventory Instance { get; private set; }  //Allows other scripts to get the singleton instance of the Inventory

        public bool GetUseInventory() { return m_useInventory; }    //Allows other scripts to see if inventory is being used

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

        // Update is called once per frame
        void Update()
        {
        
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

            //loops through all boxes and checks for the first box that this item can be added to
            foreach(InventoryItemBox box in m_inventoryItemBoxes)
            {
                //If item can be stacked, stacks item if box contains items of the same name
                if(stacking && box.GetItemName() == item.GetItemName())
                {
                    box.AddItem(item);
                    itemAdded = true;
                    break;
                }
                if(box.GetItemScript() == null) //adds item to first box that does not yet have an item
                {
                    box.AddItem(item);
                    m_itemsInInventory++;
                    itemAdded = true;
                    break;
                }
            }


            return itemAdded;
        }
    }
}
