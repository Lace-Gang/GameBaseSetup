using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> m_keys = new List<TKey>();
        [SerializeField] private List<TValue> m_values = new List<TValue>();

        public void OnBeforeSerialize()
        {
            m_keys.Clear();
            m_values.Clear();
            foreach(KeyValuePair<TKey, TValue> pair in this)
            {
                m_keys.Add(pair.Key);
                m_values.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();

            if(m_keys.Count != m_values.Count)
            {
                Debug.LogError("Tried to deserialize a SerializableDictionary, but the amount of keys (" +
                    m_keys.Count + ") does not match the number of values (" + m_values.Count + ") which indicates that something went wrong");
            }

            for(int i = 0;  i < m_keys.Count; i++)
            {
                this.Add(m_keys[i], m_values[i]);
            }
        }
    }
}
