namespace PetesPlatformer
{
    public abstract class State
    {
        protected readonly StateMachine m_stateMachine;

        protected State(StateMachine stateMachine)
        {
            m_stateMachine = stateMachine;
        }

        public virtual void OnEnter() { }

        public virtual void OnExit() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void LateUpdate() { }
    }
}
