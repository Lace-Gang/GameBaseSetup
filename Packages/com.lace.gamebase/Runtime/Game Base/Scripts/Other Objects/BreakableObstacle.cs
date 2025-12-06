using NUnit.Framework;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GameBase
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Rigidbody))]
    public class BreakableObstacle : MonoBehaviour, IDamagableInterface, IDataPersistence
    {
        #region Variables

        //Hidden Variables
        protected bool m_isDestroyed = false;   //Whether this Obsticle has been destroyed yet or not

        //Exposed Variables
        [Header("Breakable Obstacle Basic Info")]
        [Tooltip ("This Obstacle's mesh renderer")]
        [SerializeField] protected Renderer m_renderer;
        [Tooltip ("This Obstacle's collider")]
        [SerializeField] protected Collider m_collider;
        [Tooltip ("This Obstacle's 'Health' component")]
        [SerializeField] protected Health m_health;

        [Header("Breakable Obstacle Save Info")]
        [Tooltip("This BreakableObstacle instance's unique ID")]
        [SerializeField] protected int m_ID = 0;
        [Tooltip("Should this Obsticle save whether it has been destroyed or not")]
        [SerializeField] protected bool m_saveDestroyed = true;
        [Tooltip("Should this Obsticle save its health")]
        [SerializeField] protected bool m_saveHealth = true;
        [Tooltip("Should this Obsticle save its location/position")]
        [SerializeField] protected bool m_saveLocation = false;
        [Tooltip("Should this Obsticle save its rotation")]
        [SerializeField] protected bool m_saveRotation = false;

        #endregion Variables


        /// <summary>
        /// Verifies important object information
        /// </summary>
        private void Awake()
        {
            //Finds health component if it has not been added through the editor(will not find component if component is on a child of the object)
            if (m_health == null) m_health = GetComponent<Health>();

            //Verifies that an ID has been assigned, and notifies user if this is false
            Debug.Assert(m_ID != 0, "BreakableObstacle has not had a Unique ID assigned!");
        }


        #region Health and Damage

        /// <summary>
        /// Heals damage from this obstacle
        /// </summary>
        /// <param name="amount">Amount of damage being healed</param>
        public void HealDamage(float amount)
        {
            if(m_isDestroyed) return;   //does not heal if this obstacle is already destroyed

            m_health.AddToHealth(amount);
        }

        /// <summary>
        /// Deals damage to this obstacle
        /// </summary>
        /// <param name="damage">Amount of damage being dealt</param>
        /// <param name="owner">What caused the damage</param>
        public void TakeDamage(float damage, GameObject owner)
        {
            if (m_isDestroyed) return;  //Does not take damage if this obstacle is already destroyed

            //Deals damage and if that damage reduces this obstacle to zero health or less, the OnDeath function is called
            if (m_health.AddToHealth(-damage))
            {
                OnDeath();
            }
        }

        /// <summary>
        /// Makes object disapear
        /// </summary>
        public void OnDeath()
        {
            if (m_isDestroyed) return;

            m_isDestroyed = true;   //track that item has been destroyed

            //completely hide item in scene
            m_renderer.gameObject.SetActive(false);
            m_collider.gameObject.SetActive(false);
        }

        #endregion Health and Damage


        #region Save and Load

        /// <summary>
        /// Saves the relevant data of this Obstacle
        /// </summary>
        /// <param name="data">Game Data object being saved to</param>
        public void SaveData(ref GameData data)
        {
            //Save isDestroyed if indicated to do so
            if (m_saveDestroyed)
            {
                //Check boolData for key. If key exists, change value to current value, else add key with current value
                if (data.boolData.ContainsKey("BreakableObstacle." + m_ID + ".IsDestroyed"))
                {
                    data.boolData["BreakableObstacle." + m_ID + ".IsDestroyed"] = m_isDestroyed;
                }
                else
                {
                    data.boolData.Add("BreakableObstacle." + m_ID + ".IsDestroyed", m_isDestroyed);
                }
            }

            //Save health if indicated to do so
            if (m_saveHealth)
            {
                //Check floatData for key. If key exists, change value to current value, else add key with current value
                if (data.floatData.ContainsKey("BreakableObstacle." + m_ID + ".CurrentHealth"))
                {
                    data.floatData["BreakableObstacle." + m_ID + ".CurrentHealth"] = m_health.GetHealth();
                }
                else
                {
                    data.floatData.Add("BreakableObstacle." + m_ID + ".CurrentHealth", m_health.GetHealth());
                }
            }

            //Save location/position if indicated to to so
            if (m_saveLocation)
            {
                //Check vector3Data for key. If key exists, change value to current value, else add key with current value
                if (data.vector3Data.ContainsKey("BreakableObstacle." + m_ID + ".Location"))
                {
                    data.vector3Data["BreakableObstacle." + m_ID + ".Location"] = transform.position;
                }
                else
                {
                    data.vector3Data.Add("BreakableObstacle." + m_ID + ".Location", transform.position);
                }
            }

            //Save rotation if indicated to do so
            if (m_saveRotation)
            {
                //Check quaternionData for key. If key exists, change value to current value, else add key with current value
                if (data.quaternionData.ContainsKey("BreakableObstacle." + m_ID + ".Rotation"))
                {
                    data.quaternionData["BreakableObstacle." + m_ID + ".Rotation"] = transform.rotation;
                }
                else
                {
                    data.quaternionData.Add("BreakableObstacle." + m_ID + ".Rotation", transform.rotation);
                }
            }
        }

        /// <summary>
        /// Loads saved data to this obstacle
        /// </summary>
        /// <param name="data">GameData object containing the data being loaded</param>
        public void LoadData(GameData data)
        {
            //Loads whether item has been destroyed if indicated to do so and data is contained within the save file
            if (m_saveDestroyed && data.boolData.ContainsKey("BreakableObstacle." + m_ID + ".IsDestroyed"))
            {
                m_isDestroyed = data.boolData["BreakableObstacle." + m_ID + ".IsDestroyed"];

                //Hides item if item has already been destroyed
                if (m_isDestroyed)
                {
                    m_renderer.gameObject.SetActive(false);
                    m_collider.gameObject.SetActive(false);
                }
            }

            //Loads current health if indicated to do so and data is contained within the save file
            if (m_saveHealth && data.floatData.ContainsKey("BreakableObstacle." + m_ID + ".CurrentHealth"))
            {
                m_health.SetHealth(data.floatData["BreakableObstacle." + m_ID + ".CurrentHealth"]); //updates current health
            }

            //Loads location/position if indicated to do so and data is contained within the save file
            if(m_saveLocation && data.vector3Data.ContainsKey("BreakableObstacle." + m_ID + ".Location"))
            {
                transform.position = data.vector3Data["BreakableObstacle." + m_ID + ".Location"];
            }

            //Loads rotation if indicated to do so and data is contained within the save file
            if(m_saveRotation && data.quaternionData.ContainsKey("BreakableObstacle." + m_ID + ".Rotation"))
            {
                transform.rotation = data.quaternionData["BreakableObstacle." + m_ID + ".Rotation"];
            }
        }

        #endregion Save and Load
    }
}
