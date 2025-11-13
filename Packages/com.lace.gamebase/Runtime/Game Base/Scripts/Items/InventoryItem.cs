using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
    public abstract class InventoryItem : SavableItem, IDataPersistence
    {
        //

        //Hidden Variables
        protected bool m_inInventory = false;
        protected bool m_equipped = true;
        //protected static int m_numItemInInventory = 0;  //How many instances of this item are in the inventory right now

        //Exposed Variables
        [Header("Inventory Item Information")]
        //[Tooltip("The Inventory ID of this item's group. Item ID MUST be the same for all instances of a given item that can stack. All items with the same ID MUST have the same " +
        //   "inventory related variables/properties, the same name, and excute the same function in the 'Use()' method, or else code will function unexpectedly!")]


        [Tooltip("The image that will be displayed for this item in the inventory screen (and HUD when equipped)")]
        [SerializeField] protected Sprite m_inventorySprite;

        [Tooltip("Can this item be used from the inventory")]
        [SerializeField] protected bool m_useFromInventory = false;
        [Tooltip("Can this item be equipped from inventory")]
        [SerializeField] protected bool m_equippable = true;
        //protected virtual bool m_equippable { get; set; } = false; // Initial value

        [Tooltip("Can thiprotected s item be removed from inventory")]
        [SerializeField] protected bool m_removable = true;
        [Tooltip("Can thiprotected s item be used from the inventory")]
        [SerializeField] protected bool m_consumeAfterUse = false;

        [Tooltip("Will this item be in inventory upon being spawned")]
        [SerializeField] protected bool m_startInInventory = false;
        //[Tooltip("Can multiple instances of this item exist in the inventory at one time")]
        //[SerializeField] protected bool m_allowMultipleInstancesInInventory = true;
        [Tooltip("Should the inventory stack instances of this item in the same box")]
        [SerializeField] protected bool m_stackInstancesInInventory = true;

        #region Getters and Setters
        //public static int GetNumberOfThisItemInInventory() { return m_numItemInInventory; }             //Allows other scripts to see how many instances of this item are currently in the inventory
        //public static void SetNumberOfThisItemInInventory(int num) {  m_numItemInInventory = num; }     //Allows other scripts to see how many instances of this item are currently in the inventory

        public bool GetInInventory() { return m_inInventory; }          //Allows other scripts to see if this item is in the inventory
        public Sprite GetInventorySprite() { return m_inventorySprite; } //Allows other scripts to get this item's sprite
        public string GetItemName() { return m_name; }                  //Allows other scripts to see the name of this item
        //public bool GetAllowMultipleInstances() { return m_allowMultipleInstancesInInventory; }     //Allows other scripts to see if more than one instance can be in the inventory at one time
        public bool GetStackInstances() { return m_stackInstancesInInventory; }                     //Allows other scripts to see if instances of this item should stack in the inventory
        public bool GetUseFromInventory() { return m_useFromInventory; }                            //Allows other scripts to see if instances of this item can be used from the inventory
        public bool GetEquippable() { return m_equippable; }                                        //Allows other scripts to see if instances of this item can be equipped
        public bool GetRemovable() { return m_removable; }                                          //Allows other scripts to see if instances of this item can be removed from the inventory
        public bool GetConsumeAfterUse() { return m_consumeAfterUse; }                              //Allows other scripts to see if instances of this item is consumed after being used



        public void SetItemName(string s) { m_name = s; }                  //Allows other scripts to see the name of this item
        public void SetStackInstances(bool b) { m_stackInstancesInInventory = b; }                     //Allows other scripts to see if instances of this item should stack in the inventory
        public void SetUseFromInventory(bool b) { m_useFromInventory = b; }                            //Allows other scripts to see if instances of this item can be used from the inventory
        public void SetEquippable(bool b) { m_equippable = b; }                                        //Allows other scripts to see if instances of this item can be equipped
        public void SetRemovable(bool b) { m_removable = b; }                                          //Allows other scripts to see if instances of this item can be removed from the inventory
        public void SetConsumeAfterUse(bool b) { m_consumeAfterUse = b; }                              //Allows other scripts to see if instances of this item is consumed after being used

        #endregion Getters and Setters

        /// <summary>
        /// Ensures that Inventory Item is correctly configured
        /// </summary>
        private void Awake()
        {
            //m_inInventory = m_startInInventory;

            if(m_name == null || m_name == "")
            {
                Debug.LogError("Inventory Item has no name! Item may behave incorrectly!");
            }
        }

        /// <summary>
        /// Item adds self to inventory and hides self in scene if space is available in inventory
        /// </summary>
        protected void AddToInventory()
        {
            if (Inventory.Instance.AddItemToInventory(this))
            {
                m_inInventory = true;
                HideItemInScene();
            }
            else
            {
                //if item is not added succesfully (ie the inventory was full) prompter needs to be reactivated
                ItemPickupPrompter prompter = GetComponentInChildren<ItemPickupPrompter>();
                if (prompter != null)
                {
                    prompter.SetPromptActive(true);
                }
            }
        }


        #region Overloaded Operators

        /// <summary>
        /// Checks if two inventory items have all the same inventory related properties
        /// </summary>
        /// <param name="a">First inventory item being compaired</param>
        /// <param name="b">Second inventory item being compaired</param>
        /// <returns>If they have the same inventory related properties</returns>
        public static bool operator ==(InventoryItem a, InventoryItem b)
        {
            return (a?.GetItemName() == b?.GetItemName() && a?.GetRemovable() == b?.GetRemovable() && a?.GetUseFromInventory() == b?.GetUseFromInventory() && a?.GetStackInstances() == b?.GetStackInstances()
                && a?.m_equippable == b?.m_equippable && a?.GetConsumeAfterUse() == b?.GetConsumeAfterUse()); // && a?.GetInventorySprite() == b?.GetInventorySprite());


            //return (a?.GetItemName() == b?.GetItemName() && a?.GetRemovable() == b?.GetRemovable() && a?.GetUseFromInventory() == b?.GetUseFromInventory() && a?.GetStackInstances() == b?.GetStackInstances()
            //   && a?.GetEquippable() == b?.GetEquippable() && a?.GetConsumeAfterUse() == b?.GetConsumeAfterUse() && a?.GetInventorySprite() == b?.GetInventorySprite());
        }

        /// <summary>
        /// Checks if two inventory items do not have all the same inventory related properties
        /// </summary>
        /// <param name="a">First inventory item being compaired<</param>
        /// <param name="b">Second inventory item being compaired</param>
        /// <returns>If they do not have the same inventory related properties</returns>
        public static bool operator !=(InventoryItem a, InventoryItem b)
        {
            return !(a?.GetItemName() == b?.GetItemName() && a?.GetRemovable() == b?.GetRemovable() && a?.GetUseFromInventory() == b?.GetUseFromInventory() && a?.GetStackInstances() == b?.GetStackInstances()
                && a?.m_equippable == b?.m_equippable && a?.GetConsumeAfterUse() == b?.GetConsumeAfterUse()); // && a?.GetInventorySprite() == b?.GetInventorySprite());

            //return !(a?.GetItemName() == b?.GetItemName() && a?.GetRemovable() == b?.GetRemovable() && a?.GetUseFromInventory() == b?.GetUseFromInventory() && a?.GetStackInstances() == b?.GetStackInstances()
            //    && a?.GetEquippable() == b?.GetEquippable() && a?.GetConsumeAfterUse() == b?.GetConsumeAfterUse() && a?.GetInventorySprite() == b?.GetInventorySprite());
        }

        #endregion Overloaded Operators



       #region Save and Load
       //public void SaveData(ref GameData data)
       //{
       //    base.SaveData(ref data);
       //
       //
       //
       //}
       //
       //public void LoadData(GameData data)
       //{
       //    base.LoadData(data);
       //}



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


        public void LoadForInventory(GameData data, string boxID)
        {
            m_activeInScene = false;

            //Load name
            if (data.stringData.ContainsKey(boxID + ".ItemName"))
            {
                m_name = data.stringData[boxID + ".ItemName"];
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
        }
       
       
       #endregion Save and Load


        /// <summary>
        /// Item attempts to add self to inventory
        /// </summary>
        public override void OnPickedUp()
        {
            AddToInventory();
        }


        public abstract override void Use();
    }
}
