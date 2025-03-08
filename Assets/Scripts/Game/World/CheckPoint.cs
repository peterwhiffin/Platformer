using UnityEngine;
using System;

namespace PetesPlatformer
{
    public class Checkpoint : MonoBehaviour
    {
        private bool m_hasTriggered = false;

        [SerializeField] private Animator m_animator;

        public event Action<Checkpoint> CheckpointReached = delegate { };

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_hasTriggered)
            {
                return;
            }

            m_hasTriggered = true;
            m_animator.SetTrigger("Triggered");
            CheckpointReached.Invoke(this);
        }
    }
}
