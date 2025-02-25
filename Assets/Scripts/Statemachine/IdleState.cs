using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace PetesPlatformer
{
    public class IdleState : State
    {
        public IdleState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            m_player.Animator.OnPlayerIdle();
            m_player.Motor.MoveGrounded(0f);
            m_player.Motor.SetXVelocity(0f);
            m_player.Motor.SetYVelocity(0f);
            m_player.Motor.ResetJumps();
        }

        public override void OnExit()
        {
            base.OnExit();
            
        }

        public override void Update()
        {
            base.Update();

            m_player.Motor.CheckForGround();

            if (m_player.Input.MoveInput.x != 0)
            {
                m_stateMachine.ChangeState(m_player.MoveState);
            }
            else if (m_player.Input.JumpActivated)
            {
                m_stateMachine.ChangeState(m_player.JumpState);
            }
            else if (!m_player.Motor.IsGrounded)
            {
                m_stateMachine.ChangeState(m_player.FallingState);
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}
