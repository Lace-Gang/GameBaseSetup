using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class AmmunitionTracker : MonoBehaviour, IDataPersistence
    {
        //Hidden Variables
        protected int m_ammunitionAmount;
        protected List<IAmmunitionUser> m_users = new List<IAmmunitionUser>();

        //Exposed Variables
        [SerializeField] protected AmmunitionType m_type;
        [SerializeField] protected int m_startingAmount = 0;
        [SerializeField] protected bool m_save = false;
        [Tooltip("This field is absolutely REQUIRED if 'save' is checked to true, and should be unique between each instance of an object. (Zero if an invalid ID)")]
        [SerializeField] protected int m_ID = 0;



        public int GetAmmunitionAmount() { return m_ammunitionAmount; }   //Allows other scripts to get current amount of ammunition
        public AmmunitionType GetAmmunition() { return m_type; }          //Allows other scripts to get the ammunition that this component is tracking


        public void SetAmmunitionAmount(int amount) { m_ammunitionAmount = amount; }




        private void Awake()
        {
            m_ammunitionAmount = m_startingAmount;
        }


        void Start()
        {
            //An individual ID MUST be assigned to each savable item
            Debug.Assert(m_ID != 0, "Ammunition Tracker (for " + m_type.GetName() + ") does not have a valid ID");
        }


        #region Functionality

        public void ResetAmmunition()
        {
            m_ammunitionAmount = m_startingAmount;
            NotifyUsers();
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

        #endregion Functionality


        #region Save and Load
        public void SaveData(ref GameData data)
        {
            if (m_save)
            {
                //Save amount of ammunition
                if (data.intData.ContainsKey("AmmunitionTracker." + m_ID + ".AmmunitionAmount"))
                {
                    data.intData["AmmunitionTracker." + m_ID + ".AmmunitionAmount"] = m_ammunitionAmount;
                }
                else
                {
                    data.intData.Add("AmmunitionTracker." + m_ID + ".AmmunitionAmount", m_ammunitionAmount);
                }
            }
        }


        public void LoadData(GameData data)
        {
            if (m_save)
            {
                //Load amount of ammunition
                if (data.intData.ContainsKey("AmmunitionTracker." + m_ID + ".AmmunitionAmount"))
                {
                    m_ammunitionAmount = data.intData["AmmunitionTracker." + m_ID + ".AmmunitionAmount"];
                }
                else
                {
                    m_ammunitionAmount = m_startingAmount;
                }

                NotifyUsers();
            }
        }


        #endregion Save and Load
    }
}
