using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace PetesPlatformer
{    
    public abstract class EnemyBehaviorTree : ScriptableObject
    {
        public abstract BehaviorTree GetTree(Enemy enemy);
    }
}
