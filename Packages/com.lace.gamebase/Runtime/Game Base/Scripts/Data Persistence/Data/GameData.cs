using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    [System.Serializable] //Data cannot be serialized to JSON format without this! 
    public class GameData
    {
        //Items
        public SerializableDictionary<string, bool> boolData = new SerializableDictionary<string, bool>();
        public SerializableDictionary<string, int> intData = new SerializableDictionary<string, int>();
        public SerializableDictionary<string, float> floatData = new SerializableDictionary<string, float>();
        public SerializableDictionary<string, string> stringData = new SerializableDictionary<string, string>();
        public SerializableDictionary<string, Vector3> vector3Data = new SerializableDictionary<string, Vector3>();
        public SerializableDictionary<string, Quaternion> quaternionData = new SerializableDictionary<string, Quaternion>();
        public SerializableDictionary<string, List<string>> stringListData = new SerializableDictionary<string, List<string>>();
    }
}
