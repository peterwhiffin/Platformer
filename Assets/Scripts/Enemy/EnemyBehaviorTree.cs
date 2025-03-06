using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace PetesPlatformer
{    
    public abstract class EnemyBehaviorTree : ScriptableObject
    {
        protected BehaviorTree m_behaviorTree;
        public abstract void BuildTree(Enemy enemy);
        public abstract void Process();
    }
}
