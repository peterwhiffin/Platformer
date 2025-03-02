namespace PetesPlatformer
{
    public abstract class EnemyState : State
    {
        protected readonly Enemy m_enemy;

        protected EnemyState(StateMachine stateMachine, Enemy enemy) : base(stateMachine)
        {
            m_enemy = enemy;
        }
    }
}
