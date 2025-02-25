namespace PetesPlatformer
{
    public abstract class State
    {
        protected readonly Player m_player;
        protected readonly StateMachine m_stateMachine;

        protected State(Player player, StateMachine stateMachine)
        {
            m_player = player;
            m_stateMachine = stateMachine;
        }

        public virtual void OnEnter() { }

        public virtual void OnExit() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void LateUpdate() { }
    }
}
