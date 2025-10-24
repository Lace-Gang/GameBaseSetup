using UnityEngine;

namespace GameBase
{
    //This is the Data Persistence Interface
    //This interface MUST be added to any Monobehavior script that has data that the user wants to include in-
    //a persistant data (save) file 
    public interface IDataPersistence
    {
        /// <summary>
        /// Load data from GameData object
        /// </summary>
        /// <param name="data">The GameData object containing the data that needs to be loaded</param>
        void LoadData(GameData data); //not ref because Load Data only needs to be read by other scripts

        /// <summary>
        /// Save data to GameData object
        /// </summary>
        /// <param name="data">A reference to the GameData object that needs the data that is being saved</param>
        void SaveData(ref GameData data); //ref because Save Data will be modified by other scripts
    }
}
