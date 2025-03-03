using UnityEngine;
using System.Collections;

namespace PetesPlatformer
{
    public class FallingPlatform : MonoBehaviour
    {
        private bool m_isActivated = false;
        private readonly int m_moveHash = Animator.StringToHash("Move");
        private readonly int m_idleHash = Animator.StringToHash("Idle");

        [SerializeField] private Animator m_animator;
        [SerializeField] private float m_fallDelay;
        [SerializeField] private float m_fallTime;
        [SerializeField] private float m_resetTime;
        [SerializeField] private Transform m_startPosition;
        [SerializeField] private Transform m_fallPosition;

        private void Start()
        {
            transform.position = m_startPosition.position;
        }

        private IEnumerator Delay(float delayTime)
        {
            m_animator.Play(m_idleHash);
            float elapsedTime = 0f;

            while(elapsedTime < delayTime)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            if (m_isActivated)
            {
                StartCoroutine(ResetPosition());
            }
            else
            {
                StartCoroutine(Drop());                
            }
        }

        private IEnumerator ResetPosition()
        {
            m_animator.Play(m_moveHash);
            m_isActivated = false;
            float timeElapsed = 0f;

            while(timeElapsed < m_resetTime)
            {
                timeElapsed += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, m_startPosition.position, timeElapsed / m_resetTime);
                yield return null;
            }
        }

        private IEnumerator Drop()
        {
            m_animator.Play(m_idleHash);
            m_isActivated = true;
            float yVelocity = 0f;

            while(transform.position.y > m_fallPosition.position.y)
            {
                yVelocity += -18.81f * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, transform.position.y + yVelocity * Time.deltaTime, transform.position.z);

                if(transform.position.y < m_fallPosition.position.y)
                {
                    transform.position = m_fallPosition.position;
                    break;
                }
                
                yield return null;
            }

            StartCoroutine(Delay(m_resetTime));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_isActivated)
            {
                return;
            }

            StopAllCoroutines();            
            StartCoroutine(Delay(m_fallDelay));
        }
    }
}
