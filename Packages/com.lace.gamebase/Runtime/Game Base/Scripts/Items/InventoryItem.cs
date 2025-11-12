using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
    public abstract class InventoryItem : SavableItem
    {
        //Hidden Variables
        protected bool m_inInventory = false;
        protected static int m_numItemInInventory = 0;  //How many instances of this item are in the inventory right now

        //Exposed Variables
        [Header("Inventory Item Information")]
        [Tooltip("The image that will be displayed for this item in the inventory screen (and HUD when equipped)")]
        [SerializeField] protected Sprite m_inventorySprite;

        [Tooltip("Can this item be used from the inventory")]
        [SerializeField] bool m_useFromInventory = false;
        [Tooltip("Can this item be equipped from inventory")]
        [SerializeField] bool m_equippable = true;
        [Tooltip("Can this item be removed from inventory")]
        [SerializeField] bool m_removable = true;
        [Tooltip("Can this item be used from the inventory")]
        [SerializeField] bool m_consumeAfterUse = false;

        [Tooltip("Will this item be in inventory upon being spawned")]
        [SerializeField] protected bool m_startInInventory = false;
        //[Tooltip("Can multiple instances of this item exist in the inventory at one time")]
        //[SerializeField] protected bool m_allowMultipleInstancesInInventory = true;
        [Tooltip("Should the inventory stack instances of this item in the same box")]
        [SerializeField] protected bool m_stackInstancesInInventory = true;

        #region Getters and Setters
        public static int GetNumberOfThisItemInInventory() { return m_numItemInInventory; }             //Allows other scripts to see how many instances of this item are currently in the inventory
        public static void SetNumberOfThisItemInInventory(int num) {  m_numItemInInventory = num; }     //Allows other scripts to see how many instances of this item are currently in the inventory

        public bool GetInInventory() { return m_inInventory; }          //Allows other scripts to see if this item is in the inventory
        public Sprite GetInventorySprite() { return m_inventorySprite; } //Allows other scripts to get this item's sprite
        public string GetItemName() { return m_name; }                  //Allows other scripts to see the name of this item
        //public bool GetAllowMultipleInstances() { return m_allowMultipleInstancesInInventory; }     //Allows other scripts to see if more than one instance can be in the inventory at one time
        public bool GetStackInstances() { return m_stackInstancesInInventory; }                     //Allows other scripts to see if instances of this item should stack in the inventory
        public bool GetUseFromInventory() { return m_useFromInventory; }                            //Allows other scripts to see if instances of this item can be used from the inventory
        public bool GetEquippable() { return m_equippable; }                                        //Allows other scripts to see if instances of this item can be equipped
        public bool GetRemovable() { return m_removable; }                                          //Allows other scripts to see if instances of this item can be removed from the inventory
        public bool GetConsumeAfterUse() { return m_consumeAfterUse; }                              //Allows other scripts to see if instances of this item is consumed after being used
        
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

        /// <summary>
        /// Checks if two inventory items have all the same inventory related properties
        /// </summary>
        /// <param name="a">First inventory item being compaired</param>
        /// <param name="b">Second inventory item being compaired</param>
        /// <returns>If they have the same inventory related properties</returns>
        public static bool operator ==(InventoryItem a, InventoryItem b)
        {
            return (a?.GetItemName() == b?.GetItemName() && a?.GetRemovable() == b?.GetRemovable() && a?.GetUseFromInventory() == b?.GetUseFromInventory() && a?.GetStackInstances() == b?.GetStackInstances()
                && a?.GetEquippable() == b?.GetEquippable() && a?.GetConsumeAfterUse() == b?.GetConsumeAfterUse() && a?.GetInventorySprite() == b?.GetInventorySprite());
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
                && a?.GetEquippable() == b?.GetEquippable() && a?.GetConsumeAfterUse() == b?.GetConsumeAfterUse() && a?.GetInventorySprite() == b?.GetInventorySprite());
        }





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
