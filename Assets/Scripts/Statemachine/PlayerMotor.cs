using UnityEngine;
using UnityEngine.EventSystems;
namespace PetesPlatformer
{
    public class PlayerMotor : MonoBehaviour
    {
        [SerializeField] Rigidbody2D m_RigidBody;
        [SerializeField] PlayerMovementSettings m_settings;
        [SerializeField] Transform m_groundCheckPosition;
        [SerializeField] Transform m_wallCheckPosition;

        public bool IsGrounded { get; private set; }
        public int IsOnWall { get; private set; }
        public float JumpTimer { get; private set; }
        public int JumpsRemaining { get; private set; }
        public Vector2 Velocity { get { return m_RigidBody.linearVelocity; } }

        public void ResetJumps()
        {
            JumpsRemaining = 2;
        }

        public void StartJumpTimer()
        {
            JumpTimer = 0f;

        }

        public void RunJumpTimer()
        {
            JumpTimer += Time.deltaTime;
        }

        public bool IsJumpTimerDone()
        {
            return JumpTimer >= m_settings.MaxJumpTime;
        }

        public void OnFalling()
        {
            JumpsRemaining = Mathf.Clamp(JumpsRemaining - 1, 0, 2);
        }

        public void SetXVelocity(float newVelocity)
        {
            m_RigidBody.linearVelocityX = newVelocity;
        }

        public void SetYVelocity(float newVelocity)
        {
            m_RigidBody.linearVelocityY = newVelocity;
        }

        public void SetIdle()
        {
            m_RigidBody.linearVelocity = Vector2.zero;
            JumpsRemaining = 2;
        }

        public void MoveGrounded(float moveDirection)
        {
            float currentXVelocity = m_RigidBody.linearVelocityX;
            float targetXVelocity = currentXVelocity + moveDirection * m_settings.GroundAcceleration * Time.fixedDeltaTime;
            targetXVelocity = Mathf.Clamp(targetXVelocity, -m_settings.MaxMoveSpeed, m_settings.MaxMoveSpeed);

            m_RigidBody.linearVelocityX = targetXVelocity;
            m_RigidBody.linearVelocityY = 0f;
        }

        public void MoveOnWall(float moveDirection)
        {
            float currentXVelocity = m_RigidBody.linearVelocityX;
            float targetXVelocity = currentXVelocity + moveDirection * m_settings.GroundAcceleration * Time.fixedDeltaTime;
            targetXVelocity = Mathf.Clamp(targetXVelocity, -m_settings.MaxMoveSpeed, m_settings.MaxMoveSpeed);

            m_RigidBody.linearVelocityX = targetXVelocity;
        }

        public void ApplyAirDrag()
        {
            float currentXVelocity = Mathf.Abs(m_RigidBody.linearVelocityX);
            currentXVelocity = Mathf.Clamp(currentXVelocity - m_settings.AirDrag * Time.fixedDeltaTime, 0, m_settings.MaxMoveSpeed);
            m_RigidBody.linearVelocityX = Mathf.Sign(m_RigidBody.linearVelocityX) * currentXVelocity;
        }

        public void MoveInAir(float moveDirection)
        {


            float currentXVelocity = m_RigidBody.linearVelocityX;
            float targetXVelocity = currentXVelocity + moveDirection * m_settings.AirAcceleration * Time.fixedDeltaTime;
            targetXVelocity = Mathf.Clamp(targetXVelocity, -m_settings.MaxMoveSpeed, m_settings.MaxMoveSpeed);

            m_RigidBody.linearVelocityX = targetXVelocity;

            if (moveDirection != 0 && Mathf.Sign(m_RigidBody.linearVelocityX) != Mathf.Sign(moveDirection))
            {
                if(IsOnWall != 0)
                {
                    return;
                }

                m_RigidBody.linearVelocityX = 0f;
            }
        }

        public void JumpFromGround()
        {
            m_RigidBody.linearVelocityY += Mathf.Sqrt(-2f * m_settings.Gravity * m_settings.JumpHeight);
        }

        public void OnEndJump()
        {
            m_RigidBody.linearVelocityY *= m_settings.VariableJumpVelocityMultiplier;
        }

        public void JumpFromWall()
        {
            float jumpVelocity = Mathf.Sqrt(-2f * m_settings.Gravity * m_settings.JumpHeight);
            m_RigidBody.linearVelocityY = jumpVelocity;
            m_RigidBody.linearVelocityX = jumpVelocity * -IsOnWall;
        }

        public void ApplyGravity()
        {
            m_RigidBody.linearVelocityY += m_settings.Gravity * Time.fixedDeltaTime;
        }

        public void ApplyWallSlideDrag()
        {
            m_RigidBody.linearVelocityY = -m_settings.WallSlideSpeed;
        }

        public void CheckForGround()
        {
            IsGrounded = false;
            IsOnWall = 0;

            if (Physics2D.OverlapCircle(m_groundCheckPosition.position, m_settings.GroundCheckRadius, m_settings.GroundMask))
            {
                IsGrounded = true;
            }
            else
            {
                if(Physics2D.OverlapCircle(m_wallCheckPosition.position + new Vector3(m_settings.WallCheckOffset, 0f, 0f), m_settings.WallCheckRadius, m_settings.GroundMask))
                {
                    IsOnWall = 1;
                }
                else if (Physics2D.OverlapCircle(m_wallCheckPosition.position - new Vector3(m_settings.WallCheckOffset, 0f, 0f), m_settings.WallCheckRadius, m_settings.GroundMask))
                {
                    IsOnWall = -1;
                }
            }
        }
    }
}
