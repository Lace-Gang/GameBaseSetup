using UnityEngine;

namespace GameBase
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(DamageSource))]
    [RequireComponent(typeof(Rigidbody))]
    public class DamagingProjectile : Projectile
    {
        //Hidden Variables
        Rigidbody m_rb;


        /// <summary>
        /// Gets rigidbody component
        /// </summary>
        private void Awake()
        {
            m_rb = GetComponent<Rigidbody>();
        }


        /// <summary>
        /// Sets the owner of this projectile's damage
        /// </summary>
        /// <param name="owner">The owner of the damage this projectile is doing</param>
        public void SetDamageOwner(GameObject owner)
        {
            GetComponent<DamageSource>().SetDamageOwner(owner);
        }


        /// <summary>
        /// Sets the trajectory of this projectile through it's rigidbody
        /// </summary>
        /// <param name="direction">Direction projectile should move</param>
        /// <param name="velocity">Desired Velocity of the projectile</param>
        public override void SetTrajectory(Vector3 direction, float velocity)
        {
            m_rb.AddForce(direction * velocity, ForceMode.Impulse);
        }
    }
}
