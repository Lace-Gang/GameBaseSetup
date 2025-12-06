using UnityEngine;

namespace GameBase
{
    public class SavableHealthUpgradeItem : SavableItem
    {
        //Exposed Variables
        [Header("Health Upgrade Info")]
        [Tooltip("How much to add to the players Max Health")]
        [SerializeField] protected float m_upgradeAmount = 10f;
        [Tooltip("True if player should be healed to new max health after obtaining this upgrade")]
        [SerializeField] protected bool m_healToFull = false;
        [Tooltip("If not healing to full, the amount that the player should be healed after obtaining this upgrade")]
        [SerializeField] protected float m_healingAmount = 0f;


        /// <summary>
        /// Uses this item and then hides it and marks it "Inactive in Scene"
        /// </summary>
        public override void OnPickedUp()
        {
            Use();  //Uses item

            HideItemInScene();   //Hides item in the scene
        }

        /// <summary>
        /// Upgrades the player's Max Health and optionally heals the player
        /// </summary>
        public override void Use()
        {
            //Get reference to the player
            PlayerCharacter player = GameInstance.Instance.GetPlayerScript();

            player.UpgradeHealth(m_upgradeAmount, m_healToFull);    //upgrades player health, and fully heals player if indicated to do so
            player.HealDamage(m_healingAmount);                     //heals player by specified amount
        }
    }
}
