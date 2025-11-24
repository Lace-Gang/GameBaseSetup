using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class AmmunitionTracker : MonoBehaviour
    {
        //Hidden Variables
        protected int m_ammunitionAmount;
        protected List<IAmmunitionUser> m_users = new List<IAmmunitionUser>();

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
            NotifyUsers();
        }


        public void AddAmmunition(int  amount)
        {
            m_ammunitionAmount += amount;
            NotifyUsers();
        }

        public void RemoveAmmunition(int amount)
        {
            m_ammunitionAmount -= amount;
            NotifyUsers();
        }


        public void AddUser(IAmmunitionUser user)
        {
            if(!m_users.Contains(user)) m_users.Add(user);
        }

        public void RemoveUser(IAmmunitionUser user)
        {
            m_users.Remove(user);
        }

        public void NotifyUsers()
        {
            foreach (IAmmunitionUser user in m_users)
            {
                user.OnAmmunitionChange(m_ammunitionAmount);
            }
        }
    }
}
