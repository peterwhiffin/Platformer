using UnityEngine;

namespace PetesPlatformer
{
    public class FallingState : State
    {
        bool m_animationSet = false;
        public FallingState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            m_player.Motor.ApplyGravity();
            m_player.Motor.MoveInAir(m_player.Input.MoveInput.x);
        }
        public override void LateUpdate()
        {
            base.LateUpdate();
        }
        public override void OnEnter()
        {
            base.OnEnter();

            m_player.Motor.OnFalling();
            m_animationSet = false;
        }
        public override void OnExit()
        {
            base.OnExit();
        }
        public override void Update()
        {
            base.Update();
            m_player.Motor.CheckForGround();
            m_player.Animator.SetSpriteOrientation(m_player.Input.MoveInput.x);

            if (!m_animationSet && m_player.Motor.Velocity.y < 0)
            {
                m_player.Animator.OnPlayerFalling();
                m_animationSet = true;
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
            else if(m_player.Motor.IsOnWall != 0)
            {
                if (m_player.Input.MoveInput.x == m_player.Motor.IsOnWall)
                {
                    m_stateMachine.ChangeState(m_player.WallSlideState);
                }
            }
            else if(m_player.Input.JumpActivated && m_player.Motor.JumpsRemaining > 0)
            {
                m_stateMachine.ChangeState(m_player.JumpState);
            }
        }
    }
}
