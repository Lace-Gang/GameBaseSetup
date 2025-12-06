using UnityEngine;

namespace GameBase
{
    public class ItemPickupPrompter : PrompterBase
    {
        //Hidden Variables
        protected bool m_activePrompt = true;     //Is this prompt currently active

        //Exposed Variables
        [Header("Item Pickup Prompt Details")]
        [Tooltip("The item that this prompter is being used for")]
        [SerializeField] protected ItemBase m_Item;
        [Tooltip("Should prompt be removed when item is picked up")]
        [SerializeField] protected bool m_removesPromptAfterPickup = true;
        [Tooltip("Should item only be able to be picked up once")]
        [SerializeField] protected bool m_pickupOnlyOnce = true;
        [Tooltip("Should audio be played when this prompt is executed by the player (aka when this prompter's item is picked up)")]
        [SerializeField] protected bool m_playAudioOnPickup = false;


        /// <summary>
        /// Sets if this prompter is active or not
        /// </summary>
        /// <param name="promptActive">Desired "prompt is active"</param>
        public void SetPromptActive(bool promptActive)
        {
            m_activePrompt = promptActive;
        }

        /// <summary>
        /// When player enters prompt trigger, prompt is added to active prompt list
        /// </summary>
        /// <param name="other">Collider that entered this trigger (handled by game engine)</param>
        private void OnTriggerEnter(Collider other)
        {
            //if prompt is active and collider that entered trigger belongs to player, adds this prompt to the prompt list
            if (m_activePrompt)
            {
                if(other.gameObject.GetComponent<PlayerCharacter>() != null)
                {
                    AddPromptToPromptList();
                }
            }
        }

        /// <summary>
        /// When player exits prompt trigger, prompt is removed from active prompt list
        /// </summary>
        /// <param name="other">Collider that exited this trigger (handled by game engine)</param>
        private void OnTriggerExit(Collider other)
        {
            //if prompt is active or item can only be picked up once,
            //and if the collider that left the trigger belongs to the player character,
            //removes this prompt from active prompt list
            if (m_activePrompt || m_pickupOnlyOnce)
            {
                if(other.gameObject.GetComponent<PlayerCharacter>() != null)
                {
                    RemovePromptFromPromptList();
                }
            }
        }

        /// <summary>
        /// Notifies item that it has been picked up
        /// </summary>
        public override void ExecutePrompt()
        {
            if (m_removesPromptAfterPickup) RemovePromptFromPromptList();   //removes prompt if indicated to do so
            if (m_pickupOnlyOnce) m_activePrompt = false;                   //Deactivates prompt if indicated to do so
            if(m_playAudioOnPickup) m_Item.PlayPickupAudio();               //Plays pickup audio if indicated to do so

            m_Item.OnPickedUp();    //Tells item it has been picked up
        }
    }
}
