using UnityEngine;

namespace PetesPlatformer
{
    public class WallSlideState : State
    {
        private float m_movementHeldTime = 0f;

        public WallSlideState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void FixedUpdate()
        {
            if (m_movementHeldTime > m_player.Motor.Settings.WallMovementThreshold)
            {
                m_player.Motor.MoveGrounded(m_player.Input.MoveInput.x);
            }
        }

        public override void LateUpdate()
        {
        }

        public override void OnEnter()
        {
            m_player.Motor.ApplyWallSlideDrag();
            m_player.Motor.ResetJumps();
            m_player.Animator.OnPlayerWallSlide();
            m_player.Animator.SetSpriteOrientation(m_player.Motor.IsOnWall);
            m_movementHeldTime = 0f;
        }

        public override void OnExit()
        {
        }

        public override void Update()
        {
            m_player.Motor.CheckForGround();

            if (m_player.Input.MoveInput.x != 0)
            {
                m_movementHeldTime += Time.deltaTime;
            }
            else
            {
                m_movementHeldTime = 0f;
            }

            if (m_player.Motor.IsGrounded)
            {
                m_stateMachine.ChangeState(m_player.IdleState);
            }
            else if (m_player.Input.JumpActivated)
            {
                m_stateMachine.ChangeState(m_player.WallJumpState);
            }
            else if (m_player.Motor.IsOnWall == 0)
            {
                m_stateMachine.ChangeState(m_player.FallingState);
            }
        }
    }
}
