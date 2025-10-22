using UnityEngine;

namespace GameBase
{
    [RequireComponent(typeof(PlayerCharacter))]
    public class PlayerHealth : Health, IDataPersistence
    {
        //Hidden Variables
        private PlayerCharacter m_playerCharacter;




        private void Awake()
        {
            m_playerCharacter = GetComponent<PlayerCharacter>();
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            OnStart();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void LoadData(GameData data)
        {
            //throw new System.NotImplementedException();
        }

        public void SaveData(ref GameData data)
        {
            //throw new System.NotImplementedException();
        }

    }
}
