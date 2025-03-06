using UnityEngine;

namespace PetesPlatformer
{
    public class RandomIdleStateBehavior : StateMachineBehaviour
    {
        private float m_randomTime = 0f;
        private float m_currentTime = 0f;

        [SerializeField] private float m_minIdleTime;
        [SerializeField] private float m_maxIdleTime;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_randomTime = Random.Range(m_minIdleTime, m_maxIdleTime);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_currentTime += Time.deltaTime;

            if(m_currentTime >= m_randomTime)
            {
                m_currentTime = 0f;
                animator.SetTrigger("RandomIdle");
            }
        }
    }
}
