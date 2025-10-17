using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    //Allows the Serialization of Dictionaries
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> m_keys = new List<TKey>();          //list of keys that can be serialized
        [SerializeField] private List<TValue> m_values = new List<TValue>();    //list of values that can be serialized


        /// <summary>
        /// Clears the key and value lists then adds updated values to the key and value lists
        /// </summary>
        public void OnBeforeSerialize()
        {
            //Clear old data from key and value lists
            m_keys.Clear();
            m_values.Clear();

            //Add current data to the key and value lists
            foreach(KeyValuePair<TKey, TValue> pair in this)
            {
                m_keys.Add(pair.Key);
                m_values.Add(pair.Value);
            }
        }

        /// <summary>
        /// Clears the dictionary values then loads deserialized values to the dictionary from the key and value lists
        /// </summary>
        public void OnAfterDeserialize()
        {
            //Notifies user if number of keys and number of values has somehow fallen out of sync
            if(m_keys.Count != m_values.Count)
            {
                Debug.LogError("Tried to deserialize a SerializableDictionary, but the amount of keys (" +
                    m_keys.Count + ") does not match the number of values (" + m_values.Count + ") which indicates that something went wrong");
            }

            //Clear old data from dictionary
            this.Clear();

            //Add new data to dictionary from lists
            //Matches keys with their proper values
            for(int i = 0;  i < m_keys.Count; i++)
            {
                this.Add(m_keys[i], m_values[i]);
            }
        }
    }
}
