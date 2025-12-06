using UnityEngine;

namespace GameBase
{
    public class AmmunitionRefillItem : InventoryItem
    {
        //Exposed Variables
        [Header("Ammunition Refill Basic Info")]
        [Tooltip("Reference to the AmmunitionType that this item is refilling")]
        [SerializeField] protected AmmunitionType m_ammunition;
        [Tooltip("Amount of ammunition that this item refills")]
        [SerializeField] protected int m_ammunitionAmount = 0;


        public AmmunitionType GetAmmunitionType() { return m_ammunition; }  //Allows other scripts to get the ammunition

        public int GetAmmunitionAmount() { return m_ammunitionAmount; }     //Allows other scripts to get this refill amount

        /// <summary>
        /// Currently does nothing
        /// </summary>
        public override void Use()
        {
            //Doesn't need to do anything
        }
    }
}
