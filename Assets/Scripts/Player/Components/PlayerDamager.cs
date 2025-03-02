using UnityEngine;
using System;

namespace PetesPlatformer
{
    public class PlayerDamager : MonoBehaviour
    {
        private float m_lastDamageTime;
        private bool m_canDamage = true;

        [SerializeField] private DamageSettings m_settings;
        [SerializeField] private Transform m_feetPosition;
        [SerializeField] private Player m_player;

        public event Action DamageApplied = delegate { };

        public void Disable()
        {
            m_canDamage = false;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!m_canDamage)
            {
                return;
            }

            if (m_player.Motor.Velocity.y < 0)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(m_settings.Type, m_settings.Damage);
                    m_player.Motor.ExternalMove(Vector2.up, 4f);
                    DamageApplied.Invoke();
                }
            }
        }
    }
}
