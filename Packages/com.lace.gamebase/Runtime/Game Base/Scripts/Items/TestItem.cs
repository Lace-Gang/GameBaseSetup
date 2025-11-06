using UnityEngine;

namespace GameBase
{
    public class TestItem : SavableItem
    {




        public override void OnPickedUp()
        {
            ItemBaseOnPickedUp();
        }

        public override void Use()
        {
            Debug.Log("Test Item Has Been Used");
        }
    }
}
