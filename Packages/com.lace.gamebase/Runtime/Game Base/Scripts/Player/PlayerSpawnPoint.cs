
using UnityEngine;

namespace GameBase
{
    public class PlayerSpawnPoint : MonoBehaviour, IDataPersistence
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void LoadData(GameData data)
        {
            Debug.Log("Spawn point updated");
            transform.position = data.playerPosition;
            transform.rotation = data.playerRotation;
        }

        public void SaveData(ref GameData data)
        {
            //throw new System.NotImplementedException();
        }

    }
}
