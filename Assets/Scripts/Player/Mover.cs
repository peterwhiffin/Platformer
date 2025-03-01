using UnityEngine;

namespace PetesPlatformer
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float m_moveForce;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider.TryGetComponent(out IMovable movable))
            {
                movable.ExternalMove(transform.up, m_moveForce);
            }
        }
    }
}
