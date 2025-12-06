using UnityEngine;

namespace GameBase
{
    //This is the interface for damagable objects
    //It is required to add this interface to at least one script for any object you want to take damage from the damage system in this Package.
    public interface IDamagableInterface
    {
        /// <summary>
        /// Intended to receive damage from a damage source
        /// </summary>
        /// <param name="damage">Amount of damage to take</param>
        /// <param name="owner">Owning object of the damage being taken</param>
        public void TakeDamage(float damage, GameObject owner);

        /// <summary>
        /// Intended to heal damage from a healing source
        /// </summary>
        /// <param name="amount">Amount of damage being healed</param>
        public void HealDamage(float amount);

        /// <summary>
        /// Intended to execute necessary processes when this damagable object runs out of health
        /// </summary>
        void OnDeath();
    }
}
