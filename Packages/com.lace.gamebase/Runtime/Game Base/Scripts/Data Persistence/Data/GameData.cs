using UnityEngine;

namespace GameBase
{
    [System.Serializable] //Data cannot be serialized to JSON format without this! 
    public class GameData
    {
        //Save System Test Variables (Will later be removed or replaced)
        public int deathcount;


        //The values defined in this constructor will be the default values the game-
        //starts with when there is no data to Load
        public GameData()
        {
            deathcount = 0;
        }
    }
}
