using UnityEngine;

namespace GameBase
{
    public class TestItem : InventoryItem, IInventoryItem
    {


        public override void Use()
        {
            Debug.Log("Test Item Has Been Used");
        }
    }
}
