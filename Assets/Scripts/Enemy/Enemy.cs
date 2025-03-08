using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetesPlatformer
{
    public class Enemy : MonoBehaviour
    {
        private bool m_isPaused;
        private BehaviorTree m_behaviorTree;

        [SerializeField] private EnemyBehaviorTree m_behaviorTreeTemplate;

        [field: SerializeField] public Life EnemyLife { get; private set; }
        [field: SerializeField] public EnemyMotor Motor { get; private set; }
        [field: SerializeField] public EnemyAnimation Animator { get; private set; }    
        [field: SerializeField] public List<Transform> PatrolPositions { get; private set; }
        [field: SerializeField] public EnemyDamager Damager { get; private set; }
        [field: SerializeField] public PlayerDetector PlayerDetector { get; private set; }
        [field: SerializeField] public Transform AttackPosition { get; private set; }

        private void Awake()
        {
            m_behaviorTree = m_behaviorTreeTemplate.GetTree(this);
        }

        private void Start()
        {
            SceneRoot.GamePaused += OnGamePaused;

            if (EnemyLife != null)
            {
                EnemyLife.PlayerDamaged += OnDamageTaken;
            }
        }

        private void OnDestroy()
        {
            SceneRoot.GamePaused -= OnGamePaused;
            EnemyLife.PlayerDamaged -= OnDamageTaken;          
        }

        private void OnGamePaused(bool isPaused)
        {
            m_isPaused = isPaused;
        }

        public void Despawn()
        {
            Destroy(gameObject);
        }

        private void OnDamageTaken(Vector3 damagerPosition) 
        {
            Animator.OnEnemyDamaged();
        }

        private void Update()
        {
            if(m_isPaused)
            {
                return;
            }



            if(m_behaviorTree.Proccess() != Node.Status.Running)
            {
                m_behaviorTree.Reset();
            }
        }
    }
}
