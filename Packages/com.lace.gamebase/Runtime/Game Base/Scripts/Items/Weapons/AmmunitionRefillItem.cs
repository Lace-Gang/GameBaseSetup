using UnityEngine;

namespace GameBase
{
    public class AmmunitionRefillItem : InventoryItem
    {
        //Hidden Variables


        //Exposed Variables
        [SerializeField] AmmunitionType m_ammunition;
        [SerializeField] int m_ammunitionAmount = 0;



        public AmmunitionType GetAmmunitionType() { return m_ammunition; }

        public int GetAmmunitionAmount() { return m_ammunitionAmount; }

        public override void Use()
        {
            //
        }
    }
}
