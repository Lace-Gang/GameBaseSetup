using UnityEngine;

namespace GameBase
{
    public class TestItem : InventoryItem
    {
        /// <summary>
        /// Used for item testing and item component testing purposes
        /// </summary>
        public override void Use()
        {
            Debug.Log("Test Item Has Been Used");
        }
    }
}
