using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace PetesPlatformer
{
    public class UISaveSlot : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_title;
        [SerializeField] private Button m_button;
        [SerializeField] private GameSave m_save;

        public GameSave Save { get { return m_save; } }

        public Button Initialize(string title, GameSave save)
        {
            m_title.text = title;
            m_save = save;
            return m_button;
        }
    }
}
