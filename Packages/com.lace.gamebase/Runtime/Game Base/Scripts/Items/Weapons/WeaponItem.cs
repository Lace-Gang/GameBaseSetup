using UnityEngine;

namespace GameBase
{
    public class WeaponItem : InventoryItem
    {
        //Exposed Variables
        [Header("Weapon Pikcup Item Details")]
        [Tooltip("The weapon that this item represents")]
        [SerializeField] WeaponBase weapon;
        [SerializeField] string weaponName;
        [SerializeField] int weaponAmo;



        public string GetWeaponName() { return weaponName; }
        public int GetWeaponAmo() {  return weaponAmo; } 


        public void EquipWeapon()
        {
            Debug.Log("Weapon Equipped");
        }




        public override void Use()
        {
            Debug.Log("Weapon Used");


            //throw new System.NotImplementedException();
        }
    }
}
