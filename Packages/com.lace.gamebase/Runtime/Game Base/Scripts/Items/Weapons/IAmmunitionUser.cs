using UnityEngine;

namespace GameBase
{
    public interface IAmmunitionUser
    {
        public void SubscribeToTracker();

        public void UnsubscribeFromTracker();

        public void OnAmmunitionChange(int ammount);
    }
}
