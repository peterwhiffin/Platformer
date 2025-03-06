using System.Collections;
using UnityEngine;

namespace PetesPlatformer
{
    public class EnemyDamager : MonoBehaviour
    {
        private bool m_canDamage = true;
        private float m_lastDamageTime = 0f;

        [SerializeField] private DamageSettings m_settings;
        [SerializeField] private bool m_appliesKnockback;

        public void Activate(bool isActive)
        {
            m_canDamage = isActive;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!m_canDamage || Time.time - m_lastDamageTime < m_settings.DamageRate)
            {
                return;
            }

            if (collision.TryGetComponent(out IDamageable damageable))
            {
                m_lastDamageTime = Time.time;
                damageable.TakeDamage(m_settings.Type, m_settings.Damage, transform.position);
            }
        }
    }
}
