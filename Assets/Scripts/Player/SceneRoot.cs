using UnityEngine;
using System;
using NUnit.Framework.Constraints;

namespace PetesPlatformer
{
    public class SceneRoot : MonoBehaviour
    {
        private SceneLoader m_sceneLoader;
        private bool m_isPaused = false;

        [SerializeField] private bool m_isPausable = true;

        public static event Action<bool> GamePaused = delegate { };
        public SceneLoader SceneLoader {  get { return m_sceneLoader; } }

        public void Initialize(SceneLoader sceneLoader)
        {
            m_sceneLoader = sceneLoader;                      
            gameObject.SetActive(true);
        }

        private void Start()
        {
            InputReader.MenuInput += OnPauseInput;

            if (m_isPausable)
            {
                m_sceneLoader.PauseMenu.ResumeClicked += PauseMenu_OnResume;
            }
        }

        private void OnDestroy()
        {
            InputReader.MenuInput -= OnPauseInput;
            m_sceneLoader.PauseMenu.ResumeClicked -= PauseMenu_OnResume;
        }

        public void OnPauseInput()
        {
            m_isPaused = !m_isPaused;
            m_sceneLoader.TogglePauseMenu(m_isPaused);
            GamePaused.Invoke(m_isPaused);
        }

        public void PauseMenu_OnResume()
        {
            OnPauseInput();
        }
    }
}
