using UnityEngine;

namespace GameBase
{
    public class TestItem2 : InventoryItem, IInventoryItem
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
            Debug.Log("A rare and magical thing just happened!");
        }
    }
}
