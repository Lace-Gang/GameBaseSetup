using UnityEngine;

namespace GameBase
{
    public class AmmunitionRefillItem : InventoryItem
    {
        //Exposed Variables
        [SerializeField] AmmunitionType m_ammunition;
        [SerializeField] int m_ammunitionAmount = 0;



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
