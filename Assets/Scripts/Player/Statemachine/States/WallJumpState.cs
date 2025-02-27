using UnityEngine;

namespace PetesPlatformer
{
    public class WallJumpState : State
    {
        private float m_wallJumpTimer = 0f;

        public WallJumpState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void FixedUpdate()
        {
            m_player.Motor.ApplyGravity();

            if(m_wallJumpTimer >= m_player.Motor.Settings.WallJumpMovementCooldown)
            {
                if(m_player.Input.MoveInput.x != 0)
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
            m_player.Input.ConsumeJumpInput();
            m_wallJumpTimer = 0f;
        }

        public override void OnExit()
        {
        }

        public override void Update()
        {

            if (m_player.Input.JumpCancelled || m_player.Motor.Velocity.y <= 0)
            {
                m_stateMachine.ChangeState(m_player.FallingState);
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
