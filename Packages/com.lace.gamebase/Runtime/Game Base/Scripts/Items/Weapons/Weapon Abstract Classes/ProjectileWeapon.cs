using UnityEngine;

namespace GameBase
{
    public abstract class ProjectileWeapon : WeaponBase
    {
        //Hidden Variables
        //protected AmmunitionTracker m_ammunitionTracker;

        //Exposed Variables
        [Header("Ranged Weapon Details")]
        [Tooltip("What part of the weapon shoould the projectile be fired from")]
        [SerializeField] Transform m_firePoint;
        [Tooltip("What projectile ammunition is used by this weapon. MUST have a ProjectileBase component.")]
        [SerializeField] protected GameObject m_ammunition;
        [SerializeField] protected AmmunitionType m_ammunitionType;
        [Tooltip("Does the amount of ammo this weapon has go down after firing a projectile")]
        [SerializeField] protected bool m_consumesAmo = true;
        [Tooltip("Does this weapon fire regardless of ammunition amount")]
        [SerializeField] protected bool m_ignoreAmmoAmount = false;
        [Tooltip("How much ammunition does this ranged weapon start with")]
        [SerializeField] protected int m_startingAmo;



        public override int GetAmmoAmount() { return m_ammunitionTracker.GetAmmunitionAmount(); }




        /// <summary>
        /// Validates important weapon info
        /// </summary>
        private void Awake()
        {
            Debug.Assert(m_ammunition != null, "Ranged Weapon requires a ");
            Debug.Assert(m_ammunition.GetComponent<DamagingProjectile>() != null, "Ammunition MUST have a ProjectileBase interface!");

            //m_ammoAmount = (m_startingAmo >= 0) ? m_startingAmo : 0; //sets amount of amo at start

            m_ammunitionTracker = GameInstance.Instance.FindAmmunitionTracker(m_ammunitionType.GetName());
        }



        /// <summary>
        /// Spawns and fires projectile
        /// </summary>
        public override void Attack()
        {
            //if(!m_ignoreAmmoAmount && m_ammoAmount <= 0) return;   //validates that weapon has enough ammunition to attack, returns if it does not
            if(!m_ignoreAmmoAmount && m_ammunitionTracker.GetAmmunitionAmount() <= 0) return;   //validates that weapon has enough ammunition to attack, returns if it does not

            //Spawns projectile in the world
            GameObject projectileObject = GameInstance.Instance.SpawnObjectInWorld(m_ammunition);

            //Moves projectile to the correct location and rotation
            projectileObject.transform.position = m_firePoint.position;
            projectileObject.transform.rotation = m_firePoint.rotation;
            
            
            //properly configures projectile through script
            DamagingProjectile projectileScript = projectileObject.GetComponent<DamagingProjectile>();
            projectileScript.SetDamageOwner(m_weaponOwner);
            //projectileScript.SetTrajectory(m_weaponOwner.transform.forward, 100f);
            projectileScript.SetTrajectory(-m_firePoint.transform.forward, 100f);
            projectileScript.StartLifetimeTimer();  //tells projectile to destroy itself after lifespan lapses

            //decrements amount of amo if applicable
            if(m_consumesAmo)
            {
                //m_ammoAmount--;
                m_ammunitionTracker.DecrementAmmunition();
                UserInterface.Instance.UpdateWeaponBoxAmo();
            }
        }

        /// <summary>
        /// Makes this weapon visible
        /// </summary>
        public override void ShowWeapon()
        {
            GetComponentInChildren<MeshRenderer>().enabled = true;
        }

        /// <summary>
        /// Makes this weapon invisible
        /// </summary>
        public override void HideWeapon()
        {
            GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }
}
