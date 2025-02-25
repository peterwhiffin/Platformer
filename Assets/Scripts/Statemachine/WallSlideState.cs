namespace PetesPlatformer
{
    public class WallSlideState : State
    {
        public WallSlideState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            m_player.Motor.ApplyWallSlideDrag();
            m_player.Motor.ResetJumps();
            m_player.Animator.OnPlayerWallSlide();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void Update()
        {
            base.Update();

            m_player.Motor.CheckForGround();
            m_player.Motor.MoveOnWall(m_player.Input.MoveInput.x);

            if (m_player.Motor.IsGrounded)
            {
                m_stateMachine.ChangeState(m_player.IdleState);
            }
            else if (m_player.Input.JumpActivated)
            {
                m_stateMachine.ChangeState(m_player.JumpState);
            }
            else if (m_player.Motor.IsOnWall == 0)
            {
                m_stateMachine.ChangeState(m_player.FallingState);
            }
        }
    }
}
