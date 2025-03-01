namespace PetesPlatformer
{
    public class PlayerDeathState : PlayerState
    {
        public PlayerDeathState(StateMachine stateMachine, Player player) : base(stateMachine, player)
        {
        }

        public override void FixedUpdate()
        {

        }

        public override void LateUpdate()
        {

        }

        public override void OnEnter()
        {
            m_player.Animator.OnPlayerDied();
        }

        public override void OnExit()
        {

        }

        public override void Update()
        {

        }
    }
}
