using NUnit.Framework;
using NUnit.Framework.Constraints;
using PetesPlatformer;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PetesPlatformer
{
    public class SceneLoader : MonoBehaviour
    {
        private Coroutine m_fadeRoutine;

        [SerializeField] private InputReader m_inputReader;
        [SerializeField] private CanvasGroup m_splashScreen;
        [SerializeField] private CanvasGroup m_loadingScreen;
        [SerializeField] private Camera m_mainCamera;
        [SerializeField] private PauseMenu m_pauseMenu;
        [SerializeField] private GameSave m_activeSave;

        public Camera MainCamera { get { return m_mainCamera; } }
        public PauseMenu PauseMenu { get { return m_pauseMenu; } }
        public GameSave ActiveSave { get { return m_activeSave; } }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);          
        }

        private void Start()
        {
            m_fadeRoutine = StartCoroutine(FadeRoutine(0f, 0f, 0f, m_splashScreen));
            AsyncOperation op = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);     
            StartCoroutine(WaitForSceneLoad(op, "MainMenu", m_splashScreen.gameObject));         
        }

        public void LoadScene(string sceneName)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            StartCoroutine(WaitForSceneLoad(op, sceneName, m_loadingScreen.gameObject));
        }

        private IEnumerator WaitForSceneLoad(AsyncOperation op, string newSceneName, GameObject loadScreen)
        {
            loadScreen.SetActive(true);
            float duration = 0f;
            Scene currentScene = SceneManager.GetActiveScene();

            while (!op.isDone || m_fadeRoutine != null)
            {
                duration += Time.deltaTime;
                yield return null;
            }

            var newScene = SceneManager.GetSceneByName(newSceneName);
            GameObject[] rootObjects = newScene.GetRootGameObjects();
            var sceneRoot = rootObjects[0].GetComponent<SceneRoot>();
            sceneRoot.Initialize(this);           
            SceneManager.UnloadSceneAsync(currentScene);
            loadScreen.SetActive(false);
        }

        private IEnumerator FadeRoutine(float fadeInDuration, float holdDuration, float fadeOutDuration, CanvasGroup canvas)
        {
            float phaseDuration = 0;
            canvas.gameObject.SetActive(true);
            canvas.alpha = 0f;

            if (fadeInDuration > 0)
            {
                while (phaseDuration < fadeInDuration)
                {
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 1f, phaseDuration / fadeInDuration);
                    phaseDuration += Time.deltaTime;
                    yield return null;
                }
            }

            canvas.alpha = 1f;
            phaseDuration = 0f;

            while(phaseDuration < holdDuration)
            {
                phaseDuration += Time.deltaTime;
                yield return null;
            }

            phaseDuration = 0f;

            if (fadeOutDuration > 0)
            {
                while (phaseDuration < fadeOutDuration)
                {
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 0f, phaseDuration / fadeOutDuration);
                    phaseDuration += Time.deltaTime;
                    yield return null;
                }
            }

            canvas.alpha = 0f;
            m_fadeRoutine = null;
        }

        public void TogglePauseMenu(bool toggleOn)
        {
            m_pauseMenu.Toggle(toggleOn);
        }

        private void OnApplicationQuit()
        {
            if (m_activeSave.m_saveSlot >= 0)
            {
                SaveLoadHelper.SaveToSlot(m_activeSave);
            }
        }
    }
}
