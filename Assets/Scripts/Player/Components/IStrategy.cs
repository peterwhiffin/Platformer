using UnityEngine;

namespace PetesPlatformer
{
    public interface IStrategy
    {
        Node.Status Process();
        void Reset();
    }

    public class IdleStrategy : IStrategy
    {
        private readonly Enemy r_enemy;
        private readonly float r_idleTime;
        private float m_elapsedTime;

        public IdleStrategy(Enemy enemy, float idleTime)
        {
            r_enemy = enemy;
            r_idleTime = idleTime;
        }

        public Node.Status Process()
        {
            r_enemy.Motor.StopMovement();

            m_elapsedTime += Time.deltaTime;

            if(m_elapsedTime >= r_idleTime)
            {
                return Node.Status.Success;
            }

            return Node.Status.Running;
        }

        public void Reset()
        {
            m_elapsedTime = 0f;
        }

    }

    public class PatrolStrategy : IStrategy
    {
        private readonly Enemy r_enemy;
        private int m_targetIndex = 0;

        public PatrolStrategy(Enemy enemy)
        {
            r_enemy = enemy;
        }

        public Node.Status Process()
        {
            Vector3 targetPosition = r_enemy.PatrolPositions[m_targetIndex].position;
            r_enemy.Motor.MoveTowardPosition(targetPosition);
            
            if (Vector3.Distance(r_enemy.transform.position, targetPosition) <= .01f)
            {
                r_enemy.transform.position = targetPosition;
                m_targetIndex++;

                if(m_targetIndex == r_enemy.PatrolPositions.Count)
                {
                    m_targetIndex = 0;
                }

                return Node.Status.Success;
            }

            return Node.Status.Running;
        }

        public void Reset()
        {

        }
    }
}
