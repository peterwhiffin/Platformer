using UnityEngine;
using System;

namespace PetesPlatformer
{
    public class Damager : MonoBehaviour
    {
        private float m_lastDamageTime;

        [SerializeField] private DamageSettings m_settings;

        public event Action DamageApplied = delegate { };

        private void OnTriggerStay2D(Collider2D collision)
        {
            
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (Time.time - m_lastDamageTime < m_settings.DamageRate)
            {
                return;
            }

            if(collision.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(m_settings.Type, m_settings.Damage);
                DamageApplied.Invoke();
                m_lastDamageTime = Time.time;
            }           
        }
    }
}
