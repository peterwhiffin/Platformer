using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

namespace PetesPlatformer
{

    public class GameLevel : MonoBehaviour
    {
        [SerializeField] private SceneRoot m_sceneRoot;
        [SerializeField] private GameSave m_activeSave;
        [SerializeField] private Player m_player;
        [SerializeField] private FinishLine m_finishLine;
        [SerializeField] private List<LevelInfo> m_levelsToUnlock;
        

        private void Start()
        {
            SceneManager.SetActiveScene(gameObject.scene);
            
            m_finishLine.FinishReached += OnFinishLineReached;
            
        }

        private void OnDestroy()
        {
            m_finishLine.FinishReached -= OnFinishLineReached;
        }

        private void OnFinishLineReached()
        {
            foreach (var level in m_levelsToUnlock)
            {
                if (!m_activeSave.m_unlockedLevels.Contains(level.ID))
                {
                    m_activeSave.m_unlockedLevels.Add(level.ID);
                }
            }

            m_finishLine.FinishReached -= OnFinishLineReached;
            SaveLoadHelper.SaveToSlot(m_activeSave);
            m_sceneRoot.SceneLoader.LoadScene("Overworld");
        }
    }
}
