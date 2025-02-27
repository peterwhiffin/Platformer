using UnityEngine;
using System;


namespace PetesPlatformer
{
    public class AnimationEventReceiver : MonoBehaviour, INotifiable
    {
        public event Action<string> OnNotify = delegate { };
        public void Notify(string message)
        {
            OnNotify.Invoke(message);
        }
    }
}
