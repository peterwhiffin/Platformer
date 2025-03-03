using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

namespace PetesPlatformer
{
    public class MovingPlatform : MonoBehaviour
    {
        private int m_currentIndex = 0;
        private readonly int m_moveHash = Animator.StringToHash("Move");
        private readonly int m_idleHash = Animator.StringToHash("Idle");

        [SerializeField] private Animator m_animator;
        [SerializeField] private float m_moveTime;
        [SerializeField] private float m_idleTime;
        [SerializeField] private List<Transform> m_positions;

        private void Start()
        {
            StartCoroutine(MovePlatform());
        }

        public IEnumerator MovePlatform()
        {
            m_animator.Play(m_moveHash);
            int nextIndex = m_currentIndex + 1;

            if (nextIndex == m_positions.Count)
            {
                nextIndex = 0;
            }

            float timeElapsed = 0f;

            while (timeElapsed < m_moveTime)
            {
                timeElapsed += Time.deltaTime;
                transform.position = Vector3.Lerp(m_positions[m_currentIndex].position, m_positions[nextIndex].position, timeElapsed / m_moveTime);
                yield return null;
            }

            m_currentIndex = nextIndex;
            StartCoroutine(Idle());
        }

        public IEnumerator Idle()
        {
            m_animator.Play(m_idleHash);
            float timeElapsed = 0f;

            while (timeElapsed < m_idleTime)
            {
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            StartCoroutine(MovePlatform());
        }
    }
}
