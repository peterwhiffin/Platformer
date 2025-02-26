using Unity.IO.LowLevel.Unsafe;

namespace PetesPlatformer
{
    public class JumpState : State
    {
        bool m_hasLeftGround = false;

        public JumpState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {          
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            m_player.Motor.ApplyGravity();

            if (m_player.Input.MoveInput.x != 0)
            {
                m_player.Motor.MoveInAir(m_player.Input.MoveInput.x);                
            }
            else
            {
                m_player.Motor.ApplyAirDrag();
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            m_player.Motor.Jump();
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
            
            if(!m_hasLeftGround)
            {
                return;
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
            else if (m_player.Motor.IsOnWall != 0)
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
