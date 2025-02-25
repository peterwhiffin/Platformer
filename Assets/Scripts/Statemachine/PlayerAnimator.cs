using UnityEngine;

namespace PetesPlatformer
{
    public class PlayerAnimator : MonoBehaviour 
    {
        [SerializeField] Animator m_animator;
        [SerializeField] SpriteRenderer m_spriteRenderer;

  
        int m_moveHash = Animator.StringToHash("Move");
        int m_jumpHash = Animator.StringToHash("Jump");
        int m_idleHash = Animator.StringToHash("Idle");
        int m_fallingHash = Animator.StringToHash("Falling");
        int m_wallSlideHash = Animator.StringToHash("WallSlide");
        int m_currentState = Animator.StringToHash("Idle");


        public void ChangeAnimation(int newState)
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

        public void SetSpriteOrientation(float moveDirection)
        {
            if (moveDirection == 0)
                return;

            m_spriteRenderer.flipX = moveDirection < 0;
        }
    }
}
