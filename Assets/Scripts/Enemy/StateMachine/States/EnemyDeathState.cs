namespace PetesPlatformer
{
    public class EnemyDeathState : EnemyState
    {
        public EnemyDeathState(StateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
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
            m_enemy.Animator.OnEnemyDied();
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
