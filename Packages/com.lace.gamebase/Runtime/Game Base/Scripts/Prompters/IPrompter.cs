using UnityEngine;

namespace GameBase
{
    public interface IPrompter
    {
        public string GetPrompt();
        public int GetPromptPriority(); //higher numbers indicate higher priority

        public KeyCode GetPromptInteractionKey();

        public void AddPromptToPromptList();
        
        public void RemovePromptFromPromptList();

        public void ExecutePrompt();
    }
}
