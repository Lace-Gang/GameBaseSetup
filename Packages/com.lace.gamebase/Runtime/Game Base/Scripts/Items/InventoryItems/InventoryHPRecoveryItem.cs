using UnityEngine;

namespace GameBase
{
    public class InventoryHPRecoveryItem : InventoryItem
    {
        //Exposed Variables
        [Header("HP Recovery Information")]
        [Tooltip("How much will the player be healed by this item")]
        [SerializeField] protected float m_healAmount;


        /// <summary>
        /// Heals player
        /// </summary>
        public override void Use()
        {
            GameInstance.Instance.GetPlayerScript().HealDamage(m_healAmount);   //Heal player
        }
    }
}
