using UnityEngine;

namespace PetesPlatformer
{
    public class EnemyDamager : MonoBehaviour
    {
        [SerializeField] private DamageSettings m_settings;

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if(collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(m_settings.Type, m_settings.Damage);
            }
        }
    }
}
