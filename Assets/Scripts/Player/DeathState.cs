namespace PetesPlatformer
{
    public class DeathState : State
    {
        public DeathState(Player player, StateMachine stateMachine) : base(player, stateMachine)
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
