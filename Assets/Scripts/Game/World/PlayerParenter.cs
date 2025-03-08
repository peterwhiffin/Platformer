using UnityEngine;

namespace PetesPlatformer
{
    public class PlayerParenter : MonoBehaviour, IParentPlatform
    {
        public void Enter(Transform childTransform)
        {
            childTransform.SetParent(transform);
            childTransform.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.None;
        }

        public void Exit(Transform childTransform)
        {
            childTransform.SetParent(null);
            childTransform.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
}
