using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
    public abstract class InventoryItem : SavableItem, IDataPersistence
    {
        //Hidden Variables
        protected bool m_inInventory = false;   //Is this item currently in the inventory
        protected bool m_equipped = false;      //Is this item currently equipped

        #region Exposed In Editor Variables

        //Exposed Variables
        [Header("Inventory Item Information")]
        [Tooltip("Used to tell appart different configurations of the same item script in the inventory. Please set an InventoryID for each distinct configuration. And please use the same InventoryID" +
            " for all instances of objects that share a distinct configuration.")]
        [SerializeField] protected int m_inventoryID = 0;


        [Tooltip("The image that will be displayed for this item in the inventory screen (and HUD when equipped)")]
        [SerializeField] protected Sprite m_inventorySprite;

        [Tooltip("Can this item be used from the inventory")]
        [SerializeField] protected bool m_useFromInventory = false;
        [Tooltip("Can this item be equipped from inventory")]
        [SerializeField] protected bool m_equippable = true;

        [Tooltip("Can thiprotected s item be removed from inventory")]
        [SerializeField] protected bool m_removable = true;
        [Tooltip("Can thiprotected s item be used from the inventory")]
        [SerializeField] protected bool m_consumeAfterUse = false;

        [Tooltip("Should the inventory stack instances of this item in the same box")]
        [SerializeField] protected bool m_stackInstancesInInventory = true;

        #endregion Exposed In Editor Variables


        #region Getters and Setters

        public bool GetInInventory() { return m_inInventory; }                  //Allows other scripts to see if this item is in the inventory
        public Sprite GetInventorySprite() { return m_inventorySprite; }        //Allows other scripts to get this item's sprite
        public string GetItemName() { return m_name; }                          //Allows other scripts to see the name of this item
        public int GetInventoryID() { return m_inventoryID; }                   //Allows other scripts to see the inventoryID of this item
        public bool GetStackInstances() { return m_stackInstancesInInventory; } //Allows other scripts to see if instances of this item should stack in the inventory
        public bool GetUseFromInventory() { return m_useFromInventory; }        //Allows other scripts to see if instances of this item can be used from the inventory
        public bool GetEquippable() { return m_equippable; }                    //Allows other scripts to see if instances of this item can be equipped
        public bool GetRemovable() { return m_removable; }                      //Allows other scripts to see if instances of this item can be removed from the inventory
        public bool GetConsumeAfterUse() { return m_consumeAfterUse; }          //Allows other scripts to see if instances of this item is consumed after being used


        public void SetEquipped(bool equipped) { m_equipped = equipped; }           //Allows other scripts to set if this item has been equipped
        public void SetItemName(string s) { m_name = s; }                           //Allows other scripts to set the name of this item
        public void SetStackInstances(bool b) { m_stackInstancesInInventory = b; }  //Allows other scripts to set if instances of this item should stack in the inventory
        public void SetUseFromInventory(bool b) { m_useFromInventory = b; }         //Allows other scripts to set if instances of this item can be used from the inventory
        public void SetEquippable(bool b) { m_equippable = b; }                     //Allows other scripts to set if instances of this item can be equipped
        public void SetRemovable(bool b) { m_removable = b; }                       //Allows other scripts to set if instances of this item can be removed from the inventory
        public void SetConsumeAfterUse(bool b) { m_consumeAfterUse = b; }           //Allows other scripts to set if instances of this item is consumed after being used

        #endregion Getters and Setters

        /// <summary>
        /// Ensures that Inventory Item is correctly configured
        /// </summary>
        private void Awake()
        {
            //warn user if this inventory item was not given a name
            if(m_name == null || m_name == "")
            {
                Debug.LogError("Inventory Item has no name! Item may behave incorrectly!");
            }
        }

        #region InventoryItem Basic Functionality

        /// <summary>
        /// Item adds self to inventory and hides self in scene if space is available in inventory
        /// </summary>
        protected void AddToInventory()
        {
            if (Inventory.Instance.AddItemToInventory(this))
            {
                //If item successfully added to inventory, hides item in scene and tracks that item is in inventory
                m_inInventory = true;   //tracks item is in inventory
                HideItemInScene();  //hides item in scene
            }
            else
            {
                //if item is not added succesfully (ie if the inventory was full) check if this item has an ItemPickupPrompter component
                ItemPickupPrompter prompter = GetComponentInChildren<ItemPickupPrompter>();
                if (prompter != null)
                {
                    prompter.SetPromptActive(true); //reactivates ItemPickupPrompter if one was found
                }
            }
        }

        /// <summary>
        /// Item attempts to add self to inventory
        /// </summary>
        public override void OnPickedUp()
        {
            //If inventory is being used, attempts to add self to inventory
            if(Inventory.Instance.GetUseInventory())
            {
                AddToInventory();
            }
            else    //Otherwise, uses this item and then hides it in the scene
            {
                Use();
                HideItemInScene();
            }
        }

        #endregion InventoryItem Basic Functionality


        #region Overloaded Operators

        /// <summary>
        /// Checks if two inventory items have all the same inventory related properties
        /// </summary>
        /// <param name="a">First inventory item being compaired</param>
        /// <param name="b">Second inventory item being compaired</param>
        /// <returns>If they have the same inventory related properties</returns>
        public static bool operator ==(InventoryItem a, InventoryItem b)
        {
            return (a?.m_name == b?.m_name && a?.m_inventoryID == b?.m_inventoryID && a?.m_removable == b?.m_removable && a?.m_useFromInventory == b?.m_useFromInventory && a?.m_stackInstancesInInventory == b?.m_stackInstancesInInventory
                && a?.m_equippable == b?.m_equippable && a?.m_consumeAfterUse == b?.m_consumeAfterUse);
        }

        /// <summary>
        /// Checks if two inventory items do not have all the same inventory related properties
        /// </summary>
        /// <param name="a">First inventory item being compaired<</param>
        /// <param name="b">Second inventory item being compaired</param>
        /// <returns>If they do not have the same inventory related properties</returns>
        public static bool operator !=(InventoryItem a, InventoryItem b)
        {
            return !(a?.m_name == b?.m_name && a?.m_inventoryID == b?.m_inventoryID && a?.m_removable == b?.m_removable && a?.m_useFromInventory == b?.m_useFromInventory && a?.m_stackInstancesInInventory == b?.m_stackInstancesInInventory
                && a?.m_equippable == b?.m_equippable && a?.m_consumeAfterUse == b?.m_consumeAfterUse);
        }


        /// <summary>
        /// Executes the base .Equals operator
        /// </summary>
        /// <param name="obj">The object being compaired</param>
        /// <returns>If they are the same object</returns>
        public override bool Equals(System.Object obj)
        {
            //return results of base .Equals opperator
            return base.Equals(obj);
        }

        #endregion Overloaded Operators



        #region Save and Load

        /// <summary>
        /// Saves the inventory related data for the specific Inventory Item Box that this item is in
        /// </summary>
        /// <param name="data">Reference to the GameData object with the data being savedparam>
        /// <param name="boxID">The unique ID of the Inventory Item Box this item is in</param>
        public void SaveForInventory(ref GameData data, string boxID)
        {
            //Save name
            if (data.stringData.ContainsKey(boxID +".ItemName"))
            {
                data.stringData[boxID + ".ItemName"] = m_name;
            }
            else
            {
                data.stringData.Add(boxID + ".ItemName", m_name);
            }

            //Save inventory ID
            if (data.intData.ContainsKey(boxID + ".InventoryID"))
            {
                data.intData[boxID + ".InventoryID"] = m_inventoryID;
            }
            else
            {
                data.intData.Add(boxID + ".InventoryID", m_inventoryID);
            }

            //Save usable from inventory
            if (data.boolData.ContainsKey(boxID + ".ItemUsableFromInventory"))
            {
                data.boolData[boxID + ".ItemUsableFromInventory"] = m_useFromInventory;
            }
            else
            {
                data.boolData.Add(boxID + ".ItemUsableFromInventory", m_useFromInventory);
            }

            //Save equippable
            if (data.boolData.ContainsKey(boxID + ".ItemEquippable"))
            {
                data.boolData[boxID + ".ItemEquippable"] = m_equippable;
            }
            else
            {
                data.boolData.Add(boxID + ".ItemEquippable", m_equippable);
            }

            //Save removable
            if (data.boolData.ContainsKey(boxID + ".ItemRemovable"))
            {
                data.boolData[boxID + ".ItemRemovable"] = m_removable;
            }
            else
            {
                data.boolData.Add(boxID + ".ItemRemovable", m_removable);
            }

            //Save consume after use
            if (data.boolData.ContainsKey(boxID + ".ItemConsumeAfterUse"))
            {
                data.boolData[boxID + ".ItemConsumeAfterUse"] = m_consumeAfterUse;
            }
            else
            {
                data.boolData.Add(boxID + ".ItemConsumeAfterUse", m_consumeAfterUse);
            }

            //Save stack insances
            if (data.boolData.ContainsKey(boxID + ".ItemStackInstances"))
            {
                data.boolData[boxID + ".ItemStackInstances"] = m_stackInstancesInInventory;
            }
            else
            {
                data.boolData.Add(boxID + ".ItemStackInstances", m_stackInstancesInInventory);
            }
        }

        /// <summary>
        /// Loads the inventory related data base on the specific Inventory Item Box that this item is in
        /// </summary>
        /// <param name="data">GameData object with the data being loaded</param>
        /// <param name="boxID">The unique ID of the Inventory Item Box this item is in</param>
        public void LoadForInventory(GameData data, string boxID)
        {
            m_activeInScene = false;

            //Load name
            if (data.stringData.ContainsKey(boxID + ".ItemName"))
            {
                m_name = data.stringData[boxID + ".ItemName"];
            }

            //Load inventory ID
            if (data.intData.ContainsKey(boxID + ".InventoryID"))
            {
                m_inventoryID = data.intData[boxID + ".InventoryID"];
            }

            //Load usable from inventory
            if (data.boolData.ContainsKey(boxID + ".ItemUsableFromInventory"))
            {
                m_useFromInventory = data.boolData[boxID + ".ItemUsableFromInventory"];
            }

            //Load equippable
            if (data.boolData.ContainsKey(boxID + ".ItemEquippable"))
            {
                m_equippable = data.boolData[boxID + ".ItemEquippable"];
            }

            //Load removable
            if (data.boolData.ContainsKey(boxID + ".ItemRemovable"))
            {
                m_removable = data.boolData[boxID + ".ItemRemovable"];
            }

            //Load consume after use
            if (data.boolData.ContainsKey(boxID + ".ItemConsumeAfterUse"))
            {
                m_consumeAfterUse = data.boolData[boxID + ".ItemConsumeAfterUse"];
            }

            //Load stack instances
            if (data.boolData.ContainsKey(boxID + ".ItemStackInstances"))
            {
                m_stackInstancesInInventory = data.boolData[boxID + ".ItemStackInstances"];
            }


            HideItemInScene();  //Hide item in scene (because it has already been picked up)           
        }
       
       
       #endregion Save and Load



        /// <summary>
        /// Uses this InventoryItem
        /// </summary>
        public abstract override void Use();
    }
}
