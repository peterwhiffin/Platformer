using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using System;

namespace PetesPlatformer
{
    public class Life : MonoBehaviour, IDamageable
    {
        private float m_health;
        private float m_lastDamageTime;

        [SerializeField] private Stats m_stats;

        public event Action Died = delegate { };
        public event Action DamageTaken = delegate { };

        private void Start()
        {
            m_health = m_stats.MaxHealth;
        }

        public void TakeDamage(DamageType damageType, int damage)
        {
            if(!m_stats.DamageTypes.Contains(damageType) || Time.time - m_lastDamageTime < m_stats.DamageCoolDown)
            {
                return;
            }

            m_health -= damage;
            m_lastDamageTime = Time.time;


            if(m_health <= 0)
            {
                Died.Invoke();
            }
            else
            {
                DamageTaken.Invoke();
            }
        }
    }
}
