using UnityEngine;

namespace PetesPlatformer
{
    [CreateAssetMenu(menuName = "Enemy/Behavior Tree/Simple Patrol", fileName = "NewSimplePatrolTree")]
    public class SimplePatrolBehaviorTree : EnemyBehaviorTree
    {
        public override BehaviorTree GetTree(Enemy enemy)
        {
            var behaviorTree = new BehaviorTree("EnemyTree");
            var mainSelector = new SelectorNode("MainSelector");

            var lifeSequence = new SequenceNode("LifeSequence")
                .AddChild(new LeafNode("IdleAnimation", new ActionStrategy(enemy.Animator.OnEnemyIdle)))
                .AddChild(new LeafNode("Idle", new IdleStrategy(enemy, 2f)))
                .AddChild(new LeafNode("PatrolAnimation", new ActionStrategy(enemy.Animator.OnEnemyMove)))
                .AddChild(new LeafNode("Patrol", new PatrolStrategy(enemy)));

            var deathSequence = new SequenceNode("DeathSequence")
                .AddChild(new LeafNode("CheckAlive", new ConditionStrategy(() => enemy.EnemyLife.IsDead)))
                .AddChild(new LeafNode("DeathAnimation", new ActionStrategy(enemy.Animator.OnEnemyDied)))
                .AddChild(new LeafNode("DespawnRoutine", new EnemyDeathStrategy(enemy)));

            mainSelector.AddChild(deathSequence);
            mainSelector.AddChild(lifeSequence);
            behaviorTree.AddChild(mainSelector);
            return behaviorTree;
        }
    }
}
