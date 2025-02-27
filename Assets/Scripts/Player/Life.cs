using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using System;

namespace PetesPlatformer
{
    public class Life : MonoBehaviour, IDamageable
    {
        private float m_health;

        [SerializeField] private Stats m_stats;

        public event Action Died = delegate { };

        private void Start()
        {
            m_health = m_stats.MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            m_health -= damage;

            if(m_health <= 0)
            {
                Died.Invoke();
            }
        }
    }
}
