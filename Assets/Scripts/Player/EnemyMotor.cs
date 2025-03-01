using System.Collections.Generic;
using UnityEngine;

namespace PetesPlatformer
{
    public class EnemyMotor : MonoBehaviour, IMovable
    {
        [SerializeField] private Rigidbody2D m_rigidbody;

        [field: SerializeField] public EnemyMovementSettings Settings { get; private set; }

        public void ExternalMove(Vector2 direction, float distance)
        {
            
        }

        public void MoveTowardPosition(Vector3 targetPosition)
        {
            m_rigidbody.linearVelocity = (targetPosition - transform.position).normalized * Settings.MoveSpeed;
        }
    }
}
