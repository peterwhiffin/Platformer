using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader")]
public class InputReader : ScriptableObject, InputSystem_Actions.IPlayerActions
{
    public InputSystem_Actions m_PlayerInputActions;
    public InputAction m_Move;
    public InputAction m_Jump;
    public InputAction m_Sprint;

    public static event Action<Vector2> PlayerMoved = delegate { };
    public static event Action<bool> PlayerJumped = delegate { };
    public static event Action PlayerSprinted = delegate { };

    public void Initialize()
    {
        m_PlayerInputActions = new InputSystem_Actions();
        m_PlayerInputActions.Player.Enable();
        m_PlayerInputActions.Player.SetCallbacks(this);

        m_Move = m_PlayerInputActions.Player.Move;
        m_Jump = m_PlayerInputActions.Player.Jump;
        m_Sprint = m_PlayerInputActions.Player.Sprint;

        m_Move.performed += OnMove;
        m_Move.canceled += OnMove;
        m_Jump.performed += OnJump;
        m_Jump.canceled += OnJump;
        m_Sprint.performed += OnSprint;
    }

    private void OnDisable()
    {
        m_Move.performed -= OnMove;
        m_Move.canceled -= OnMove;
        m_Jump.performed -= OnJump;
        m_Jump.canceled -= OnJump;
        m_Sprint.performed -= OnSprint;
    }

    public void OnAttack(InputAction.CallbackContext context) { }

    public void OnCrouch(InputAction.CallbackContext context) { }

    public void OnInteract(InputAction.CallbackContext context) { }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (m_Jump.WasPerformedThisFrame())
        {
            PlayerJumped.Invoke(true);
        }
        else if (m_Jump.WasReleasedThisFrame())
        {
            PlayerJumped.Invoke(false);
        }
    }

    public void OnLook(InputAction.CallbackContext context) { }

    public void OnMove(InputAction.CallbackContext context)
    {
        PlayerMoved.Invoke(context.ReadValue<Vector2>());
    }

    public void OnNext(InputAction.CallbackContext context) { }

    public void OnPrevious(InputAction.CallbackContext context) { }

    public void OnSprint(InputAction.CallbackContext context) { }
}
