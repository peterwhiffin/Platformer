using System;
using UnityEngine;

namespace PetesPlatformer
{
    public class EnemyPatrolState : EnemyState
    {
        private int m_patrolIndex;
        private Vector3 m_targetPosition;

        public EnemyPatrolState(StateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
        {
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            m_enemy.Motor.MoveTowardPosition(m_targetPosition);
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            m_enemy.Animator.OnEnemyIdle();

            m_patrolIndex++;

            if(m_patrolIndex == m_enemy.PatrolPositions.Count)
            {
                m_patrolIndex = 0;
            }

            m_targetPosition = m_enemy.PatrolPositions[m_patrolIndex].position;
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void Update()
        {
            base.Update();

            if(Vector3.Distance(m_enemy.transform.position, m_targetPosition) < .1f)
            {
                m_enemy.transform.position = m_targetPosition;
                m_stateMachine.ChangeState(m_enemy.IdleState);
            }
        }
    }
}
