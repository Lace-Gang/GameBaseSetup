using System.Collections;
using UnityEngine;

namespace GameBase
{
    public abstract class ItemBase : MonoBehaviour
    {
        //Hidden Variables
        protected bool m_activeInScene = true;  //Is the object currently present in the world
        //protected AudioSource m_audioSource;    //AudioSource of this item (if applicable)


        //Exposed Variables
        [Header("Basic Item Components")]
        [SerializeField] protected MeshRenderer m_renderer;
        [SerializeField] protected Collider m_collider;

        [Header("Basic Item Information")]
        [Tooltip("The name of this configuration of this item")]
        [SerializeField] protected string m_name;
        [Tooltip("Is item picked up automatically when player enters trigger")]
        [SerializeField] protected bool m_autoPickup = true;

        ////[SerializeField] protected bool m_playAudioOnPickup = false;
        //[SerializeField] protected bool m_playAudioOnUse = false;
        //[SerializeField] protected AudioClip m_useAudio;
        //
        //
        ///// <summary>
        ///// Creates audio source if one will be necessary
        ///// </summary>
        //private void Awake()
        //{
        //    if(m_playAudioOnUse)
        //    {
        //        m_audioSource = new AudioSource();
        //    }
        //}


        /// <summary>
        /// If the object entering this item's trigger is the player, executes the "ItemBaseTriggerEnter" function
        /// </summary>
        /// <param name="other">Collider that entered the trigger (handled by game engine)</param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerCharacter>() != null)
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
            if (m_autoPickup)
            {
                OnPickedUp();           //Child class "OnPickedUp"
            }
        }

        /// <summary>
        /// Hides item and marks item as "Inactive in Scene"
        /// </summary>
        protected void HideItemInScene()
        {
            m_activeInScene = false;            //item no longer present in the world
            m_renderer.enabled = false;     //item no longer visible
            m_collider.enabled = false;     //item no longer collidable
        }


        /// <summary>
        /// Individual code for each item when picked up
        /// </summary>
        public abstract void OnPickedUp();

        /// <summary>
        /// Allows item to be used
        /// </summary>
        public abstract void Use();
    }
}

