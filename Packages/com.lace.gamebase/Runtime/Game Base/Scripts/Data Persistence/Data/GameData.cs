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
        public Quaternion playerRotation;
        public float playerHealth;
    }
}
