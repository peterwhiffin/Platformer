using UnityEngine;
using System;


namespace PetesPlatformer
{

    public class AnimationEventStateBehavior : StateMachineBehaviour
    {
        private bool m_hasTriggered = false;

        [SerializeField] private string eventName;
        [SerializeField, Range(0, 1)] private float triggerTime;


        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_hasTriggered = false;    
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!m_hasTriggered)
            {
                NotifyReceiver(animator);
                m_hasTriggered = true;
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float currentTime = stateInfo.normalizedTime % 1f;

            if(!m_hasTriggered && currentTime >= triggerTime)
            {
                NotifyReceiver(animator);
                m_hasTriggered = true;
            }
        }

        private void NotifyReceiver(Animator animator)
        {
            if (animator.TryGetComponent(out INotifiable notifiable))
            {
                notifiable.Notify(eventName);
            }
            else
            {
                Debug.LogWarning("No INotifiable component found on " + animator.gameObject.name);
            }
        }
    }
}
