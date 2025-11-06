using System.Collections.Generic;
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

        //Player
        public Vector3 playerPosition;
        public Quaternion playerRotation;
        public float playerMaxHealth;
        public float playerCurrentHealth;
        public int playerLives;

        //Game Instance
        public float score;


        //Items
        public SerializableDictionary<string, bool> itemBoolData = new SerializableDictionary<string, bool>();
        public SerializableDictionary<string, Vector3> itemVector3Data = new SerializableDictionary<string, Vector3>();
        public SerializableDictionary<string, Quaternion> itemQuaternionData = new SerializableDictionary<string, Quaternion>();
    }

    public class ItemData
    {
        public int itemID;
        public bool activeInScene;
        public Vector3 Position;
        public Quaternion Rotation;
    }
}
