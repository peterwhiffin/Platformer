using UnityEngine;
namespace PetesPlatformer
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float m_rotationSpeed;

        private void Update()
        {
            transform.Rotate(Vector3.forward, m_rotationSpeed *  Time.deltaTime);
        }
    }
}
