using UnityEngine;

namespace GameBase
{
    public class Health : MonoBehaviour
    {
        //Hidden Variables
        protected float m_health; //current health of this health's owning object/script

        //Exposed Variables
        [Header("Health Basic Information")]
        [Tooltip("The max health of the owner")]
        [SerializeField] protected float m_maxHealth;
        [Tooltip("Health the owner starts with")]
        [SerializeField] protected float m_startingHealth;


        #region Getters and Setters

        /// <summary>
        /// Allows other classes to view current health
        /// </summary>
        /// <returns>Current health</returns>
        public float GetHealth() {  return m_health; }

        /// <summary>
        /// Directly set player health
        /// </summary>
        /// <param name="health">Desired player health</param>
        public void SetHealth(float health)
        {
            m_health = health;
        }

        /// <summary>
        /// Allows other classes to view max health
        /// </summary>
        /// <returns>Max health</returns>
        public float GetMaxHealth() { return m_maxHealth; }


        /// <summary>
        /// Sets a new Max Health, and ensures that current health does not exceed that max
        /// </summary>
        /// <param name="newMaxHealth">Desired max health</param>
        public void SetMaxHealth(float newMaxHealth) 
        {
            //sets new max health
            m_maxHealth = newMaxHealth;

            //clamps current health to stay within max health
            m_health = (m_health <= m_maxHealth) ? m_health : m_maxHealth;
        }

        #endregion Getters and Setters


        /// <summary>
        /// Sets current health equal to starting health.
        /// </summary>
        void Awake()
        {
            //Set current health to starting health
            m_health = m_startingHealth;
        }

        /// <summary>
        /// Changes current health by adding an amount of health.
        /// </summary>
        /// <param name="changeInHealth">Amount to change health by. A negative float will result in a decrease in health.</param>
        /// <returns>True if health has dropped to zero, otherwise returns False</returns>
        public bool AddToHealth(float changeInHealth)
        {
            //Adds the change in health, without allowing the health to exceed max
            if (m_health + changeInHealth < m_maxHealth) m_health += changeInHealth;
            else m_health = m_maxHealth;

            //Checks for health equal to or less than zero
            if(m_health <= 0)
            {
                m_health = 0;   //clamps health to a minimum of zero
                return true;    //notifies the calling script that health has hit zero
            }

            return false;   //notifies the calling script that health is above zero
        }
    }
}
