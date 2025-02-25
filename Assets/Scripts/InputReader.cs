using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PetesPlatformer
{
    [CreateAssetMenu(fileName = "InputReader")]
    public class InputReader : ScriptableObject, InputSystem_Actions.IPlayerActions
    {
        InputSystem_Actions m_playerInputActions;
        InputAction m_move;
        InputAction m_jump;
        InputAction m_dash;

        public static event Action<Vector2> MoveInput = delegate { };
        public static event Action JumpActivated = delegate { };
        public static event Action JumpCancelled = delegate { };
        public static event Action DashInput = delegate { };

        public void Initialize()
        {
            m_playerInputActions = new InputSystem_Actions();
            m_playerInputActions.Player.Enable();
            m_playerInputActions.Player.SetCallbacks(this);

            m_move = m_playerInputActions.Player.Move;
            m_jump = m_playerInputActions.Player.Jump;
            m_dash = m_playerInputActions.Player.Sprint;

            m_move.performed += OnMove;
            m_move.canceled += OnMove;
            m_jump.performed += OnJump;
            m_jump.canceled += OnJump;
            m_dash.performed += OnSprint;
        }

        private void OnDisable()
        {
            m_move.performed -= OnMove;
            m_move.canceled -= OnMove;
            m_jump.performed -= OnJump;
            m_jump.canceled -= OnJump;
            m_dash.performed -= OnSprint;
        }

        public void OnAttack(InputAction.CallbackContext context) { }

        public void OnCrouch(InputAction.CallbackContext context) { }

        public void OnInteract(InputAction.CallbackContext context) { }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (m_jump.WasPerformedThisFrame())
            {
                JumpActivated.Invoke();
            }
            else if (m_jump.WasReleasedThisFrame())
            {
                JumpCancelled.Invoke();
            }
        }

        public void OnLook(InputAction.CallbackContext context) { }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput.Invoke(context.ReadValue<Vector2>());
        }

        public void OnNext(InputAction.CallbackContext context) { }

        public void OnPrevious(InputAction.CallbackContext context) { }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (m_dash.WasPerformedThisFrame())
            {
                DashInput.Invoke();
            }
        }
    }
}
