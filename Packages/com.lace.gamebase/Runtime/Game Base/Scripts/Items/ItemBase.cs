using System.Collections;
using UnityEngine;

namespace GameBase
{
    public abstract class ItemBase : MonoBehaviour
    {
        //Hidden Variables
        protected bool m_activeInScene = true;  //Is the object currently present in the world


        //Exposed Variables
        [Header("Basic Item Components")]
        [SerializeField] protected MeshRenderer m_renderer;
        [SerializeField] protected Collider m_collider;

        [Header("Basic Item Information")]
        //Add a tooltip here later!!!!!
        [SerializeField] protected string m_name;
        [Tooltip("Is item picked up automatically when player enters trigger")]
        [SerializeField] protected bool m_autoPickup = true;
        

        



        /// <summary>
        /// If the object entering this item's trigger is the player, executes the "ItemBaseTriggerEnter" function
        /// </summary>
        /// <param name="other">Collider that entered the trigger (handled by game engine)</param>
        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<PlayerCharacter>() != null)
            { 
                ItemBaseTriggerEnter();
            }
        }


        /// <summary>
        /// Evaluates if the object triggering is the player. If so, and if the item is to be automatically picked up, executes all necessary code for 
        /// when the item is "picked up"
        /// </summary>
        /// <returns></returns>
        protected void ItemBaseTriggerEnter()
        {
            if(m_autoPickup)
            {
                OnPickedUp();           //Child class "OnPickedUp"
                //ItemBaseOnPickedUp();   //Parent class "OnPickedUp"
            }
        }

        /// <summary>
        /// Hides item and marks item as "Inactive in Scene"
        /// </summary>
        protected void ItemBaseOnPickedUp()
        {
            m_activeInScene = false;            //item no longer present in the world
            m_renderer.enabled = false;     //item no longer visible
            m_collider.enabled = false;     //item no longer collidable
        }


        /// <summary>
        /// Individual code for each item when picked up
        /// </summary>
        public abstract void OnPickedUp();


        public abstract void Use();
    }
}

