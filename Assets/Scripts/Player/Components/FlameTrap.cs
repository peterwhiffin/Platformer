using System.Collections;
using System;
using UnityEngine;
namespace PetesPlatformer
{
    public class FlameTrap : MonoBehaviour
    {
        private bool m_isActivated = false;
        private readonly int r_activateHash = Animator.StringToHash("Activate");
        private readonly int r_idleHash = Animator.StringToHash("Idle");
        private readonly int r_triggeredHash = Animator.StringToHash("Trigger");

        [SerializeField] private Animator m_animator;
        [SerializeField] private EnemyDamager m_damager;
        [SerializeField] private float m_delayTime;
        [SerializeField] private float m_damageTime;

        private void Start()
        {
            m_damager.Activate(false);
        }

        private IEnumerator WaitForDelay()
        {
            m_animator.Play(r_triggeredHash);
            float elapsedTime = 0f;

            while (elapsedTime < m_delayTime)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            StartCoroutine(DoDamage());
        }

        private IEnumerator DoDamage()
        {
            m_damager.Activate(true);
            m_animator.Play(r_activateHash);
            float elapsedTime = 0f;

            while(elapsedTime < m_damageTime)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            m_damager.Activate(false);
            m_animator.Play(r_idleHash);
            m_isActivated = false;
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            if (m_isActivated)
            {
                return;
            }

            m_isActivated = true;
            StartCoroutine(WaitForDelay());
        }
    }
}
