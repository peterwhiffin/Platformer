using Unity.VisualScripting;
using UnityEngine;

namespace PetesPlatformer
{
    [CreateAssetMenu(menuName = "Enemy/Behavior Tree/Stomp", fileName = "NewStompTree")]
    public class StompBehaviorTree : EnemyBehaviorTree
    {
        [SerializeField] private float m_stompDuration;
        [SerializeField] private float m_resetDelay;
        [SerializeField] private float m_resetDuration;

        public override void BuildTree(Enemy enemy)
        {
            m_behaviorTree = new("EnemyTree");
            var mainSelector = new SelectorNode("MainSelector");

            var stompSequence = new SequenceNode("StompSequence")
                .AddChild(new LeafNode("CheckForPlayer", new ConditionStrategy(() => enemy.PlayerDetector.IsPlayerDetected)))
                .AddChild(new LeafNode("ActivateDamager", new ActionStrategy(() => enemy.Damager.Activate(true))))
                .AddChild(new LeafNode("Stomp", new MoveStrategy(enemy, enemy.PatrolPositions[0].position, enemy.PatrolPositions[1].position, m_stompDuration, 12f)))
                .AddChild(new LeafNode("HitAnimation", new ActionStrategy(enemy.Animator.OnEnemyDamaged)))
                .AddChild(new LeafNode("DeactivateDamager", new ActionStrategy(() => enemy.Damager.Activate(false))))
                .AddChild(new LeafNode("ResetDelay", new IdleStrategy(enemy, m_resetDelay)))
                .AddChild(new LeafNode("ResetPosition", new MoveStrategy(enemy, enemy.PatrolPositions[1].position, enemy.PatrolPositions[0].position, m_resetDuration, 12f)));

            mainSelector.AddChild(stompSequence);
            m_behaviorTree.AddChild(mainSelector);
        }

        public override void Process()
        {
            if (m_behaviorTree.Proccess() != Node.Status.Running)
            {
                m_behaviorTree.Reset();
            }
        }
    }
}
