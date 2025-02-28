using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace PetesPlatformer
{
    [DataContract]
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewGameSave", menuName = "GameSave")]
    public class GameSave : ScriptableObject
    {
        public int m_saveSlot;
        public int m_lives;
        public int m_fruit;
        public List<int> m_unlockedLevels;
        public int m_currentLevel;
    }
}
