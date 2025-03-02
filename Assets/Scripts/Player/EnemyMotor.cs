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

        public void StopMovement()
        {
            m_rigidbody.linearVelocity = Vector2.zero;
        }

        public void MoveTowardPosition(Vector3 targetPosition)
        {
            m_rigidbody.linearVelocityX = ((targetPosition - transform.position).normalized * Settings.MoveSpeed).x;
            m_rigidbody.linearVelocityY = 0f;
        }

        public void Disable()
        {
            m_rigidbody.simulated = false;
        }
    }
}
