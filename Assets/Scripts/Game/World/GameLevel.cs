using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

namespace PetesPlatformer
{

    public class GameLevel : SceneRoot
    {
        [SerializeField] private Checkpoint m_currentCheckpoint;

        [SerializeField] private GameSave m_activeSave;
        [SerializeField] private Player m_player;
        [SerializeField] private FinishLine m_finishLine;
        [SerializeField] private List<LevelInfo> m_levelsToUnlock;
        [SerializeField] private List<Checkpoint> m_checkpoints;
        [SerializeField] private float m_respawnTime;
        [SerializeField] private float m_victoryTime;

        protected override void Start()
        {
            base.Start();
            SceneManager.SetActiveScene(gameObject.scene);
            
            m_finishLine.FinishReached += OnFinishLineReached;
            m_player.PlayerLife.PlayerDied += OnPlayerDied;

            foreach(Checkpoint checkpoint in m_checkpoints)
            {
                checkpoint.CheckpointReached += OnCheckPointTriggered;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            m_finishLine.FinishReached -= OnFinishLineReached;
            m_player.PlayerLife.PlayerDied -= OnPlayerDied;

            foreach (Checkpoint checkpoint in m_checkpoints)
            {
                checkpoint.CheckpointReached -= OnCheckPointTriggered;
            }
        }

        private void OnPlayerDied()
        {
            StartCoroutine(RespawnTimer());
        }

        private IEnumerator RespawnTimer()
        {
            float timeElapsed = 0f;

            while(timeElapsed < m_respawnTime)
            {
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            m_activeSave.m_lives -= 1;

            if(m_activeSave.m_lives == 0)
            {
                SceneLoader.LoadScene("Overworld");
            }
            else
            {
                m_player.OnSpawn(m_currentCheckpoint.transform.position);
            }
        }

        private void OnCheckPointTriggered(Checkpoint checkpoint)
        {
            m_currentCheckpoint = checkpoint;
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

            StartCoroutine(VictoryTimer());
        }

        private IEnumerator VictoryTimer()
        {
            float timeElapsed = 0f;
            
            while(timeElapsed < m_victoryTime)
            {
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            m_finishLine.FinishReached -= OnFinishLineReached;
            SaveLoadHelper.SaveToSlot(m_activeSave);
            SceneLoader.LoadScene("Overworld");
        }
    }
}
