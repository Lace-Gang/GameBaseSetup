using UnityEngine;

namespace GameBase
{
    public class WeaponItem : InventoryItem
    {
        //Hidden Variables
        protected string m_weaponName = string.Empty;
        protected int m_Amo = -1;
        protected GameObject m_weaponMesh;


        //Exposed Variables
        [Header("Weapon Pikcup Item Details")]
        [Tooltip("The weapon that this item represents")]
        [SerializeField] WeaponBase m_weaponScript;
        //[SerializeField] string weaponName;
        //[SerializeField] int weaponAmo;
        [SerializeField] WeaponBase m_weapon;


        private void Start()
        {
            m_weaponName = m_weaponScript.GetWeaponName();
        }

        public string GetWeaponName() { return m_weaponName; }
        public int GetWeaponAmo() {  return m_Amo; }






        public void EquipWeapon()
        {
            //Debug.Log("Weapon Equipped");
            GameInstance.Instance.GetPlayerScript().EquipWeapon(m_weapon);
        }




        public override void Use()
        {
            Debug.Log("Weapon Used");


            //throw new System.NotImplementedException();
        }


        public override void OnPickedUp()
        {
            //m_weapon.SetActive(false);
            m_weapon.HideWeapon();
            base.OnPickedUp();
        }
    }
}
