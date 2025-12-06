using UnityEngine;

namespace GameBase
{
    public abstract class ProjectileWeapon : WeaponBase, IAmmunitionUser
    {
        #region Variables

        //Exposed Variables
        [Header("Ranged Weapon Details")]
        [Tooltip("What part of the weapon shoould the projectile be fired from")]
        [SerializeField] protected Transform m_firePoint;
        [Tooltip("What projectile ammunition type is used by this weapon. (included ammunition MUST have a ProjectileBase component.)")]
        [SerializeField] protected AmmunitionType m_ammunitionType;
        [Tooltip("Does the amount of ammo this weapon has go down after firing a projectile")]
        [SerializeField] protected bool m_consumesAmo = true;
        [Tooltip("Does this weapon fire regardless of ammunition amount")]
        [SerializeField] protected bool m_ignoreAmmoAmount = false;
        [Tooltip("How much ammunition does this ranged weapon start with")]
        [SerializeField] protected int m_startingAmo;

        #endregion Variables


        public override int GetAmmoAmount() { return m_ammunitionTracker.GetAmmunitionAmount(); }   //Allows other scripts to see how much ammo this weapon currently has access to


        #region Awake and OnDestroy

        /// <summary>
        /// Validates important weapon info
        /// </summary>
        private void Awake()
        {
            //Warns user if AmmunitionType is null or invalid
            Debug.Assert(m_ammunitionType != null, "Ranged Weapon requires an AmmunitionType!");
            Debug.Assert(m_ammunitionType.GetAmmunition().GetComponent<DamagingProjectile>() != null, "Ammunition MUST have a ProjectileBase interface!");

            SubscribeToTracker();   //subscribe to ammunition tracker
        }

        /// <summary>
        /// Unsubscribes from AmmunitionTracker to prevent AmmunitionTracker from calling a destroyed script
        /// </summary>
        private void OnDestroy()
        {
            UnsubscribeFromTracker();
        }

        #endregion Awake and OnDestroy


        #region ProjectileWeapon Basic Functionality

        /// <summary>
        /// Spawns and fires projectile
        /// </summary>
        public override void Attack()
        {
            //if(!m_ignoreAmmoAmount && m_ammoAmount <= 0) return;   //validates that weapon has enough ammunition to attack, returns if it does not
            if(!m_ignoreAmmoAmount && m_ammunitionTracker.GetAmmunitionAmount() <= 0) return;   //validates that weapon has enough ammunition to attack, returns if it does not

            //Play attack audio
            if(m_playAttackSound && m_attackAudio != null)
            {
                m_attackAudio.Play();
            }

            //Spawns projectile in the world
            GameObject projectileObject = GameInstance.Instance.SpawnObjectInWorld(m_ammunitionType.GetAmmunition());

            //Moves projectile to the correct location and rotation
            projectileObject.transform.position = m_firePoint.position;
            projectileObject.transform.rotation = m_firePoint.rotation;
            
            
            //properly configures projectile through script
            DamagingProjectile projectileScript = projectileObject.GetComponent<DamagingProjectile>();
            projectileScript.SetDamageOwner(m_weaponOwner);
            projectileScript.SetTrajectory(-m_firePoint.transform.forward, 100f);
            projectileScript.StartLifetimeTimer();  //tells projectile to destroy itself after lifespan lapses

            //decrements amount of amo if applicable
            if(m_consumesAmo)
            {
                m_ammunitionTracker.DecrementAmmunition();
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

        #endregion ProjectileWeapon Basic Functionality


        #region Ammunition User

        /// <summary>
        /// Finds the AmmunitionTracker that matches this weapon's ammunition type, and subscribes to it
        /// </summary>
        public void SubscribeToTracker()
        {
            m_ammunitionTracker = GameInstance.Instance.FindAmmunitionTracker(m_ammunitionType.GetName());  //Find correct AmmunitionTracker
            m_ammunitionTracker.AddUser(this);  //Subscribe to AmmunitionTracker
            m_ammoAmount = m_ammunitionTracker.GetAmmunitionAmount();   //Track currect amount of ammunition
        }

        /// <summary>
        /// Unsubscribes from AmmunitionTracker
        /// </summary>
        public void UnsubscribeFromTracker()
        {
            if(m_ammunitionTracker != null) m_ammunitionTracker.RemoveUser(this);   //Unsubscribes from Ammunition Tracker (if this weapon was subscribed to a tracker)
            m_ammoAmount = -1;  //Sets ammunition amount to -1 to indicate that ammunition is no longer being used
        }

        /// <summary>
        /// Tracks new amount of ammunition
        /// </summary>
        /// <param name="ammount">Amount of ammunition after the change</param>
        public void OnAmmunitionChange(int ammount)
        {
            m_ammoAmount = ammount; //Updates ammunition amount
        }

        #endregion Ammunition User
    }
}
