using UnityEngine;
using System;

namespace PetesPlatformer
{
    public class FinishLine : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;
        
        public event Action FinishReached = delegate { };

        private void OnTriggerEnter2D(Collider2D collision)
        {
           
            m_animator.SetTrigger("Finished");
            FinishReached.Invoke();
        }
    }
}
