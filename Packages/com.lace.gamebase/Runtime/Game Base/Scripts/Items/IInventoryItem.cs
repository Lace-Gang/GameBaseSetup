using UnityEngine;

namespace GameBase
{
    public interface IInventoryItem
    {
        public static bool GetInInventory;          //Allows other scripts to see if this item is in the inventory
        public static Sprite GetInventorySprite; //Allows other scripts to get this item's sprite
        public static string GetItemName;                  //Allows other scripts to see the name of this item
        public static bool GetStackInstances;               //Allows other scripts to see if instances of this item should stack in the inventory
        public static bool GetUseFromInventory;                            //Allows other scripts to see if instances of this item can be used from the inventory
        public static bool GetEquippable;                                        //Allows other scripts to see if instances of this item can be equipped
        public static bool GetRemovable;                                          //Allows other scripts to see if instances of this item can be removed from the inventory
        public static bool GetConsumeAfterUse;                              //Allows other scripts to see if instances of this item is consumed after being used
    }
}
