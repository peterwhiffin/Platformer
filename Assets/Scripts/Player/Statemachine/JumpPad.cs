using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
namespace PetesPlatformer
{
    public class JumpPad : MonoBehaviour
    {
        private bool m_canTrigger = true;
        private Dictionary<string, Action> m_animationEvents = new();

        [SerializeField] private float m_jumpHeight;
        [SerializeField] private Animator m_animator;
        [SerializeField] private AnimationEventReceiver m_animationEventReceiver;

        private void Start()
        {
            m_animationEventReceiver.OnNotify += OnNotify;
            m_animationEvents.Add("PadReset", OnPadReset);
        }

        private void OnPadReset()
        {
            m_canTrigger = true;
        }

        private void OnNotify(string message)
        {
            if(m_animationEvents.ContainsKey(message))
            {
                m_animationEvents[message].Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {           
            if (!m_canTrigger)
            {
                return;
            }
            
            if (collision.gameObject.TryGetComponent(out IMovable movable))
            {
                movable.ExternalMove(transform.up, m_jumpHeight);
                m_animator.SetTrigger("Activate");
                m_canTrigger = false;
            }
        }
    }
}
