using UnityEngine;

namespace GameBase
{
    public class WeaponItem : InventoryItem
    {
        //Hidden Variables
        protected int m_ammoAmount = -1;                       //Current amount of amo
        protected GameObject m_weaponMesh;                     //The mesh of the weapon being held in this weapon item
        protected AmmunitionTracker m_ammoTracker = null;      //The AmmunitionTracker for the ammunition that this weapon item's weapon uses (if applicable)


        //Exposed Variables
        [Header("Weapon Pikcup Item Details")]
        [Tooltip("The weapon being stored in this item")]
        [SerializeField] WeaponBase m_weapon;

        

        /// <summary>
        /// If this WeaponItem has a weapon, gets the AmmunitionTracker of that weapon
        /// </summary>
        private void Start()
        {
            m_ammoTracker = m_weapon?.GetAmmunitionTracker();
        }


        #region Getters and Setters

        public string GetWeaponName() { return m_weapon?.GetWeaponName(); }      //Allows other scripts to get the name of the weapon being held in this item
        public AmmunitionTracker GetAmmunitionTracker() { return m_weapon?.GetAmmunitionTracker(); }    //Lets other scripts get the ammunition tracker of the weapon this WeaponItem represents

        /// <summary>
        /// Allows other scripts to see the current ammount of Ammunition that the weapon held in this item's AmmunitionTracker has
        /// </summary>
        /// <returns>Amount of ammunition</returns>
        public int GetAmmoAmount() 
        {
            //if weapon does not have an AmmunitionTracker, returns -1 to indicate that no ammunition is being used
            if (m_ammoTracker == null) return -1;

            //Otherwise, returns the current ammount of Ammunition that the weapon's AmmunitionTracker has
            m_ammoAmount = m_ammoTracker.GetAmmunitionAmount(); return m_ammoAmount;
        }

        #endregion Getters and Setters



        #region WeaponItem Basic Functionality

        /// <summary>
        /// Tells player to equip this weapon
        /// </summary>
        public void EquipWeapon()
        {
            //Only executes if this WeaponItem currently has a weapon
            if (m_weapon != null)
            {
                GameInstance.Instance.GetPlayerScript()?.EquipWeapon(m_weapon); //tells player to equip this WeaponItem's weapon

                m_equipped = true;  //indicates that this WeaponItem has been equipped

                m_weapon.ShowWeapon();  //Ensures this WeaponItem's weapon is now visible
            }          
        }

        /// <summary>
        /// Currently, does nothing. This can be changed if desired
        /// </summary>
        public override void Use()
        {
            //Does nothing, but can be overriden if desired
        }

        /// <summary>
        /// Tells this item to be "picked up"
        /// </summary>
        public override void OnPickedUp()
        {
            base.OnPickedUp();      //Does all "OnPickedUp" opperations associated with the InventoryItem

            if(m_equipped) m_weapon.ShowWeapon();   //Shows weapon prefab that is displayed by this item if this item is equipped
            else m_weapon.HideWeapon();             //Hides this WeaponItem's weapon if it is not equipped
        }

        #endregion WeaponItem Basic Functionality
    }
}
