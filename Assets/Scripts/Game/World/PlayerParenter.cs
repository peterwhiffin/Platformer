using UnityEngine;

namespace PetesPlatformer
{
    public class PlayerParenter : MonoBehaviour, IParentPlatform
    {
        public void Enter(Transform childTransform)
        {
            childTransform.SetParent(transform);
        }

        public void Exit(Transform childTransform)
        {
            childTransform.SetParent(null);
        }
    }
}
