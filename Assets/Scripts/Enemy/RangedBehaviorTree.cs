using UnityEngine;

namespace PetesPlatformer
{
    [CreateAssetMenu(menuName = "Enemy/Behavior Tree/Ranged", fileName = "NewRangedTree")]
    public class RangedBehaviorTree : EnemyBehaviorTree
    {
        [SerializeField] private RangedWeaponSettings m_weapon;
        [SerializeField] private float m_fireRate = 1f;

        public override BehaviorTree GetTree(Enemy enemy)
        {
            var behaviorTree = new BehaviorTree("EnemyTree");
            var mainSelector = new SelectorNode("MainSelector");

            var deathSequence = new SequenceNode("DeathSequence")
                .AddChild(new LeafNode("CheckAlive", new ConditionStrategy(() => enemy.EnemyLife.IsDead)))
                .AddChild(new LeafNode("DeathAnimation", new ActionStrategy(enemy.Animator.OnEnemyDied)))
                .AddChild(new LeafNode("DespawnRoutine", new EnemyDeathStrategy(enemy)));

            var attackSequence = new SequenceNode("RangedAttackSequence")
                .AddChild(new LeafNode("SearchForPlayer", new ActionStrategy(enemy.PlayerDetector.SearchForPlayer)))
                .AddChild(new LeafNode("CheckSearchResult", new ConditionStrategy(() => enemy.PlayerDetector.IsPlayerDetected)))
                .AddChild(new LeafNode("FireAnimation", new ActionStrategy(enemy.Animator.OnEnemyAttack)))
                .AddChild(new LeafNode("WaitForAnimation", new WaitForAnimationStrategy(enemy)))
                .AddChild(new LeafNode("FireProjectile", new RangedAttackStrategy(enemy, m_weapon)))              
                .AddChild(new LeafNode("Cooldown", new IdleStrategy(enemy, m_fireRate)));   

            var patrolSequence = new SequenceNode("LifeSequence")
                .AddChild(new LeafNode("IdleAnimation", new ActionStrategy(enemy.Animator.OnEnemyIdle)))
                .AddChild(new LeafNode("Idle", new IdleStrategy(enemy, 2f)))
                .AddChild(new LeafNode("PatrolAnimation", new ActionStrategy(enemy.Animator.OnEnemyMove)))
                .AddChild(new LeafNode("Patrol", new PatrolStrategy(enemy)));

            mainSelector.AddChild(deathSequence);
            mainSelector.AddChild(attackSequence);
            mainSelector.AddChild(patrolSequence);
            behaviorTree.AddChild(mainSelector);
            return behaviorTree;
        }
    }
}
