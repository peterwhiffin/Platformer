using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
            m_sceneRoot.GamePaused += OnGamePaused;
        }

        private void OnDestroy()
        {
            m_finishLine.FinishReached -= OnFinishLineReached;
            m_sceneRoot.GamePaused -= OnGamePaused;
        }

        private void OnGamePaused(bool isPaused)
        {
            m_player.OnGamePaused(isPaused);
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

            SaveLoadHelper.SaveToSlot(m_activeSave);
            m_sceneRoot.SceneLoader.LoadScene("Overworld");
        }
    }
}
