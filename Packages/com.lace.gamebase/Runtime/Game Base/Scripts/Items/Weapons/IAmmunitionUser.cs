using UnityEngine;

namespace GameBase
{
    public interface IAmmunitionUser
    {
        /// <summary>
        /// Intended to add self to ammunition tracker
        /// </summary>
        public void SubscribeToTracker();

        /// <summary>
        /// Intended to remove self from ammunition tracker
        /// </summary>
        public void UnsubscribeFromTracker();

        /// <summary>
        /// Intended to execute code when ammunition being tracked changes to react to that change
        /// </summary>
        /// <param name="ammount">New ammunition amount</param>
        public void OnAmmunitionChange(int ammount);
    }
}
