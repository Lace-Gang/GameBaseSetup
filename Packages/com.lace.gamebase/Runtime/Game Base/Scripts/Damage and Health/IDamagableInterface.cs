using UnityEngine;

namespace GameBase
{
    //This is the interface for damagable objects
    //It is required to add this interface to at least one script for any object you want to take damage from the damage system in this Package.
    public interface IDamagableInterface
    {
        /// <summary>
        /// Takes Damage from a damage source
        /// </summary>
        /// <param name="damage">Amount of damage to take</param>
        /// <param name="owner">Owning object of the damage being taken</param>
        public void TakeDamage(float damage, GameObject owner);

        /// <summary>
        /// Heal damage from a healing source
        /// </summary>
        /// <param name="amount">Amount of damage being healed</param>
        public void HealDamage(float amount);

        /// <summary>
        /// What the object should do when it runs out of health
        /// </summary>
        void OnDeath();
    }
}
