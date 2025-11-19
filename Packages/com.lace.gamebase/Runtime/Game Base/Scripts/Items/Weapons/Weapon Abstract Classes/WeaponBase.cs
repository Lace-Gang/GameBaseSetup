using UnityEngine;

namespace GameBase
{
    

    public abstract class WeaponBase : MonoBehaviour
    {
        //Hidden Variables



        //Exposed Variables
        [SerializeField] string m_weaponName;
        [SerializeField] string m_socketName = "PlayerRightHandWeaponSocket";
        //[SerializeField] MeshFilter m_meshFilter;
        //[SerializeField] GameObject m_esh;



        //public GameObject GetMesh() { return m_mesh; }
        //public MeshFilter GetMeshFilter() { return m_meshFilter; }
        public string GetWeaponName() { return m_weaponName; }
        public string GetSocketName() { return m_socketName; }


        public abstract void Attack();

        public abstract void ShowWeapon();
        public abstract void HideWeapon();
    }
}



//What does a weapon need?

//1.) Be able to attack
//2.) Damage
    //2a.) Get Damage owner
//3.) Weapon Type
    //3a.) One handed melee
    //3b.) Two handed melee
    //3c.) One handed gun/projectile
        //3ca.) Ammunition
    //3d.) Two handed gun/projectile
            //3da.) Ammunition
//4.) In inventory
    //4a.) Weapon name
    //4b.) NOT equippable
    //4c.) Tracks amount of amo, not number of weapon
    //4d.) Sprite
//5.) Saves
//6.) Use just means attack probably
//7.) Notify Animator somehow
//8.) UI Displays equipped Weapon
//9.) Probably want weapons to be usable by both player and others.
