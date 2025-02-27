using UnityEngine;

namespace PetesPlatformer
{
    [CreateAssetMenu(menuName = "Settings/Stats", fileName = "NewStats")]
    public class Stats : ScriptableObject
    {
        [SerializeField] public float MaxHealth { get; private set; }
    }
}
