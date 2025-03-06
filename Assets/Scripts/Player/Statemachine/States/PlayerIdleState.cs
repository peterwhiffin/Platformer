using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace PetesPlatformer
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(StateMachine stateMachine, Player player) : base(stateMachine, player)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            m_player.Animator.OnPlayerIdle();
            m_player.Motor.SetIdle();
            m_player.Motor.ApplyGravity();
        }

        public override void OnExit()
        {
            base.OnExit();
            
        }

        public override void Update()
        {
            base.Update();

            
            m_player.Motor.CheckForGround();
            //m_player.Motor.ApplyHorizontalDrag();

            if (m_player.PlayerLife.IsDead)
            {
                m_stateMachine.ChangeState(m_player.DeathState);
            }
            else if (m_player.PlayerLife.WasHitTaken())
            {
                m_stateMachine.ChangeState(m_player.HitState);
            }
            else if (m_player.Input.MoveInput.x != 0)
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
                m_player.Motor.SetIdle();
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
