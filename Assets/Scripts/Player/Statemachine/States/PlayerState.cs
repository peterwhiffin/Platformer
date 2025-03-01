namespace PetesPlatformer
{
    public abstract class PlayerState : State
    {
        protected readonly Player m_player;

        public PlayerState(StateMachine stateMachine, Player player) : base(stateMachine)
        {
            m_player = player;
        }
    }
}
