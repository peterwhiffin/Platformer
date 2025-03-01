using System.Collections.Generic;
using UnityEngine;

namespace PetesPlatformer
{
    public enum OverworldDirection
    {
        North = 1,
        South = -1,
        East = 2,
        West = -2
    }

    [System.Serializable]
    public struct OverworldConnection
    {
        public  OverworldDirection Direction;
        public  List<Transform> AnimationPath;
        public OverworldNode Node;

        public OverworldConnection(OverworldDirection direction, List<Transform> animationPath, OverworldNode node)
        {
            Direction = direction;
            AnimationPath = animationPath;
            Node = node;
        }
    }

    public class OverworldNode : MonoBehaviour
    {
        private bool m_isUnlocked;

        [SerializeField] private List<OverworldConnection> m_connections;
        [SerializeField] private LevelInfo m_levelInfo;

        public LevelInfo LevelInfo { get { return m_levelInfo; } }
        public List<OverworldConnection> Connections {  get { return m_connections; } }
        public bool IsUnlocked { get { return m_isUnlocked; } }

        public void Initialize(bool isUnlocked)
        {
            m_isUnlocked = isUnlocked;
        }
    }
}
