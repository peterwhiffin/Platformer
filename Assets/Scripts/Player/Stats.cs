using System.Collections.Generic;
using UnityEngine;

namespace PetesPlatformer
{
    [CreateAssetMenu(menuName = "Settings/Stats", fileName = "NewStats")]
    public class Stats : ScriptableObject
    {
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float DamageCoolDown { get; private set; }
        [field: SerializeField] public List<DamageType> DamageTypes { get; private set; }
    }
}
