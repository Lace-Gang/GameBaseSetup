using UnityEngine;

namespace GameBase
{
    public class TestItem1 : InventoryItem
    {

        //public override void OnPickedUp()
        //{
        //    ////ItemBaseOnPickedUp();
        //    //
        //    //Debug.Log(this.prefa)
        //    //
        //    //HideItemInScene();
        //}

        public override void Use()
        {
            Debug.Log("Something very helpful!");
        }
    }
}
