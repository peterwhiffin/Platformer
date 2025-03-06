using System;
using System.Threading;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

namespace PetesPlatformer
{
    public interface IStrategy
    {
        Node.Status Process();
        void Reset() { }
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
            if (r_enemy.Motor != null)
            {
                r_enemy.Motor.StopMovement();
            }

            m_elapsedTime += Time.deltaTime;

            if(m_elapsedTime >= r_idleTime)
            {
                return Node.Status.Success;
            }

            return Node.Status.Running;
        }

        public void Reset()
        {
            m_elapsedTime = 0;
        }
    }

    public class ConditionStrategy : IStrategy
    {
        private readonly Func<bool> r_predicate;

        public ConditionStrategy(Func<bool> predicate)
        {
            r_predicate = predicate;
        }

        public Node.Status Process()
        {
            var result = Node.Status.Failure;

            if (r_predicate())
            {
                result = Node.Status.Success;
            }

            return result;
        }
    }

    public class ActionStrategy : IStrategy
    {
        private readonly Action r_action;

        public ActionStrategy(Action action)
        {
            r_action = action;
        }

        public Node.Status Process()
        {
            r_action();
            return Node.Status.Success;
        }
    }

    public class WaitForAnimationStrategy : IStrategy
    {
        private Enemy m_enemy;

        public WaitForAnimationStrategy(Enemy enemy)
        {
            m_enemy = enemy;
        }

        public Node.Status Process()
        {
            if (m_enemy.Animator.AttackFrameReached)
            {
                return Node.Status.Success;
            }

            return Node.Status.Running;
        }
    }

    public class RangedAttackStrategy : IStrategy
    {
        private Enemy m_enemy;
        private RangedWeaponSettings m_weaponSettings;

        public RangedAttackStrategy(Enemy enemy, RangedWeaponSettings weaponSettings)
        {
            m_enemy = enemy;
            m_weaponSettings = weaponSettings;
        }

        public Node.Status Process()
        {
            m_weaponSettings.SpawnProjectile(m_enemy.AttackPosition.position, m_enemy.AttackPosition.right);
            return Node.Status.Success;
        }
    }

    public class MoveStrategy : IStrategy
    {
        private readonly Enemy r_enemy;
        private readonly float r_duration;
        private readonly Vector3 r_startPosition;
        private readonly Vector3 r_endPosition;
        private float m_elapsedTime = 0f;
        private float m_moveSpeed = 0f;
        private Vector3 m_moveDirection;


        public MoveStrategy(Enemy enemy, Vector3 startPosition, Vector3 endPosition, float duration, float speed)
        {
            r_enemy = enemy;
            r_duration = duration;
            r_startPosition = startPosition;
            r_endPosition = endPosition;
            m_moveDirection = (endPosition - startPosition).normalized;
            m_moveSpeed = speed;
        }

        public Node.Status Process()
        {
            m_elapsedTime += Time.deltaTime;
            var moveDirection = (r_endPosition - r_enemy.transform.position).normalized;
            r_enemy.Motor.SetVelocity(moveDirection * m_moveSpeed);
            var targetPosition = r_endPosition;

            if (Vector3.Distance(r_enemy.transform.position, r_endPosition) <= .2f)
            {
                r_enemy.transform.position = r_endPosition;
                r_enemy.Motor.SetVelocity(Vector2.zero);
                return Node.Status.Success;
            }

            return Node.Status.Running;
        }

        public void Reset()
        {
            m_elapsedTime = 0;
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
            var moveDirection = (targetPosition - r_enemy.transform.position).normalized;

            r_enemy.Motor.MoveTowardPosition(moveDirection);
            r_enemy.Motor.SetLookDirection();

            if (Vector3.Distance(r_enemy.transform.position, targetPosition) <= .2f)
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
    }

    public class EnemyDeathStrategy : IStrategy
    {
        private readonly Enemy r_enemy;
        private float m_timer = 0f;

        public EnemyDeathStrategy(Enemy enemy)
        {
            r_enemy = enemy;
        }

        public Node.Status Process()
        {
            r_enemy.Motor.Disable();
            r_enemy.Damager.Activate(false);

            m_timer += Time.deltaTime;

            if(m_timer >= 3f)
            {
                r_enemy.Despawn();
            }

            return Node.Status.Running;
        }
    }
}
