namespace PetesPlatformer
{
    public abstract class PlayerState : State
    {
        protected readonly Player m_player;

        public PlayerState(StateMachine stateMachine, Player player) : base(stateMachine)
        {
            m_player = player;
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
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
