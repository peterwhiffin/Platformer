using UnityEngine;

namespace PetesPlatformer
{
    [CreateAssetMenu(menuName = "Settings/Weapons/Ranged Weapon", fileName = "NewRangedWeaponSettings")]
    public class RangedWeaponSettings : ScriptableObject
    {
        [field: SerializeField] public Projectile ProjectilePrefab;
        [field: SerializeField] public Sprite ProjectileSprite;
        [field: SerializeField] public float ProjectileSpeed;
        [field: SerializeField] public int Damage;
        [field: SerializeField] public DamageType DamageType;

        public void SpawnProjectile(Vector3 firePosition, Vector3 moveDirection)
        {
            var projectile = Instantiate(ProjectilePrefab, null);
            projectile.transform.position = firePosition;
            projectile.transform.right = moveDirection;
            projectile.Initialize(ProjectileSpeed, Damage, DamageType, ProjectileSprite);
        }
    }
}
