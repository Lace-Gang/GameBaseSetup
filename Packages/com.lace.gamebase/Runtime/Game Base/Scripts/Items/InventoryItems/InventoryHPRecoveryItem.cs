using UnityEngine;

namespace GameBase
{
    public class InventoryHPRecoveryItem : InventoryItem
    {
        //Exposed Variables
        [Header("HP Recovery Information")]
        [Tooltip("How much will the player be healed by this item")]
        [SerializeField] float m_healAmount;


        ///// <summary>
        ///// Uses this item and then hides it and marks it "Inactive in Scene"
        ///// </summary>
        //public override void OnPickedUp()
        //{
        //    Use();
        //
        //    HideItemInScene();   //Hides item in the scene
        //}

        /// <summary>
        /// Heals player
        /// </summary>
        public override void Use()
        {
            GameInstance.Instance.GetPlayerScript().HealDamage(m_healAmount);   //Heal player
        }
    }
}
