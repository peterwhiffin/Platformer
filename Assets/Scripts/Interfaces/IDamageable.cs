using UnityEngine;

namespace PetesPlatformer
{
    public interface IDamageable
    {
        void TakeDamage(DamageType damageType, int damage, Vector3 damagerPosition);
    }
}
