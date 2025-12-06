using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class AmmunitionTracker : MonoBehaviour, IDataPersistence
    {
        #region Variables

        //Hidden Variables
        protected int m_ammunitionAmount;                                           //Current amount of ammunition
        protected List<IAmmunitionUser> m_users = new List<IAmmunitionUser>();      //List of every script that has subscribed to this AmmunitionTracker

        //Exposed Variables
        [Header("Ammunition Tracker Basic Info")]
        [Tooltip ("A reference to the type of ammunition this is tracking")]
        [SerializeField] protected AmmunitionType m_type;
        [Tooltip("The ammount of ammunition in this tracker at start")]
        [SerializeField] protected int m_startingAmount = 0;
        [Tooltip("Does this AmmunitionTracker save its data")]
        [SerializeField] protected bool m_save = false;
        [Tooltip("The individual ID of this tracker. This field is absolutely REQUIRED if 'save' is checked to true, and should be unique between each instance of an object. (Zero if an invalid ID)")]
        [SerializeField] protected int m_ID = 0;

        #endregion Variables


        #region Getters and Setters

        public int GetAmmunitionAmount() { return m_ammunitionAmount; }   //Allows other scripts to get current amount of ammunition
        public AmmunitionType GetAmmunition() { return m_type; }          //Allows other scripts to get the ammunition that this component is tracking

        public void SetAmmunitionAmount(int amount) { m_ammunitionAmount = amount; }    //Allows other scripts to set the ammount of ammunition

        #endregion Getters and Setters


        #region Awake and Start

        /// <summary>
        /// Sets ammunition amount to starting amount
        /// </summary>
        private void Awake()
        {
            m_ammunitionAmount = m_startingAmount;
        }

        /// <summary>
        /// Warns user if a valid ID has not been set
        /// </summary>
        void Start()
        {
            //An individual ID MUST be assigned to each savable item
            Debug.Assert(m_ID != 0, "Ammunition Tracker (for " + m_type.GetName() + ") does not have a valid ID");
        }

        #endregion Awake and Start


        #region Functionality

        /// <summary>
        /// Resets amount of ammunition to starting amount
        /// </summary>
        public void ResetAmmunition()
        {
            m_ammunitionAmount = m_startingAmount;
            NotifyUsers();  //notifies all subscribed scripts of the change
        }

        /// <summary>
        /// Decrements the amount of ammunition by one
        /// </summary>
        public void DecrementAmmunition()
        {
            m_ammunitionAmount--;
            NotifyUsers();  //notifies all subscribed scripts of the change
        }

        /// <summary>
        /// Adds a specified amount of ammunition to the current amount of ammunition
        /// </summary>
        /// <param name="amount">Amount of ammunition to add</param>
        public void AddAmmunition(int amount)
        {
            m_ammunitionAmount += amount;
            NotifyUsers();  //notifies all subscribed scripts of the change
        }

        /// <summary>
        /// Removes a specified amount of ammunition from the current amount of ammunition
        /// </summary>
        /// <param name="amount">Amount of ammunition to remove</param>
        public void RemoveAmmunition(int amount)
        {
            m_ammunitionAmount -= amount;
            NotifyUsers();  //notifies all subscribed scripts of the change
        }

        /// <summary>
        /// Adds a user to the list of subscribed users
        /// </summary>
        /// <param name="user">Reference to the script that is being added</param>
        public void AddUser(IAmmunitionUser user)
        {
            if(!m_users.Contains(user)) m_users.Add(user);  //if list does not already contain the user, adds the user
        }

        /// <summary>
        /// Removes a user from the list of subscribed users
        /// </summary>
        /// <param name="user">Reference to the script that is being removed</param>
        public void RemoveUser(IAmmunitionUser user)
        {
            m_users.Remove(user);   //Removes user from the list of users
        }

        /// <summary>
        /// Notifies all users of the current amount of ammunition
        /// </summary>
        public void NotifyUsers()
        {
            //notifies all users in the list of users
            foreach (IAmmunitionUser user in m_users)
            {
                user.OnAmmunitionChange(m_ammunitionAmount);
            }
        }

        #endregion Functionality


        #region Save and Load

        /// <summary>
        /// Saves data to be added to save file
        /// </summary>
        /// <param name="data">GameData object to save data to</param>
        public void SaveData(ref GameData data)
        {
            if (m_save)     //Only saves data if indicated to do so
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

        /// <summary>
        /// Loads data that was loaded from save file
        /// </summary>
        /// <param name="data">GameData object to load data from</param>
        public void LoadData(GameData data)
        {
            if (m_save) //Only loads data if indicated to do so
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

                NotifyUsers();  //notifies all subscribed scripts of the change
            }
        }

        #endregion Save and Load
    }
}
