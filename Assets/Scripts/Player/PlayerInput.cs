using UnityEngine;

namespace PetesPlatformer
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] InputReader m_InputReader;

        public Vector2 MoveInput { get; private set; }
        public bool JumpActivated { get; private set; }
        public bool JumpCancelled { get; private set; }
        public bool DashInput { get; private set; }

        private void Start()
        {
            m_InputReader.Initialize();
            InputReader.MoveInput += OnMoveInput;
            InputReader.JumpActivated += OnJumpInput;
            InputReader.JumpCancelled += OnJumpCancelled;
            InputReader.DashInput += OnDashInput;
        }

        private void OnDestroy()
        {
            InputReader.MoveInput -= OnMoveInput;
            InputReader.JumpActivated -= OnJumpInput;
            InputReader.JumpCancelled -= OnJumpCancelled;
            InputReader.DashInput -= OnDashInput;
        }

        private void OnMoveInput(Vector2 rawMove)
        {
            MoveInput = rawMove.normalized;
        }

        private void OnJumpInput()
        {
            JumpActivated = true;
            JumpCancelled = false;
        }

        private void OnJumpCancelled()
        {
            JumpActivated = false;
            JumpCancelled = true;
        }

        public void ConsumeJumpInput()
        {
            JumpActivated = false;
        }

        private void OnDashInput()
        {
            DashInput = true;
        }
    }
}
