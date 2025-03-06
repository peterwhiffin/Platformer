using UnityEngine;
using System;

namespace PetesPlatformer
{
    public class Life : MonoBehaviour, IDamageable
    {
        private float m_health;
        private float m_lastDamageTime;
        public event EventHandler PlayerDied = delegate { };

        [SerializeField] private Stats m_stats;

        public Action Died = delegate { };
        public event Action<Vector3> DamageTaken = delegate { };
        public bool IsDead { get; private set; }

        private void Start()
        {
            IsDead = false;
            m_health = m_stats.MaxHealth;
        }

        public void TakeDamage(DamageType damageType, int damage, Vector3 damagerPosition)
        {
            if(!m_stats.DamageTypes.Contains(damageType) || Time.time - m_lastDamageTime < m_stats.DamageCoolDown || IsDead)
            {
                return;
            }

            m_health -= damage;
            m_lastDamageTime = Time.time;

            if(m_health <= 0)
            {
                IsDead = true;
                gameObject.SetActive(false);
                Died.Invoke();
            }
            else
            {
                DamageTaken.Invoke(damagerPosition);
            }
        }
    }
}
