using UnityEngine;

namespace GameBase
{
    public class AmmunitionTracker : MonoBehaviour
    {
        //Hidden Variables
        protected int m_ammunitionAmount;

        //Exposed Variables
        [SerializeField] protected AmmunitionType m_type;
        [SerializeField] protected int m_startingAmount = 0;



        public int GetAmmunitionAmount() { return m_ammunitionAmount; }   //Allows other scripts to get current amount of ammunition
        public AmmunitionType GetAmmunition() { return m_type; }          //Allows other scripts to get the ammunition that this component is tracking


        public void SetAmmunitionAmount(int amount) { m_ammunitionAmount = amount; }




        private void Awake()
        {
            m_ammunitionAmount = m_startingAmount;
        }

        public void DecrementAmmunition()
        {
            m_ammunitionAmount--;
        }


        public void AddAmmunition(int  amount)
        {
            m_ammunitionAmount += amount;
        }

        public void RemoveAmmunition(int amount)
        {
            m_ammunitionAmount -= amount;
        }
    }
}
