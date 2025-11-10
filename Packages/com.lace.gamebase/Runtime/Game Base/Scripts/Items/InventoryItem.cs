using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
    public class InventoryItem : SavableItem
    {
        //Hidden Variables
        protected bool m_inInventory = false;
        protected static int m_numItemInInventory = 0;  //How many instances of this item are in the inventory right now

        //Exposed Variables
        [Header("Inventory Item Information")]
        [Tooltip("The image that will be displayed for this item in the inventory screen (and HUD when equipped)")]
        [SerializeField] protected Sprite m_inventorySprite;
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
            if(Inventory.Instance.AddItemToInventory(this))
            {
                m_inInventory = true;
                HideItemInScene();
            }
        }


        /// <summary>
        /// Item attempts to add self to inventory
        /// </summary>
        public override void OnPickedUp()
        {
            AddToInventory();
        }


        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}
