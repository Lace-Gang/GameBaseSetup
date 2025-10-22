using UnityEngine;

namespace GameBase
{
    public interface IDamagableInterface
    {
        public void TakeDamage(float damage, GameObject owner);

        public void HealDamage(float amount);

        void OnDeath();
    }
}
