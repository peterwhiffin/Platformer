using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetesPlatformer
{
    public class Enemy : MonoBehaviour
    {
        private BehaviorTree m_behaviorTree;
        private bool m_isPaused;

        [field: SerializeField] public Life EnemyLife { get; private set; }
        [field: SerializeField] public EnemyMotor Motor { get; private set; }
        [field: SerializeField] public EnemyAnimation Animator { get; private set; }    
        [field: SerializeField] public List<Transform> PatrolPositions { get; private set; }

        private void Awake()
        {
            m_behaviorTree = new("EnemyTree");
            m_behaviorTree.AddChild(new LeafNode("Idle", new IdleStrategy(this, 2f)));
            m_behaviorTree.AddChild(new LeafNode("Patrol", new PatrolStrategy(this)));
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
            StartCoroutine(Despawn());
        }

        private IEnumerator Despawn()
        {
            Motor.Disable();
            float timer = 0f;

            while(timer < 3f)
            {
                timer += Time.deltaTime;
                yield return null;
            }

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

           if(m_behaviorTree.Proccess() == Node.Status.Success)
            {
                m_behaviorTree.Reset();
            }
        }

        private void FixedUpdate()
        {
            if (m_isPaused)
            {
                return;
            }
        }
    }
}
