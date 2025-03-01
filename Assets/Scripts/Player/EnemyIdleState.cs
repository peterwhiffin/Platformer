using UnityEngine;

namespace PetesPlatformer
{
    public class EnemyIdleState : EnemyState
    {
        private float m_enterTime = 0f;

        public EnemyIdleState(StateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
        {
        }

        public override void OnEnter()
        {
            m_enemy.Animator.OnEnemyIdle();
            m_enterTime = Time.time;
        }

        public override void OnExit()
        {
        }

        public override void FixedUpdate()
        {
        }

        public override void LateUpdate()
        {
        }

        public override void Update()
        {
            if(Time.time - m_enterTime > m_enemy.Motor.Settings.IdleTime)
            {
                m_stateMachine.ChangeState(m_enemy.PatrolState);
            }
        }
    }
}
