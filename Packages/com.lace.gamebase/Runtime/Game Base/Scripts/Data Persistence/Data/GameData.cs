using UnityEngine;

namespace GameBase
{
    [System.Serializable] //Data cannot be serialized to JSON format without this! 
    public class GameData
    {
        //It is highly advised not to remove the following two variables:
        public bool isNewSave = true; //indicates if save file is new (doess not yet contain persistent data)

        //Save System Test Variables (Will later be removed or replaced)
        public int deathcount;
        public Vector3 playerPosition;


        //The values defined in this constructor will be the default values the game-
        //starts with when there is no data to Load
        public GameData()
        {
            deathcount = 25;
            playerPosition = new Vector3(1, 20, 1);
        }
    }
}
