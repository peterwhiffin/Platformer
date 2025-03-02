using UnityEngine;
using System;

namespace PetesPlatformer
{
    public class FinishLine : MonoBehaviour
    {
        private bool m_hasFinished = false;

        [SerializeField] private Animator m_animator;
        
        public event Action FinishReached = delegate { };

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_hasFinished)
            {
                return;
            }

            m_hasFinished = true;
            m_animator.SetTrigger("Finished");
            FinishReached.Invoke();
        }
    }
}
