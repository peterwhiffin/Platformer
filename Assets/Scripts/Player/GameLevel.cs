using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace PetesPlatformer
{

    public class GameLevel : MonoBehaviour
    {
        [SerializeField] private GameSave m_activeSave;
        [SerializeField] private FinishLine m_finishLine;
        [SerializeField] private List<LevelInfo> m_levelsToUnlock;

        private void Start()
        {
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

            SceneManager.LoadScene("Overworld", LoadSceneMode.Single);
        }
    }
}
