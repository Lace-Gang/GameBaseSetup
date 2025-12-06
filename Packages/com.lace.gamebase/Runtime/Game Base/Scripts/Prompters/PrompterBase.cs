using UnityEngine;

namespace GameBase
{
    public abstract class PrompterBase : MonoBehaviour, IPrompter
    {
        //Exposed Variables
        [Header("Basic Prompter Info")]
        [Tooltip("Prompt Message")]
        [SerializeField] protected string m_prompt;
        [Tooltip("Prompt Priority (higher numbers indicate higher priority)")]
        [SerializeField] protected int m_priority;
        [Tooltip("What key on the keyboard will be used to execute/interact with this prompt")]
        [SerializeField] protected KeyCode m_promptInteractionKey = KeyCode.F;


        #region Getters and Setters

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

        #endregion Getters and Setters


        #region PrompterBase Basic Functionality

        /// <summary>
        /// Adds this prompt to the list of active prompts
        /// </summary>
        public void AddPromptToPromptList()
       {
            //Tells GameInstance to add this prompt to the list of active prompts
            GameInstance.Instance.AddToActivePrompts(this);
       }
       
       /// <summary>
       /// Removes this prompt from the list of active prompts
       /// </summary>
       public void RemovePromptFromPromptList()
       {
            //Tells GameInstance to remove this prompt from the list of active prompts
            GameInstance.Instance.RemoveFromActivePrompts(this);
       }

        #endregion PrompterBase Basic Functionality


        /// <summary>
        /// What the prompt does when when interacted with
        /// </summary>
        public abstract void ExecutePrompt();
    }
}
