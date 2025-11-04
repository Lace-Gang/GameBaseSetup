using UnityEngine;

namespace GameBase
{
    public abstract class PrompterBase : MonoBehaviour, IPrompter
    {
        //Hidden Variables



        //Exposed Variables

        [Header("Basic Prompter Info")]
        [SerializeField] string m_prompt;
        [SerializeField] int m_priority;
        [SerializeField] KeyCode m_promptInteractionKey = KeyCode.F;

       /// <summary>
       /// Tells GameInstance to add this prompt to the list of active prompts
       /// </summary>
       public void AddPromptToPromptList()
       {
           GameInstance.Instance.AddToActivePrompts(this);
       }
       
       /// <summary>
       /// Tells GameInstance to remove this prompt from the list of active prompts
       /// </summary>
       public void RemovePromptFromPromptList()
       {
           GameInstance.Instance.RemoveFromActivePrompts(this);
       }
        
        /// <summary>
        /// Allows other scripts to get this prompter's prompts
        /// </summary>
        /// <returns>This prompter's prompt</returns>
        public string GetPrompt()
        {
            return m_prompt;
        }

        /// <summary>
        /// Allows other scripts to get this prompter's priority
        /// </summary>
        /// <returns>Priority of this prompter</returns>
        public int GetPromptPriority()
        {
            return m_priority;
        }

        /// <summary>
        /// Allows other scripts to get this prompter's interaction key
        /// </summary>
        /// <returns>This prompter's interaction key</returns>
        public KeyCode GetPromptInteractionKey()
        {
            return m_promptInteractionKey;
        }

        /// <summary>
        /// What the prompt does when when interacted with
        /// </summary>
        public abstract void ExecutePrompt();
    }
}
