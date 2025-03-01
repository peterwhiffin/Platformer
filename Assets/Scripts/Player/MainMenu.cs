using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

namespace PetesPlatformer
{
    public class MainMenu : MonoBehaviour
    {
        private UISaveSlot m_selectedSaveSlot;
        private List<AsyncOperation> m_sceneOps = new List<AsyncOperation>();

        [SerializeField] private SceneRoot m_sceneRoot;
        [SerializeField] private GameObject m_MainMenu;
        [SerializeField] private GameObject m_SaveSlotMenu;
        [SerializeField] private GameSave m_defaultSaveData;
        [SerializeField] private Transform m_saveSlotParent;
        [SerializeField] private GameSave m_activeSave;
        [SerializeField] private List<UISaveSlot> m_loadedSaveSlots = new();
        [SerializeField] private UISaveSlot m_saveSlotPrefab;

        private void Start()
        {
            Directory.CreateDirectory(Application.persistentDataPath);
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath);
            Dictionary<int, GameSave> saveSlots = new();

            foreach (FileInfo file in directoryInfo.GetFiles("*.sav"))
            {
                GameSave loadedSave = SaveLoadHelper.LoadSave(file.Name);

                if (loadedSave != null)
                {
                    saveSlots.Add(loadedSave.m_saveSlot, loadedSave);
                }
            }

            List<KeyValuePair<int, GameSave>> sorted = saveSlots.OrderBy(x => x.Key).ToList();

            foreach (var slot in sorted)
            {
                SaveSlotMenu_CreateSaveSlotButton(slot.Key, slot.Value);
            }
        }

        public void MainMenu_OnClickPlay()
        {
            m_MainMenu.SetActive(false);
            m_SaveSlotMenu.SetActive(true);
        }

        public void MainMenu_OnClickQuit()
        {
            SaveLoadHelper.SaveToSlot(m_activeSave);

#if !UNITY_EDITOR
            Application.Quit();
#else
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
        }

        public void SaveSlotMenu_OnClickBack()
        {
            m_SaveSlotMenu.SetActive(false);
            m_MainMenu.SetActive(true);
        }

        public void SaveSlotMenu_OnClickSlot(UISaveSlot saveSlot)
        {
            m_selectedSaveSlot = saveSlot;
        }

        public void SaveSlotMenu_OnClickNewSaveSlot()
        {
            GameSave newSave = ScriptableObject.CreateInstance<GameSave>();
            newSave.m_saveSlot = 0;
            newSave.m_fruit = 0;
            newSave.m_lives = 1;
            newSave.m_currentLevel = 0;
            newSave.m_unlockedLevels = new List<int> { 0 };

            if (m_loadedSaveSlots.Count != 0)
            {
                UISaveSlot lastSlot = m_loadedSaveSlots[^1];
                newSave.m_saveSlot = lastSlot.SaveData.m_saveSlot + 1;
            }

            if (SaveLoadHelper.SaveToSlot(newSave))
            {
                SaveSlotMenu_CreateSaveSlotButton(newSave.m_saveSlot, newSave);
            }
        }

        private void SaveSlotMenu_CreateSaveSlotButton(int slot, GameSave save)
        {
            UISaveSlot saveSlotPrefab = Instantiate(m_saveSlotPrefab, m_saveSlotParent);
            Button button = saveSlotPrefab.Initialize("SaveData slot " + slot.ToString(), save);
            button.onClick.AddListener(delegate { SaveSlotMenu_OnClickSlot(saveSlotPrefab); });
            m_loadedSaveSlots.Add(saveSlotPrefab);
        }

        public void SaveSlotMenu_OnClickPlay()
        {
            if (m_selectedSaveSlot != null)
            {
                m_activeSave.m_saveSlot = m_selectedSaveSlot.SaveData.m_saveSlot;
                m_activeSave.m_lives = m_selectedSaveSlot.SaveData.m_lives;
                m_activeSave.m_fruit = m_selectedSaveSlot.SaveData.m_fruit;
                m_activeSave.m_unlockedLevels = m_selectedSaveSlot.SaveData.m_unlockedLevels;
                m_activeSave.m_currentLevel = m_selectedSaveSlot.SaveData.m_currentLevel;

                m_sceneRoot.SceneLoader.LoadScene("Overworld");
            }
        }
    }
}
