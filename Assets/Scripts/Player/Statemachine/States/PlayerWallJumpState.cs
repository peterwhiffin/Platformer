using UnityEngine;

namespace PetesPlatformer
{
    public class PlayerWallJumpState : PlayerState
    {
        private float m_wallJumpTimer = 0f;

        public PlayerWallJumpState(StateMachine stateMachine, Player player) : base(stateMachine, player)
        {
        }

        public override void FixedUpdate()
        {
            

            if(m_wallJumpTimer >= m_player.Motor.Settings.WallJumpMovementCooldown)
            {
                m_player.Motor.ApplyGravity();

                if (m_player.Input.MoveInput.x != 0)
                {
                    m_player.Motor.MoveInAir(m_player.Input.MoveInput.x);
                }
            }
        }

        public override void LateUpdate()
        {
        }

        public override void OnEnter()
        {
            m_player.Motor.JumpFromWall();
            m_player.Animator.OnPlayerJump();
            m_player.Animator.SetSpriteOrientation(-m_player.Motor.IsOnWall);
            m_player.Input.ConsumeJumpInput();
            m_wallJumpTimer = 0f;
        }

        public override void OnExit()
        {
        }

        public override void Update()
        {
            m_player.Motor.CheckForGround();

            if (m_player.Input.MoveInput.x != 0)
            {
                m_player.Animator.SetSpriteOrientation(m_player.Input.MoveInput.x);
            }

            if (m_player.Input.JumpCancelled || m_player.Motor.Velocity.y <= 0)
            {
                m_stateMachine.ChangeState(m_player.FallingState);
            }
            else if(m_player.Motor.IsOnWall != 0 && m_wallJumpTimer >= m_player.Motor.Settings.WallJumpMovementCooldown)
            {
                if (m_player.Input.MoveInput.x == m_player.Motor.IsOnWall)
                {
                    m_stateMachine.ChangeState(m_player.WallSlideState);
                }
            }
            else if (m_player.Motor.IsGrounded)
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

            m_wallJumpTimer += Time.deltaTime;
        }
    }
}
