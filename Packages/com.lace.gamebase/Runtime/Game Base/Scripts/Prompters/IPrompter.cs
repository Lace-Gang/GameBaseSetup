using UnityEngine;

namespace GameBase
{
    public interface IPrompter
    {
        /// <summary>
        /// Intended to allow other scripts see the prompt's message
        /// </summary>
        /// <returns>The prompt's message</returns>
        public string GetPrompt();

        /// <summary>
        /// Intended to allow other scripts to see the prompt's priority (higher numbers indicate higher priority)
        /// </summary>
        /// <returns>The prompt's priority</returns>
        public int GetPromptPriority(); //higher numbers indicate higher priority

        /// <summary>
        /// Intended to allow other scripts to get this prompt's interaction key
        /// </summary>
        /// <returns>This prompt's interaction key</returns>
        public KeyCode GetPromptInteractionKey();

        /// <summary>
        /// Intended to add this prompt to the list of active prompts
        /// </summary>
        public void AddPromptToPromptList();
        
        /// <summary>
        /// Intended to remove this prompt from the list of active prompts
        /// </summary>
        public void RemovePromptFromPromptList();

        /// <summary>
        /// Intended to execute functionality when the player interacts with this prompt
        /// </summary>
        public void ExecutePrompt();
    }
}
