
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
            //Loads the players saved position so that the player can be spawned through this PlayerSpawnPoint
            if(data.vector3Data.ContainsKey("Player.Position"))
            {
                transform.position = data.vector3Data["Player.Position"];
            }

            if(data.quaternionData.ContainsKey("Player.Rotation"))
            {
                transform.rotation = data.quaternionData["Player.Rotation"];
            }
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
