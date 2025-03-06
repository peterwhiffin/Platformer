using UnityEngine;

namespace PetesPlatformer
{
    public class TriggerPlayerDetector : PlayerDetector
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            IsPlayerDetected = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            IsPlayerDetected = false;
        }
    }
}
