using UnityEngine;
using System;
using System.IO; //For reading from and writing to save files on the user/player device

namespace GameBase
{
    public class FileDataHandler
    {
        private string m_dataDirPath;       //directory path
        private string m_dataFileName;      //name of the file we want to save to 
        private bool m_useEncryption = false;
        private readonly string m_encryptionCodeWord = "defaultCodeWord";


        public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
        {
            m_dataDirPath = dataDirPath;
            m_dataFileName = dataFileName;
            m_useEncryption = useEncryption;
        }



        public void Save(GameData data)
        {
            //Use Path.Combine to account for different OS's using different path seperators
            string fullPath = Path.Combine(m_dataDirPath, m_dataFileName);

            try
            {
                //Create the directory the file will be written to if it does not already exist
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                //Serialize GameData Object into JSON string
                string dataToStore = JsonUtility.ToJson(data, true);

                //optionally encrypt the data
                if(m_useEncryption)
                {
                    dataToStore = EncryptDecrypt(dataToStore);
                }

                //Write the serialized data to the file
                using (FileStream steam = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(steam))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e.Message);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns>GameData object containing deserialized save data. Returns null GameData object if save data does not exist</returns>
        public GameData Load()
        {
            //Use Path.Combine to account for different OS's using different path seperators
            string fullPath = Path.Combine(m_dataDirPath, m_dataFileName);

            GameData loadedData = null;
            if (File.Exists(fullPath))
            {
                try
                {
                    //load the serialized data from the file
                    string dataToLoad = "";
                    using (FileStream steam = new FileStream(fullPath, FileMode.Open))
                    {
                        using(StreamReader reader = new StreamReader(steam))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    //optionally decrypt the data
                    if (m_useEncryption)
                    {
                        dataToLoad = EncryptDecrypt(dataToLoad);
                    }

                    //Deserialize data from JSON back into the GameData object
                    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e.Message);
                }
            }

            return loadedData;
        }

        

        /// <summary>
        /// Encrypts/decrypts data using XOR encryption via the encryptionCodeWord. This function handles both encryption and decryption.
        /// </summary>
        /// <param name="data">Data being encrypted or decrypted</param>
        /// <returns> The encrypted or decrypted data</returns>
        private string EncryptDecrypt(string data)
        {
            string modifiedData = "";
            for(int i = 0; i < data.Length; i++)
            {
                modifiedData += (char)(data[i] ^ m_encryptionCodeWord[i % m_encryptionCodeWord.Length]);
            }
            return modifiedData;
        }
    }
}
