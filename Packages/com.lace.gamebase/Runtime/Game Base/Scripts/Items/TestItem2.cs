using UnityEngine;

namespace GameBase
{
    public class TestItem2 : InventoryItem
    {
        /// <summary>
        /// Used for item testing and item component testing purposes
        /// </summary>
        public override void Use()
        {
            Debug.Log("A rare and magical thing just happened!");
        }
    }
}
