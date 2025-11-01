
using UnityEngine;

namespace GameBase
{
    public class PlayerSpawnPoint : MonoBehaviour, IDataPersistence
    {
        /// <summary>
        /// Load Data from the save file to change transform of the PlayerSpawnPoint
        /// </summary>
        /// <param name="data">Copy of GameData object</param>
        public void LoadData(GameData data)
        {
            transform.position = data.playerPosition;
            transform.rotation = data.playerRotation;
        }

        /// <summary>
        /// Does nothing as the PlayerSpawnPoint does not have data to save
        /// </summary>
        /// <param name="data">Reference to Game Data object</param>
        public void SaveData(ref GameData data)
        {
            //No Necessary Save Data
        }

    }
}
