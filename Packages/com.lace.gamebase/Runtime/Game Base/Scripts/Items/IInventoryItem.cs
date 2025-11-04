using UnityEngine;
using UnityEngine.UIElements;

namespace GameBase
{
    public interface IInventoryItem
    {
        public static Image GetInventorySprite;

        public int AddToInventory();    //returns item ID

        public void Use();
    }
}
