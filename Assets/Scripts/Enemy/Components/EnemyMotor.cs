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
            var velocity = (targetPosition - transform.position).normalized * Settings.MoveSpeed;
            //m_rigidbody.linearVelocityX = ((targetPosition - transform.position).normalized * Settings.MoveSpeed).x;
            //m_rigidbody.linearVelocityY = 0f;
            m_rigidbody.linearVelocity = velocity;
        }

        public void SetLookDirection()
        {
            var velocity = m_rigidbody.linearVelocityX;

            if(velocity == 0)
            {
                return;
            }

            float yRotation = m_rigidbody.linearVelocityX > 0 ? 180 : 0;

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
