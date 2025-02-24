using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_Rigidbody;

    [SerializeField]
    private Animator m_Animator;

    [SerializeField]
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField]
    private Camera m_Camera;

    [SerializeField]
    private Transform m_CameraTarget;

    [SerializeField]
    private PlayerSettings m_Settings;

    [SerializeField]
    private InputReader m_Input;

    [SerializeField]
    private Vector2 m_MoveInput;

    [SerializeField]
    private bool m_JumpInput;

    [SerializeField]
    private bool m_IsGrounded;
    private bool m_DidJump = false;
    private float m_JumpTimer = 0f;
    private float m_YVelocity;

    private void Start()
    {
        m_Input.Initialize();
        InputReader.PlayerMoved += OnMoveInput;
        InputReader.PlayerJumped += OnJumpInput;
    }

    private void OnDestroy()
    {
        InputReader.PlayerMoved -= OnMoveInput;
        InputReader.PlayerJumped -= OnJumpInput;
    }

    private void OnMoveInput(Vector2 move)
    {
        m_MoveInput = move.normalized;
    }

    private void OnJumpInput(bool wasPerformed)
    {
        m_JumpInput = wasPerformed;
        if (!wasPerformed)
            m_DidJump = false;
    }

    private void Update()
    {
        AnimatePlayer();
    }

    private void AnimatePlayer()
    {
        if (m_MoveInput.x != 0f)
        {
            m_SpriteRenderer.flipX = m_MoveInput.x < 0;
        }

        if (m_IsGrounded)
        {
            m_Animator.SetFloat("Move", Mathf.Abs(m_MoveInput.x));
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();

        if (m_IsGrounded && !m_DidJump)
        {
            MoveGrounded();
        }
        else
        {
            MoveInAir();
        }
    }

    private void LateUpdate()
    {
        m_Camera.transform.position = new Vector3(
            m_CameraTarget.position.x,
            m_CameraTarget.position.y,
            -10f
        );
    }

    private void MoveGrounded()
    {
        m_YVelocity = 0f;
        Vector2 jumpForce = Vector2.zero;
        if (m_JumpInput && !m_DidJump)
        {
            m_IsGrounded = false;
            m_DidJump = true;
            m_JumpTimer = 0f;
            jumpForce = transform.up * m_Settings.JumpForce;
        }
        Vector2 moveForce = Vector2.zero;

        if (Mathf.Abs(m_Rigidbody.linearVelocityX) < m_Settings.MaxSpeed)
            moveForce = new Vector2(m_MoveInput.x, 0f) * m_Settings.MoveSpeed;
        else
        {
            Vector2 direction = m_Rigidbody.linearVelocity.normalized;
            m_Rigidbody.linearVelocityX = direction.x * m_Settings.MaxSpeed;
        }
        if (m_MoveInput.x == 0)
        {
            moveForce = new Vector2(-m_Rigidbody.linearVelocity.x, 0) * m_Settings.StopForce;
        }
        m_Rigidbody.AddForce(moveForce + jumpForce);
    }

    private void MoveInAir()
    {
        Vector2 jumpForce = Vector2.zero;
        Vector2 moveForce = Vector2.zero;
        if (Mathf.Abs(m_Rigidbody.linearVelocityX) < m_Settings.MaxSpeed)
            moveForce = new Vector2(m_MoveInput.x, 0f) * m_Settings.InAirMoveSpeed;

        if (m_DidJump && m_JumpInput)
        {
            if (m_JumpTimer < m_Settings.MaxJumpTime)
            {
                m_JumpTimer += Time.fixedDeltaTime;
                //jumpForce =
                //   Vector2.up * m_Settings.JumpForce * (1 - m_JumpTimer / m_Settings.MaxJumpTime);
            }
            else
            {
                m_DidJump = false;
                m_Rigidbody.linearVelocityY = 0f;
            }
        }
        else
        {
            if (m_DidJump)
                m_Rigidbody.linearVelocityY = 0f;
            m_DidJump = false;
        }
        //m_YVelocity += m_Settings.GravityAdd * Time.fixedDeltaTime;

        m_ri
        // m_Rigidbody.linearVelocityY += m_Settings.GravityAdd * Time.fixedDeltaTime;
        m_Rigidbody.AddForce(moveForce);
    }

    private void GroundCheck()
    {
        m_IsGrounded = false;
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            m_Settings.GroundCheckDistance,
            m_Settings.GroundMask
        );
        if (hit)
        {
            m_IsGrounded = true;
        }
    }
}
