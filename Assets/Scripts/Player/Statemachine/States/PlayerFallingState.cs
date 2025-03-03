using UnityEngine;

namespace PetesPlatformer
{
    public class PlayerFallingState : PlayerState
    {
        private bool m_jumpRemoved = false;
        private float m_enterTime;
        private readonly float m_coyoteTime = .05f;

        public PlayerFallingState(StateMachine stateMachine, Player player) : base(stateMachine, player)
        {
        }

        public override void FixedUpdate()
        {
            m_player.Motor.ApplyGravity();
            m_player.Motor.MoveInAir(m_player.Input.MoveInput.x);
        }
        public override void LateUpdate()
        {
        }
        public override void OnEnter()
        {
            m_jumpRemoved = false;
            m_enterTime = Time.time;
            m_player.Animator.OnPlayerJump();
        }
        public override void OnExit()
        {
        }
        public override void Update()
        {
            m_player.Motor.CheckForGround();
            m_player.Animator.SetSpriteOrientation(m_player.Input.MoveInput.x);

            if(!m_jumpRemoved && Time.time - m_enterTime >= m_coyoteTime)
            {
                m_jumpRemoved = true;
                m_player.Motor.OnFalling();
            }

            if (m_player.Motor.Velocity.y < 0)
            {
                m_player.Animator.OnPlayerFalling();
            }
            else
            {
                m_player.Animator.OnPlayerJump();
            }

            if (m_player.Motor.IsGrounded)
            {
                if (m_player.Input.MoveInput.x == 0)
                {
                    m_stateMachine.ChangeState(m_player.IdleState);
                }
                else
                {
                    m_stateMachine.ChangeState(m_player.MoveState);
                }
            }
            else if (m_player.Motor.IsOnWall != 0 && m_player.Input.MoveInput.x == m_player.Motor.IsOnWall)
            {
                m_stateMachine.ChangeState(m_player.WallSlideState);
            }
            else if (m_player.Input.JumpActivated && m_player.Motor.JumpsRemaining > 0)
            {
                m_stateMachine.ChangeState(m_player.JumpState);
            }
        }
    }
}
