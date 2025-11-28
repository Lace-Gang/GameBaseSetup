using UnityEngine;

namespace GameBase
{
    

    public abstract class WeaponBase : MonoBehaviour
    {
        //Hidden Variables
        protected AmmunitionTracker m_ammunitionTracker = null; //Tracker for this weapon's ammunition (if applicable)
        protected GameObject m_weaponOwner = null;              //Who or what is using this projectile weapon
        protected int m_ammoAmount = -1;                        //Amount of ammunition this weapon currently has (defaults to negative one which indicates that the weapon does not use ammo)




        //Exposed Variables
        [Header("Weapon Components")]
        //[SerializeField] protected Renderer m_renderer;
        [SerializeField] protected MeshRenderer m_renderer;

        [Header("Base Weapon Information")]
        [Tooltip("The name of this weapon")]
        [SerializeField] protected string m_weaponName;
        [Tooltip("Name of the weapon socket on which this weapon is intended to be equipped")]
        [SerializeField] protected string m_socketName = "PlayerRightHandWeaponSocket";
        [Tooltip("Does a sound play when an attack using this weapon is triggered")]
        [SerializeField] protected bool m_playAttackSound = false;
        [Tooltip("Sound that plays when attacking with this weapon")]
        [SerializeField] protected AudioSource m_attackAudio;
        [Tooltip("How long does an attack with this weapon last")]
        [SerializeField] protected float m_attackDuration = 1f;


        public AmmunitionTracker GetAmmunitionTracker() { return m_ammunitionTracker; }
        public float GetAttackDuration() { return m_attackDuration; }   //Allows other scripts to see this weapon's attack duration

        //public GameObject GetMesh() { return m_mesh; }
        //public MeshFilter GetMeshFilter() { return m_meshFilter; }
        public string GetWeaponName() { return m_weaponName; }      //Allows other scripts to get the name of this weapon
        public string GetSocketName() { return m_socketName; }      //Allows other scripts to get the name of the script this weapon is supposed to be equipped to
        public virtual int GetAmmoAmount() { return m_ammoAmount; }


        public virtual void SetWeaponOwner(GameObject owner) { m_weaponOwner = owner; }     //Allows other scripts to set the owner of this weapon


       //public void ShowWeapon()
       //{
       //    m_renderer.enabled = false;
       //}
       //
       //
       //public void HideWeapon()
       //{
       //    m_renderer.enabled = true;
       //}

        /// <summary>
        /// Does anything required by this weapon in order to attack with this weapon
        /// </summary>
        public abstract void Attack();

        /// <summary>
        /// Makes this weapon visible
        /// </summary>
        public abstract void ShowWeapon();

        /// <summary>
        /// Makes this weapon invisible
        /// </summary>
        public abstract void HideWeapon();
    }
}

