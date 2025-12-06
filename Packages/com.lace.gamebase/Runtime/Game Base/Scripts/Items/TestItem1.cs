using UnityEngine;

namespace GameBase
{
    public class TestItem1 : InventoryItem
    {
        /// <summary>
        /// Used for item testing and item component testing purposes
        /// </summary>
        public override void Use()
        {
            Debug.Log("TestItem1 has been used and done something very helpful!");
        }
    }
}
