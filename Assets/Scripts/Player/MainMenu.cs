using System.IO;
using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace PetesPlatformer
{

    public class MainMenu : MonoBehaviour
    {
        private UISaveSlot m_selectedSaveSlot;
        private const string m_baseFileName = "saveslot";
        private const string m_fileExtension = ".sav";
        
        [SerializeField] private InputReader m_inputReader;        
        [SerializeField] private GameObject m_MainMenu;
        [SerializeField] private GameObject m_SaveSlotMenu;
        [SerializeField] private GameSave m_defaultSaveData;
        [SerializeField] private Transform m_saveSlotParent;
        [SerializeField] private GameSave m_activeSave;
        [SerializeField] private List<UISaveSlot> m_saveSlots = new();
        [SerializeField] private UISaveSlot m_saveSlotPrefab;

        private void Start()
        {
            m_inputReader.Initialize();

            if(Directory.Exists(Application.persistentDataPath))
            {
                string savePath = Application.persistentDataPath;

                DirectoryInfo directoryInfo = new DirectoryInfo(savePath);
                Dictionary<int, GameSave> saveSlots = new();

                foreach (FileInfo file in directoryInfo.GetFiles("*.sav"))
                {
                    try
                    {
                        string fullPath = Path.Combine(savePath, file.Name);
                        string dataToLoad = "";
                        using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                dataToLoad = reader.ReadToEnd();
                            }
                        }
                        GameSave save = ScriptableObject.CreateInstance<GameSave>();
                        JsonUtility.FromJsonOverwrite(dataToLoad, save);
                        //var save = JsonUtility.FromJson<GameSave>(dataToLoad);
                        saveSlots.Add(save.m_saveSlot, save);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError(e.Message);
                    }
                }

                List<KeyValuePair<int, GameSave>> sorted = saveSlots.OrderBy(x => x.Key).ToList();

                foreach(var slot in sorted)
                {
                    CreateSaveSlotButton(slot.Key, slot.Value);
                }
            }
            else
            {
                Directory.CreateDirectory(Application.persistentDataPath);
            }
        }

        private void CreateSaveSlotButton(int slot, GameSave save)
        {
            UISaveSlot saveSlotPrefab = Instantiate(m_saveSlotPrefab, m_saveSlotParent);
            Button button = saveSlotPrefab.Initialize("Save slot " + slot.ToString(), save);
            button.onClick.AddListener(delegate { OnClickSaveSlot(saveSlotPrefab); });
            m_saveSlots.Add(saveSlotPrefab);
        }

        public void OnClickPlay_MainMenu()
        {
            m_MainMenu.SetActive(false);
            m_SaveSlotMenu.SetActive(true);
        }

        public void OnClickBack_SaveSlotMenu()
        {
            m_SaveSlotMenu.SetActive(false);
            m_MainMenu.SetActive(true);
        }

        public void OnClickQuit()
        {
#if !UNITY_EDITOR
            Application.Quit();
#else
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
        }

        public void OnClickSaveSlot(UISaveSlot saveSlot)
        {
            m_selectedSaveSlot = saveSlot;
        }

        public void OnClickCreateSaveSlot()
        {         
            try
            {              
                GameSave newSave = ScriptableObject.CreateInstance<GameSave>();
                newSave.m_saveSlot = 0;
                newSave.m_fruit = 0;
                newSave.m_lives = 1;
                newSave.m_currentLevel = 0;
                newSave.m_unlockedLevels = new List<int>();
                newSave.m_unlockedLevels.Add(0);

                if(m_saveSlots.Count != 0)
                {
                    newSave.m_saveSlot = m_saveSlots[m_saveSlots.Count - 1].Save.m_saveSlot + 1;
                }

                string fullPath = Path.Combine(Application.persistentDataPath, m_baseFileName + newSave.m_saveSlot.ToString() + m_fileExtension);
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                string dataToStore = JsonUtility.ToJson(newSave);

                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }

                CreateSaveSlotButton(newSave.m_saveSlot, newSave);
            }
            catch (System.Exception e)
            {
                Debug.LogError("big time error town: " + e.Message);
            }
        }

        public void OnClickPlay_SaveSlotMenu()
        {
            if(m_selectedSaveSlot != null)
            {
                m_activeSave.m_saveSlot = m_selectedSaveSlot.Save.m_saveSlot;
                m_activeSave.m_lives = m_selectedSaveSlot.Save.m_lives;
                m_activeSave.m_fruit = m_selectedSaveSlot.Save.m_fruit;
                m_activeSave.m_unlockedLevels = m_selectedSaveSlot.Save.m_unlockedLevels;
                SceneManager.LoadScene("Overworld");
            }
        }
    }
}
