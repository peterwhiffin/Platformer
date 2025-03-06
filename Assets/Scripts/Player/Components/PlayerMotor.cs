using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
namespace PetesPlatformer
{

    public class PlayerMotor : MonoBehaviour, IMovable
    {
        private float m_lastJumpTime = 0f;

        [SerializeField] private Player m_player;
        [SerializeField] private Rigidbody2D m_RigidBody;
        [SerializeField] private PlayerMovementSettings m_settings;
        [SerializeField] private Transform m_groundCheckPosition;
        [SerializeField] private Transform m_wallCheckPosition;
        
        public bool IsGrounded { get; private set; }
        public int IsOnWall { get; private set; }
        public int JumpsRemaining { get; private set; }
        public Vector2 Velocity { get { return m_RigidBody.linearVelocity; } }
        public PlayerMovementSettings Settings { get { return m_settings; } }

        public void OnDamageTaken(Vector3 damagerPosition)
        {
            var moveVelocity = (transform.position - damagerPosition).normalized * m_settings.DamageKnockbackSpeed;
           
            var knockUp = Vector3.up * m_settings.DamageKnockupSpeed;
            knockUp.x += moveVelocity.x;

            SetVelocity(knockUp);
        }

        public void OnStompedEnemy()
        {
            SetVelocity(Vector3.up * m_settings.EnemyStompKnockUp);
        }

        public void AddVelocity(Vector2 velocity)
        {
            m_RigidBody.linearVelocity += velocity;
            JumpsRemaining = 1;
            IsGrounded = false;
        }

        public void SetVelocity(Vector2 velocity)
        {
            m_RigidBody.linearVelocity = velocity;
            JumpsRemaining = 1;
            IsGrounded = false;
        }

        public bool CanJump()
        {
            bool canJump = false;

            if(Time.time - m_lastJumpTime > m_settings.JumpInterval)
            {
                canJump = true;
            }

            return canJump;
        }

        public void MoveGrounded(float moveDirection)
        {
            m_RigidBody.linearVelocityX = moveDirection * m_settings.GroundSpeed;
        }

        public void MoveInAir(float moveDirection)
        {
            m_RigidBody.linearVelocityX = moveDirection * m_settings.AirSpeed;
        }

        public void JumpFromGround()
        {
            float verticalJumpVelocity = Mathf.Sqrt(-2f * m_settings.Gravity * m_settings.JumpHeight);
            m_RigidBody.linearVelocityY = verticalJumpVelocity;
            m_lastJumpTime = Time.time;
            JumpsRemaining -= 1;
        }

        public void JumpFromWall()
        {
            float verticalJumpVelocity = Mathf.Sqrt(-2f * m_settings.Gravity * m_settings.WallJumpHeight);
            m_RigidBody.linearVelocityY = verticalJumpVelocity;
            m_RigidBody.linearVelocityX = IsOnWall * m_settings.WallJumpHorizontalSpeed;
            m_lastJumpTime = Time.time;
            JumpsRemaining -= 1;
        }

        public void OnEndJump()
        {
            if (m_RigidBody.linearVelocityY < 0)
            {
                return;
            }

            m_RigidBody.linearVelocityY *= m_settings.VariableJumpVelocityMultiplier;
        }

        public void ResetJumps()
        {
            JumpsRemaining = 2;
        }

        public void ApplyWallSlideDrag()
        {
            m_RigidBody.linearVelocityY = -m_settings.WallSlideSpeed;
        }

        public void ApplyHorizontalDrag()
        {
            float xVelocity = Mathf.Abs(m_RigidBody.linearVelocityX);
            xVelocity = Mathf.Max(xVelocity - m_settings.HorizontalDrag * Time.fixedDeltaTime, 0);
            m_RigidBody.linearVelocityX = Mathf.Sign(m_RigidBody.linearVelocityX) * xVelocity;
        }

        public void ApplyGravity()
        {
            m_RigidBody.linearVelocityY = Mathf.Max(m_RigidBody.linearVelocityY + m_settings.Gravity * Time.fixedDeltaTime, m_settings.TerminalVelocity);
        }

        public void OnFalling()
        {
            if(JumpsRemaining < 2) 
            {
                return;
            }

            JumpsRemaining = Mathf.Max(JumpsRemaining - 1, 0);
        }

        public void SetIdle()
        {
            m_RigidBody.linearVelocityX = 0f;
            JumpsRemaining = 2;
        }

        public void CheckForGround()
        {
            IsGrounded = false;
            IsOnWall = 0;

            if (Physics2D.OverlapCircle(m_groundCheckPosition.position, m_settings.GroundCheckRadius, m_settings.GroundMask))
            {
                IsGrounded = true;
                //m_RigidBody.linearVelocityY = 0f;
            }
            else
            {
                if (Physics2D.OverlapCircle(m_wallCheckPosition.position + new Vector3(m_settings.WallCheckOffset, 0f, 0f), m_settings.WallCheckRadius, m_settings.WallMask))
                {
                    IsOnWall = 1;
                }
                else if (Physics2D.OverlapCircle(m_wallCheckPosition.position - new Vector3(m_settings.WallCheckOffset, 0f, 0f), m_settings.WallCheckRadius, m_settings.WallMask))
                {
                    IsOnWall = -1;
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider.TryGetComponent(out IParentPlatform platform))
            {
                platform.Enter(transform);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out IParentPlatform platform))
            {
                platform.Exit(transform);
            }
        }
    }
}
