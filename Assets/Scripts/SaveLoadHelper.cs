using System.IO;
using UnityEngine;


namespace PetesPlatformer
{
    public static class SaveLoadHelper
    {
        private static readonly string m_baseFileName = "saveslot";
        private static readonly string m_fileExtension = ".sav";

        public static bool SaveToSlot(GameSave saveData)
        {
            bool successful = false;

            try
            {
                string fullPath = Path.Combine(Application.persistentDataPath, m_baseFileName + saveData.m_saveSlot.ToString() + m_fileExtension);
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                string dataToStore = JsonUtility.ToJson(saveData);

                using (FileStream stream = new(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }

                successful = true;
            }
            catch (System.Exception e)
            {
                Debug.LogError("big time error town: " + e.Message);
            }

            return successful;
        }

        public static GameSave LoadSave(string fileName)
        {
            try
            {
                string fullPath = Path.Combine(Application.persistentDataPath, fileName);
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                GameSave save = ScriptableObject.CreateInstance<GameSave>();
                JsonUtility.FromJsonOverwrite(dataToLoad, save);
                return save;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
                return null;
            }
        }
    }
}
