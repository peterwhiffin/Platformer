using UnityEngine;

namespace PetesPlatformer
{
    public interface IParentPlatform
    {
        public void Enter(Transform transform);
        public void Exit(Transform transform);
    }
}
