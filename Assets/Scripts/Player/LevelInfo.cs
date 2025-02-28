using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PetesPlatformer
{
    [CreateAssetMenu(fileName = "NewLevelInfo", menuName = "LevelInfo")]
    public class LevelInfo : ScriptableObject
    {
       [field: SerializeField] public int ID { get; private set; }
       [field: SerializeField] public string SceneName { get; private set; }
    }
}
