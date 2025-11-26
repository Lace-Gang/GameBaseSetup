using UnityEngine;

namespace GameBase
{
    public interface IAmmunitionUser
    {
        /// <summary>
        /// Add self to ammunition tracker
        /// </summary>
        public void SubscribeToTracker();

        /// <summary>
        /// Remove self from ammunition tracker
        /// </summary>
        public void UnsubscribeFromTracker();

        /// <summary>
        /// Executes code when ammunition being tracked changes
        /// </summary>
        /// <param name="ammount">New ammunition amount</param>
        public void OnAmmunitionChange(int ammount);
    }
}
