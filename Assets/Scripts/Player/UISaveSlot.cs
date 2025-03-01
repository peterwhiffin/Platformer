using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace PetesPlatformer
{
    public class UISaveSlot : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_title;
        [SerializeField] private Button m_button;
        [SerializeField] private GameSave m_saveData;

        public GameSave SaveData { get { return m_saveData; } }

        public Button Initialize(string title, GameSave saveData)
        {
            m_title.text = title;
            m_saveData = saveData;
            return m_button;
        }
    }
}
