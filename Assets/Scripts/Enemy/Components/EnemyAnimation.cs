using UnityEngine;
using System.Collections.Generic;
using System;

namespace PetesPlatformer
{
    public class EnemyAnimation : MonoBehaviour
    {
        private readonly int m_idleHash = Animator.StringToHash("Idle");
        private readonly int m_moveHash = Animator.StringToHash("Move");
        private readonly int m_diedHash = Animator.StringToHash("Died");
        private readonly int m_damagedHash = Animator.StringToHash("Damaged");
        private readonly int m_attackHash = Animator.StringToHash("Attack");

        private int m_currentState = Animator.StringToHash("Idle");        
        private Dictionary<string, Action> m_animationEvents = new();

        [SerializeField] private AnimationEventReceiver m_eventReceiver;
        [SerializeField] Animator m_animator;
        [SerializeField] SpriteRenderer m_spriteRenderer;

        public bool AttackFrameReached { get; private set; }

        private void Start()
        {
            if (m_eventReceiver != null)
            {
                m_eventReceiver.OnNotify += OnNotify;
                m_animationEvents.Add("Attack", OnAttackFrameReached);
            }
        }

        private void OnDestroy()
        {
            if (m_eventReceiver != null)
            {
                m_eventReceiver.OnNotify -= OnNotify;
            }
        }

        private void OnNotify(string message)
        {
            if (m_animationEvents.ContainsKey(message))
            {
                m_animationEvents[message].Invoke();
            }
        }

        private void OnAttackFrameReached()
        {
            AttackFrameReached = true;
        }

        private void ChangeAnimation(int newState)
        {
            m_animator.SetBool(m_currentState, false);
            m_animator.SetBool(newState, true);
            m_currentState = newState;
        }

        public void OnEnemyIdle()
        {
            ChangeAnimation(m_idleHash);
        }

        public void OnEnemyMove()
        {
            ChangeAnimation(m_moveHash);
        }

        public void OnEnemyDied()
        {
            ChangeAnimation(m_diedHash);
        }

        public void OnEnemyDamaged()
        {
            m_animator.Play(m_damagedHash, 0, 0f);
        }

        public void OnEnemyAttack()
        {
            AttackFrameReached = false;
            m_animator.Play(m_attackHash, 0, 0f);
        }
    }
}
