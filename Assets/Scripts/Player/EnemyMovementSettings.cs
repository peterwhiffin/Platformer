using UnityEngine;

namespace PetesPlatformer
{
    [CreateAssetMenu(menuName = "Enemy/Settings/Movement Settings", fileName = "NewEnemyMovementSettings")]
    public class EnemyMovementSettings : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float IdleTime { get; private set; }
    }
}
