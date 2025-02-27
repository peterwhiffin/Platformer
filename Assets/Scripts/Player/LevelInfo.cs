using UnityEngine;
using UnityEngine.SceneManagement;

namespace PetesPlatformer
{
    [CreateAssetMenu(fileName = "NewLevelInfo", menuName = "LevelInfo")]
    public class LevelInfo : ScriptableObject
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public Scene Scene { get; private set; }
    }
}
