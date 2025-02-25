using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace PetesPlatformer
{
    public class MoveState : State
    {
        public MoveState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            m_player.Motor.MoveGrounded(Mathf.Sign(m_player.Input.MoveInput.x));
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            m_player.Animator.OnPlayerMove();
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

            m_player.Animator.SetSpriteOrientation(m_player.Input.MoveInput.x);

            if (m_player.Input.MoveInput.x == 0)
            {
                m_stateMachine.ChangeState(m_player.IdleState);
            }
            else if (m_player.Input.JumpActivated)
            {
                m_stateMachine.ChangeState(m_player.JumpState);
            }
            else if(!m_player.Motor.IsGrounded)
            {
                m_stateMachine.ChangeState(m_player.FallingState);
            }
        }
    }
}
