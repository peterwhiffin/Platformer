using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace PetesPlatformer
{
    public class PlayerAnimator : MonoBehaviour
    {
        readonly int m_moveHash = Animator.StringToHash("Move");
        readonly int m_jumpHash = Animator.StringToHash("Jump");
        readonly int m_idleHash = Animator.StringToHash("Idle");
        readonly int m_fallingHash = Animator.StringToHash("Falling");
        readonly int m_wallSlideHash = Animator.StringToHash("WallSlide");
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

        public void OnPlayerIdle()
        {
            ChangeAnimation(m_idleHash);
        }

        public void OnPlayerMove()
        {
            ChangeAnimation(m_moveHash);
        }

        public void OnPlayerJump()
        {
            ChangeAnimation(m_jumpHash);
        }

        public void OnPlayerFalling()
        {
            ChangeAnimation(m_fallingHash);
        }

        public void OnPlayerWallSlide()
        {
            ChangeAnimation(m_wallSlideHash);
        }

        public void OnPlayerDied()
        {
            ChangeAnimation(m_diedHash);
        }

        public void OnPlayerDamaged()
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
