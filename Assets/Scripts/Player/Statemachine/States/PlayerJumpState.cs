using Unity.IO.LowLevel.Unsafe;

namespace PetesPlatformer
{
    public class PlayerJumpState : PlayerState
    {
        private bool m_hasLeftGround = false;

        public PlayerJumpState(StateMachine stateMachine, Player player) : base(stateMachine, player)
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
            m_player.Motor.JumpFromGround();
            m_player.Animator.OnPlayerJump();
            m_player.Input.ConsumeJumpInput();                 
            m_hasLeftGround = false;
        }

        public override void OnExit()
        {
            base.OnExit();
            m_player.Motor.OnEndJump();
        }

        public override void Update()
        {
            base.Update();

            m_player.Motor.CheckForGround();
            m_player.Animator.SetSpriteOrientation(m_player.Input.MoveInput.x);

            if (!m_player.Motor.IsGrounded && m_player.Motor.IsOnWall == 0 || m_player.Input.JumpCancelled)
            {
                m_hasLeftGround = true;
            }

            if (m_player.PlayerLife.IsDead)
            {
                m_stateMachine.ChangeState(m_player.DeathState);
            }
            else if (m_player.PlayerLife.WasHitTaken())
            {
                m_stateMachine.ChangeState(m_player.HitState);
            }

            if (!m_hasLeftGround)
            {
                return;
            }

            if (m_player.PlayerLife.IsDead)
            {
                m_stateMachine.ChangeState(m_player.DeathState);
            }
            else if (m_player.PlayerLife.WasHitTaken())
            {
                m_stateMachine.ChangeState(m_player.HitState);
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
            else if (m_player.Motor.IsOnWall != 0 && m_player.Input.MoveInput.x == m_player.Motor.IsOnWall)
            {
                m_stateMachine.ChangeState(m_player.WallSlideState);
            }
            else if (m_player.Input.JumpCancelled || m_player.Motor.Velocity.y <= 0)
            {
                m_stateMachine.ChangeState(m_player.FallingState);
            }
        }
    }
}
