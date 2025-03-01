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

        public event Action<bool> GamePaused = delegate { };
        public Camera MainCamera { get; private set; }
        public SceneLoader SceneLoader {  get { return m_sceneLoader; } }

        public void Initialize(SceneLoader sceneLoader)
        {
            m_sceneLoader = sceneLoader;           
            MainCamera = m_sceneLoader.MainCamera;            
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
