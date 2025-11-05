using System.Collections;
using UnityEngine;

namespace GameBase
{
    public abstract class ItemBase : MonoBehaviour
    {
        //Hidden Variables
        private bool m_presentInScene = true;  //Is the object currently present in the world


        //Exposed Variables
        [Header("Basic Item Components")]
        [SerializeField] MeshRenderer m_renderer;
        [SerializeField] Collider m_collider;

        [Header("Basic Item Information")]
        [SerializeField] string m_name;
        [Tooltip("Is item picked up automatically when player enters trigger")]
        [SerializeField] protected bool m_autoPickup = true;

        //[Header("Basic Item Behavior")]
        //[SerializeField] bool m_rotates = false;
        //[Tooltip("Rotation speed in degrees per second")]
        //[SerializeField] float m_rotationSpeed;


       //private void Start()
       //{
       //   //if(m_rotates)
       //   //{
       //   //    StartCoroutine(Rotate());
       //   //}
       //}


        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<PlayerCharacter>() != null)
            { 
                ParentTriggerEnter();
            }
        }


        /// <summary>
        /// Evaluates if the object triggering is the player. If so, and if the item is to be automatically picked up, executes all necessary code for 
        /// when the item is "picked up"
        /// </summary>
        /// <returns></returns>
        protected void ParentTriggerEnter()
        {
            if(m_autoPickup)
            {
                ItemBaseOnPickedUp();   //Parent class "OnPickedUp"
                OnPickedUp();           //Child class "OnPickedUp"
            }
        }

        /// <summary>
        /// Executes all necessary code for all items for when a player exits the trigger of an item
        /// </summary>
        protected void ParentTriggerExit()
        {
           //if(!m_autoPickup)
           //{
           //    UserInterface.Instance.HidePromptBox();
           //}
        }


        /// <summary>
        /// Executes all necessary code for all items for when item is picked up
        /// </summary>
        protected void ItemBaseOnPickedUp()
        {
            m_presentInScene = false;            //item no longer present in the world
            m_renderer.enabled = false;     //item no longer visible
            m_collider.enabled = false;     //item no longer collidable
        }


        /// <summary>
        /// Individual code for each item when picked up
        /// </summary>
        public abstract void OnPickedUp();



        //public IEnumerator Rotate()
        //{
        //    while(true)
        //    {
        //        Quaternion q = new Quaternion(transform.rotation.x, transform.rotation.y + (m_rotationSpeed * Time.deltaTime), transform.rotation.z, transform.rotation.w);
        //        transform.rotation = q;
        //        yield return null;
        //    }
        //}
    }
}



//Which items do we want?

//Inventory & non-inventory

//Non-inventory
//Health pick up
//Amo pickup
//Health Upgrade
//Score Increase


//inventory
//weapons
//Tools
//Inventory-health increase (like a health potion?)
//




//What about this?
//each item has an ID
//Inventory has a dictionary with item ID as the key and the number present in the inventory as the value
//loading inventory uses that dictionary

