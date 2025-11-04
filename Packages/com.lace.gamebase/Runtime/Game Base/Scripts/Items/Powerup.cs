using UnityEngine;

namespace GameBase
{
    public enum ItemType
    {
        Upgrade,    //ie heart container or double magic
        Powerup,    //ie Speed boost, temp invincibility
        Restock,    //ie amo, recovery heart, stamina fruit
        Tool,       //things that can be equipped and used
        Weapon,     //self explanitory
        Collectable //ie coins, stars, other score increasers
    }

    public enum UpgradeType
    {
        MaxHealth
    }

    public enum PowerupType
    {
        Speedboost,
        TempInvincibility
    }

    public enum RestockType
    {
        Health
    }


    public class Powerup : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
