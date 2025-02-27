using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PetesPlatformer
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Scene m_mainMenuScene;
        [SerializeField] private Scene m_overworldScene;
        [SerializeField] private List<LevelInfo> m_levels;
        [SerializeField] private CanvasGroup m_splashScreen;
        [SerializeField] private float m_splashScreenFadeRate;

        private void Awake()
        {
            StartCoroutine(FadeInSplashScreen());
        }

        private IEnumerator FadeInSplashScreen()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(m_mainMenuScene.buildIndex, LoadSceneMode.Single);
            float alpha = 0;
            
            while(alpha < 1 && !asyncOperation.isDone)
            {
                alpha += m_splashScreenFadeRate * Time.deltaTime;
                yield return null;
            }
        }
    }
}
