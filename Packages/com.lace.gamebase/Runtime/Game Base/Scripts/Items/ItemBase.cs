using System.Collections;
using UnityEngine;

namespace GameBase
{
    public abstract class ItemBase : MonoBehaviour
    {
        #region Variables

        //Hidden Variables
        protected bool m_activeInScene = true;  //Is the object currently present in the world

        //Exposed Variables
        [Header("Basic Item Components")]
        [Tooltip("Reference to this item's mesh renderer")]
        [SerializeField] protected MeshRenderer m_renderer;
        [Tooltip("Reference to this item's collider (whichever collider would need to be disabled when the item is picked up")]
        [SerializeField] protected Collider m_collider;

        [Header("Basic Item Information")]
        [Tooltip("The name of this configuration of this item")]
        [SerializeField] protected string m_name;
        [Tooltip("Is item picked up automatically when player enters trigger")]
        [SerializeField] protected bool m_autoPickup = true;
        [Tooltip("Should this item play audio when it is picked up")]
        [SerializeField] protected bool m_playAudioOnAutoPickup = false;
        [Tooltip("AudioClip that should be played when this item is picked up")]
        [SerializeField] protected AudioSource m_pickupAudio = null;

        #endregion Variables


        /// <summary>
        /// If the object entering this item's trigger is the player, executes the "ItemBaseTriggerEnter" function
        /// </summary>
        /// <param name="other">Collider that entered the trigger (handled by game engine)</param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerCharacter>() != null)  //Checks that collider belongs to a player character
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
                if (m_playAudioOnAutoPickup) PlayPickupAudio();     //plays audio if indicated to do so
            }
        }

        /// <summary>
        /// Hides item and marks item as "Inactive in Scene"
        /// </summary>
        protected void HideItemInScene()
        {
            m_activeInScene = false;        //item no longer present in the world
            m_renderer.enabled = false;     //item no longer visible
            m_collider.enabled = false;     //item no longer collidable
        }

        /// <summary>
        /// Plays item pickup audio if indicated to do so
        /// </summary>
        public void PlayPickupAudio()
        {            
            m_pickupAudio?.Play();    //plays audio for item picked up if such an audio exists        
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

