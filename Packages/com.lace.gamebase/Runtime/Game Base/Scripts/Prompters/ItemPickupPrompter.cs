using UnityEngine;

namespace GameBase
{
    public class ItemPickupPrompter : PrompterBase
    {
        //Hidden Variables
        private bool m_activePrompt = true;

        //Exposed Variables
        [Header("Pickup Prompt Details")]
        [SerializeField] ItemBase m_Item;
        [SerializeField] bool m_removesPromptAfterPickup = true;
        [SerializeField] bool m_pickupOnlyOnce = true;


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
            if(m_activePrompt || m_pickupOnlyOnce)
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
            if (m_removesPromptAfterPickup) RemovePromptFromPromptList();
            if (m_pickupOnlyOnce) m_activePrompt = false;

            m_Item.OnPickedUp();
        }
    }
}
