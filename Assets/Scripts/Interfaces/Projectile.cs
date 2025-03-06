using UnityEngine;

namespace PetesPlatformer
{
    public class Projectile : MonoBehaviour
    {
        private int m_damage;
        private float m_speed;
        private DamageType m_damageType;
        private Sprite m_sprite;

        private void Update()
        {
            transform.position += m_speed * Time.deltaTime * transform.right;
        }

        public void Initialize(float speed, int damage, DamageType damageType, Sprite sprite)
        {
            m_speed = speed;
            m_damage = damage;
            m_damageType = damageType;
            m_sprite = sprite;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(m_damageType, m_damage, transform.position);
                Destroy(gameObject);
            }
        }
    }
}
