using UnityEngine;

namespace PetesPlatformer
{
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField] protected Player m_player;
        [field: SerializeField] public bool IsPlayerDetected {  get; protected set; }

        private void Initialize(Player player)
        {
            m_player = player;
        }

        public virtual void SearchForPlayer() { }
    }
}
