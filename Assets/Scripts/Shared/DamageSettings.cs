using UnityEngine;

namespace PetesPlatformer
{
    [CreateAssetMenu(menuName = "Settings/Damage Settings", fileName = "NewDamageSettings")]
    public class DamageSettings : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float DamageRate { get; private set; }
        [field: SerializeField] public DamageType Type { get; private set; }
    }
}
