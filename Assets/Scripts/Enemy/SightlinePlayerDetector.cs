using UnityEngine;

namespace PetesPlatformer
{
    public class SightlinePlayerDetector : PlayerDetector
    {
        [SerializeField] private LayerMask m_layerMask;
        [SerializeField] private float m_sightDistance;
        [SerializeField] private float m_maxSightAngle;
        [SerializeField] private Transform m_lookTransform;

        public override void SearchForPlayer()
        {
            IsPlayerDetected = false;
            Vector3 playerDirection = m_player.transform.position - transform.position;

            if (Physics2D.Raycast(transform.position, playerDirection, m_sightDistance, m_layerMask))
            {
                if(Vector3.Angle(m_lookTransform.right, playerDirection) <= m_maxSightAngle)
                {
                    IsPlayerDetected = true;
                }
            }
        }
    }
}
