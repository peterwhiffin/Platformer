using UnityEngine;

namespace PetesPlatformer
{
    public class EnemyAnimation : MonoBehaviour
    {
        readonly int m_idleHash = Animator.StringToHash("Idle");
        readonly int m_moveHash = Animator.StringToHash("Move");       
        readonly int m_diedHash = Animator.StringToHash("Died");
        readonly int m_damagedHash = Animator.StringToHash("Damaged");

        int m_currentState = Animator.StringToHash("Idle");

        [SerializeField] Animator m_animator;
        [SerializeField] SpriteRenderer m_spriteRenderer;

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

        public void SetSpriteOrientation(float moveDirection)
        {
            if (moveDirection == 0)
                return;

            m_spriteRenderer.flipX = moveDirection < 0;
        }
    }
}
