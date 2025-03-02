using UnityEngine;

namespace PetesPlatformer
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public bool JumpActivated { get; private set; }
        public bool JumpCancelled { get; private set; }
        public bool DashInput { get; private set; }

        private void Start()
        {
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
            float x = rawMove.x;
            float y = rawMove.y;

            if(x != 0)
            {
                x = Mathf.Sign(x);
            }

            if(y != 0)
            {
                y = Mathf.Sign(y);
            }

            MoveInput = new Vector2(x, y);
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
