using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
    public class EquippedWeaponBox : MonoBehaviour, IDataPersistence
    {
        //Hidden Variables
        private string m_itemName = "";           //Name of the weaponItem currently being stored in this Equipped Weapon Box
        private string m_weaponName = "";         //Name of the weapon currently being stored in this Equipped Weapon Box
        private int m_numWeapons = 0;             //Number of items currently being stored in this Equipped Weapon Box
        private int m_ammoAmount = 0;             //Amount of amo this weapon has
        private WeaponItem m_weaponItem = null;       //Script of the item currently being stored in this Equipped Weapon Box
        private Sprite m_weaponSprite;            //The sprite of the item burrently being stored in this Equipped Weapon Box

        [Header("Important Components")]
        [Tooltip("Image component to display item sprite")]
        [SerializeField] Image m_image;
        [Tooltip("Rect transform component of this Equipped Weapon Box")]
        [SerializeField] RectTransform m_rectTransform;
        [Tooltip("Text box to display weapon name")]
        [SerializeField] TextMeshProUGUI m_nameText;
        [Tooltip("Text box to display number of weapons")]
        [SerializeField] TextMeshProUGUI m_numberText;
        [Tooltip("Text used to indicate remaining amount of ammunition (if applicable)")]
        [SerializeField] TextMeshProUGUI m_ammunitionText;
        




        #region Awake Start and Update

        /// <summary>
        /// Hides empty image
        /// </summary>
        private void Awake()
        {
            m_image.enabled = false;
        }

       /// <summary>
       /// Adjusts the UI to properly indicate which key must be pressed to use the Equipped Item
       /// </summary>
       private void Start()
       {
           //m_keyIndicatorText.text = m_useKey.ToString();
           m_ammunitionText.text = string.Empty;
       }



        /// <summary>
        /// Checks if player uses equipped item, uses item if so
        /// </summary>
        private void Update()
        {
            ////only uses item if correct key is down, there is an equipped item, and the player is alive
            //if (Input.GetKeyDown(m_useKey) && m_item != null && GameInstance.Instance.getPlayerAlive())
            //{
            //    m_item.Use();
            //
            //    if (m_item.GetConsumeAfterUse()) //consumes item (reduces number of item by one) if item is marked to be consumed.
            //    {
            //        if (removeInstanceOfItem())    //If all instances of the equipped item were used up, empties and updates equipped item box
            //        {
            //            //empties box and shows that box is empty
            //            EmptyBox();
            //
            //            //notifies Inventory that the equipped item box is now empty
            //            Inventory.Instance.OnEquippedItemEmpty();
            //        }
            //    }
            //}
        }

        #endregion Awake Start and Update



        #region Getters and Setters
        public string GetItemName() { return m_itemName; }          //Allows other scripts to get the name of the item(s) being stored in this box
        public string GetWeaponName() { return m_weaponName; }          //Allows other scripts to get the name of the item(s) being stored in this box
        public WeaponItem GetItemScript() { return m_weaponItem; }     //Allows other scripts to get the script of this item being held in this box
        public int GetNumberOfWeapons() { return m_numWeapons; }        //Allows other scripts to get how many items this box currently contains


        public void SetNumberOfWeapons(int numWeapons) { m_numWeapons = numWeapons; UpdateBox(); }          //Allows other scripts to get how many items this box currently contains

        /// <summary>
        /// Sets this box's image
        /// </summary>
        /// <param name="image">The desired image</param>
        public void SetImage(Image image)
        {
            m_image.sprite = m_weaponSprite;
        }
        #endregion Getters and Setters


        #region Equipped Weapon Box Functionality


        /// <summary>
        /// Removes all item instances from this box, and resets all values in this box
        /// </summary>
        public void EmptyBox()
        {
            m_weaponItem = null;
            m_itemName = string.Empty;
            m_weaponName = string.Empty;
            m_numWeapons = 0;
            m_ammoAmount = 0;
            m_weaponSprite = null;

            UpdateBox();
        }

        /// <summary>
        /// Removes one instance of this item box's item. 
        /// </summary>
        /// <returns>If this box is empty</returns>
        public bool removeInstanceOfItem()
        {
            m_numWeapons--;

            if (m_numWeapons <= 0)
            {
                EmptyBox();
                return true;
            }

            UpdateBox();
            return false;
        }


        /// <summary>
        /// Adds weaponItem to this equipped weapon box
        /// </summary>
        /// <param name="weaponItem">Weapon Item being added</param>
        public void AddWeapon(WeaponItem weaponItem)
        {
            m_weaponItem = weaponItem;
            m_itemName = weaponItem.GetItemName();
            m_weaponName = weaponItem.GetWeaponName();
            m_weaponSprite = weaponItem.GetInventorySprite();
            m_ammoAmount = weaponItem.GetWeaponAmo();

            m_numWeapons++;   //increment number of weapons stored in the box

            UpdateBox();    //update box to propery display weapon
        }



        /// <summary>
        /// Updates the equipped weapon box appearance in the UI
        /// </summary>
        public void UpdateBox()
        {
            m_image.sprite = m_weaponSprite;
            m_nameText.text = (m_numWeapons <= 0) ? string.Empty : (m_weaponName != "" && m_weaponName != string.Empty)? m_weaponName : m_itemName;
            m_numberText.text = (m_numWeapons > 1) ? m_numWeapons.ToString() : string.Empty;
            m_ammunitionText.text = (m_ammoAmount > 1) ? m_ammoAmount.ToString() : string.Empty;

            if (m_weaponSprite == null)
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

        #endregion Equipped Weapon Box Functionality


        #region Save and Load

        /// <summary>
        /// Saves this item, as well as all inventory related data for the item in this equipped item box (if this box currently contains at least one item)
        /// </summary>
        /// <param name="data">Reference to the GameData object with the data being saved</param>
        public void SaveData(ref GameData data)
        {
            //Saves number of items currently stored in this Equipped Item Box
            if (data.intData.ContainsKey("EquippedWeaponBox.NumberOfItems"))
            {
                data.intData["EquippedWeaponBox.NumberOfItems"] = m_numWeapons;
            }
            else
            {
                data.intData.Add("EquippedWeaponBox.NumberOfItems", m_numWeapons);
            }


            if (m_numWeapons > 0)
            {
                //Save name of item's item script (if this Equipped Item Box currently has an item)
                if (data.stringData.ContainsKey("EquippedWeaponBox.ItemScript"))
                {
                    data.stringData["EquippedWeaponBox.ItemScript"] = m_weaponItem.GetType().Name;
                }
                else
                {
                    data.stringData.Add("EquippedWeaponBox.ItemScript", m_weaponItem.GetType().Name);
                }

                //Saves the inventory related information for the item in this Equipped Item Box
                m_weaponItem.SaveForInventory(ref data, "EquippedWeapon");
            }
        }

        /// <summary>
        /// Loads this equipped item box, and if this box contains an item, creates and loads that item
        /// </summary>
        /// <param name="data">GameData object with the data being loaded</param>
        public void LoadData(GameData data)
        {
            //Check if key exists in intData, if so continue loading if not then do nothing
            if (data.intData.ContainsKey("EquippedWeaponBox.NumberOfItems"))
            {
                m_numWeapons = data.intData["EquippedWeaponBox.NumberOfItems"];

                //only continue loading if Equipped item box had any items to load
                if(m_numWeapons > 0)
                {
                    //Loads the name of the specific Inventory Item script for the item that was stored in this box
                    if (data.stringData.ContainsKey("EquippedWeaponBox.ItemScript"))
                    {
                        string scriptName = data.stringData["EquippedWeaponBox.ItemScript"];

                        string itemName = "";
                        
                        if (data.stringData.ContainsKey("EquippedWeapon.ItemName"))
                        {
                            itemName = data.stringData["EquippedWeapon.ItemName"];
                        }
                        
                        //Obtains stored item script from the Inventory
                        WeaponItem weaponScript = Inventory.Instance.FindItemByScriptNameAndItemName(scriptName, itemName) as WeaponItem;

                        //If a script was found, creates a new instance of that item to be stored in this Equipped Item Box
                        if (weaponScript != null)
                        {
                            WeaponItem newWeapon = GameObject.Instantiate(weaponScript); //Creates item

                            //WeaponItem newWeapon = newItem as WeaponItem;

                            newWeapon.LoadForInventory(data, "EquippedWeapon"); //Loads data into item

                            //Properly adds item to box
                            m_weaponItem = newWeapon;
                            m_itemName = newWeapon.GetItemName();
                            m_weaponSprite = newWeapon.GetInventorySprite();

                            //Updates Box to properly reflect the current item
                            UpdateBox();

                            //Notifies Inventory of the new equipped item
                            Inventory.Instance.SetEquippedItem(newWeapon);
                        }
                        else
                        {
                            m_numWeapons = 0; //if no item script was found, then no item can be loaded, and the number of items in this Inventory Item Box must be returned to zero
                        }
                    }
                }                     
            }
        }
        #endregion Save and Load

    }
}
