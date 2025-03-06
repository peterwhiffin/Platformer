using System.Collections.Generic;
using UnityEngine;

namespace PetesPlatformer
{
    public class EnemyMotor : MonoBehaviour, IMovable
    {
        [SerializeField] private Rigidbody2D m_rigidbody;

        [field: SerializeField] public EnemyMovementSettings Settings { get; private set; }
        
        public Rigidbody2D Rigidbody { get { return m_rigidbody; } }

        public void StopMovement()
        {
            m_rigidbody.linearVelocity = Vector2.zero;
        }

        public void MoveTowardPosition(Vector3 targetPosition)
        {
            m_rigidbody.linearVelocity = targetPosition * Settings.MoveSpeed;
        }

        public void SetLookDirection()
        {
            var velocity = m_rigidbody.linearVelocityX;

            if(velocity == 0)
            {
                return;
            }

            float yRotation = m_rigidbody.linearVelocityX > 0 ? 0 : 180;

            transform.eulerAngles = new Vector3(0f, yRotation, 0f);
        }

        public void Disable()
        {
            m_rigidbody.simulated = false;
        }

        public void AddVelocity(Vector2 velocity)
        {

        }

        public void SetVelocity(Vector2 velocity)
        {
            m_rigidbody.linearVelocity = velocity;
        }
    }
}
