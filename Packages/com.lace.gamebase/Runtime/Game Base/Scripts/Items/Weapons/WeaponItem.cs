using UnityEngine;

namespace GameBase
{
    public class WeaponItem : InventoryItem
    {
        //Hidden Variables
        //protected string m_weaponName = string.Empty;   //Name of the weapon being held in this weapon item
        protected int m_ammoAmount = -1;                       //Current amount of amo
        protected GameObject m_weaponMesh;              //The mesh of the weapon being held in this weapon item
        protected AmmunitionTracker m_ammoTracker = null;


        //Exposed Variables
        [Header("Weapon Pikcup Item Details")]
        [Tooltip("The weapon being stored in this item")]
        [SerializeField] WeaponBase m_weapon;

        ///// <summary>
        ///// Tracks the name of the weapon
        ///// </summary>
        //private void Start()
        //{
        //    //m_weaponName = m_weaponScript.GetWeaponName();
        //    m_weaponName = m_weapon.GetWeaponName();
        //}


        private void Start()
        {
            m_ammoTracker = m_weapon?.GetAmmunitionTracker();
        }


        public string GetWeaponName() { return m_weapon?.GetWeaponName(); }      //Allows other scripts to get the name of the weapon being held in this item
        public AmmunitionTracker GetAmmunitionTracker() { return m_ammoTracker; }

        //public int GetAmmoAmount() {  return m_ammoAmount; }                    //Allows other scripts to see how much amo the weapon being held in this item still has
        public int GetAmmoAmount() {
            if (m_ammoTracker == null) return -1;
            m_ammoAmount = m_ammoTracker.GetAmmunitionAmount(); return m_ammoAmount;
        }                    //Allows other scripts to see how much amo the weapon being held in this item still has

        //public int CheckAmoAmount() { m_ammoAmount = m_weapon.GetAmmoAmount(); return m_ammoAmount; }   //Allows other scripts to set the amount of ammo the weapon being held in this item sti
        public int CheckAmoAmount() 
        {
            if (m_ammoTracker == null) return -1;
            m_ammoAmount = m_ammoTracker.GetAmmunitionAmount(); return m_ammoAmount; 
        }   //Allows other scripts to set the amount of ammo the weapon being held in this item sti

        




        /// <summary>
        /// Tells player to equip this weapon
        /// </summary>
        public void EquipWeapon()
        {
            if(m_weapon != null) GameInstance.Instance.GetPlayerScript()?.EquipWeapon(m_weapon);
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
            m_weapon.HideWeapon();  //Hides weapon prefab that is displayed by this item
            base.OnPickedUp();      //Does all "OnPickedUp" opperations associated with the InventoryItem
        }
    }
}
