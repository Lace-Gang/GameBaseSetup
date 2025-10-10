using UnityEngine;

namespace GameBase
{
    //This is the Data Persistence Interface
    public interface IDataPersistence
    {
        void LoadData(GameData data); //not ref because Load Data only needs to be read by other scripts
        void SaveData(ref GameData data); //ref because Save Data will be modified by other scripts
    }
}
