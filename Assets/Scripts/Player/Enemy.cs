using System.Collections.Generic;
using UnityEngine;

namespace PetesPlatformer
{
    public class Enemy : MonoBehaviour
    {
        private StateMachine m_stateMachine;
        private bool m_isPaused;

        [field: SerializeField] public Life EnemyLife { get; private set; }
        [field: SerializeField] public EnemyMotor Motor { get; private set; }
        [field: SerializeField] public EnemyAnimation Animator { get; private set; }
        [field: SerializeField] public Damager EnemyDamager { get; private set; }

        [field: SerializeField] public EnemyIdleState IdleState { get; private set; }
        [field: SerializeField] public EnemyPatrolState PatrolState { get; private set; }
        [field: SerializeField] public EnemyPursueState PursueState { get; private set; }
        [field: SerializeField] public EnemyAttackState AttackState { get; private set; }
        [field: SerializeField] public EnemyDeathState DeathState { get; private set; }
        
        [field: SerializeField] public List<Transform> PatrolPositions { get; private set; }

        private void Awake()
        {
            m_stateMachine = new StateMachine();

            IdleState = new EnemyIdleState(m_stateMachine, this);
            PatrolState = new EnemyPatrolState(m_stateMachine, this);
            PursueState = new EnemyPursueState(m_stateMachine, this);
            AttackState = new EnemyAttackState(m_stateMachine, this);
            DeathState = new EnemyDeathState(m_stateMachine, this);

            m_stateMachine.Initialize(IdleState);
        }

        private void Start()
        {
            SceneRoot.GamePaused += OnGamePaused;
            EnemyLife.DamageTaken += OnDamageTaken;
            EnemyLife.Died += OnDied;
        }

        private void OnDestroy()
        {
            SceneRoot.GamePaused -= OnGamePaused;
            EnemyLife.DamageTaken -= OnDamageTaken;
            EnemyLife.Died -= OnDied;
        }

        private void OnGamePaused(bool isPaused)
        {
            m_isPaused = isPaused;
        }

        private void OnDied()
        {
            EnemyDamager.gameObject.SetActive(false);
            EnemyLife.gameObject.SetActive(false);
            m_stateMachine.ChangeState(DeathState);
        }


        private void OnDamageTaken() 
        {
            Animator.OnEnemyDamaged();
        }

        private void Update()
        {
            if(m_isPaused)
            {
                return;
            }

            m_stateMachine.CurrentState.Update();
        }

        private void FixedUpdate()
        {
            if (m_isPaused)
            {
                return;
            }
            
            m_stateMachine.CurrentState.FixedUpdate();
        }
    }
}
