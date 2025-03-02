using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

namespace PetesPlatformer
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private SceneLoader m_sceneLoader;
        [SerializeField] private GameSave m_activeSave;
        [SerializeField] private Button m_exitStageButton;

        public event Action ResumeClicked = delegate { };

        private void Start()
        {
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }

        private void OnActiveSceneChanged(Scene prev, Scene next)
        {
            Toggle(false);
            m_exitStageButton.gameObject.SetActive(true);

            if (next.name == "Overworld")
            {
                m_exitStageButton.gameObject.SetActive(false);
            }
        }

        public void OnClickResume()
        {
            ResumeClicked.Invoke();
        }

        public void OnClickQuitToMenu()
        {
            SaveLoadHelper.SaveToSlot(m_activeSave);
            m_sceneLoader.LoadScene("MainMenu");
        }

        public void OnClickExitStage()
        {
            m_sceneLoader.LoadScene("Overworld");
        }

        public void Toggle(bool isActive)
        {
            Time.timeScale = isActive ? 0f : 1f;

            gameObject.SetActive(isActive);
        }

        public void OnClickQuitToDesktop()
        {
#if !UNITY_EDITOR
            Application.Quit();
#else
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
        }
    }
}
