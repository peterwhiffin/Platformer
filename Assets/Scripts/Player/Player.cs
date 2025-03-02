using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

namespace PetesPlatformer
{
    public class Player : MonoBehaviour
    {

        private StateMachine m_stateMachine;
        private bool m_isPaused = false;

        [field: SerializeField] public Life PlayerLife { get; private set; }
        [field: SerializeField] public PlayerMotor Motor { get; private set; }
        [field: SerializeField] public PlayerAnimator Animator { get; private set; }
        [field: SerializeField] public PlayerInput Input { get; private set; }
        [field: SerializeField] public PlayerDamager PlayerDamager {  get; private set; }

        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerFallingState FallingState { get; private set; }
        public PlayerWallSlideState WallSlideState { get; private set; }
        public PlayerWallJumpState WallJumpState { get; private set; }
        public PlayerDeathState DeathState { get; private set; }

        private void Awake()
        {
            m_stateMachine = new StateMachine();

            IdleState = new PlayerIdleState(m_stateMachine, this);
            MoveState = new PlayerMoveState(m_stateMachine, this);
            JumpState = new PlayerJumpState(m_stateMachine, this);
            FallingState = new PlayerFallingState(m_stateMachine, this);
            WallSlideState = new PlayerWallSlideState(m_stateMachine, this);
            WallJumpState = new PlayerWallJumpState(m_stateMachine, this);
            DeathState = new PlayerDeathState(m_stateMachine, this);

            m_stateMachine.Initialize(IdleState);
        }

        private void Start()
        {
            SceneRoot.GamePaused += OnGamePaused;
            PlayerLife.DamageTaken += OnDamaged;
            PlayerLife.Died += OnDeath;
        }

        private void OnDestroy()
        {
            SceneRoot.GamePaused -= OnGamePaused;
            PlayerLife.DamageTaken -= OnDamaged;
            PlayerLife.Died -= OnDeath;
        }

        public void OnGamePaused(bool isPaused)
        {
            m_isPaused = isPaused;
        }

        private void Update()
        {
            if (m_isPaused)
            {
                return;
            }

            m_stateMachine.CurrentState.Update();
        }

        private void FixedUpdate()
        {
            if (m_isPaused)
            {
                return;
            }

            m_stateMachine.CurrentState.FixedUpdate();
        }

        private void LateUpdate()
        {
            if (m_isPaused)
            {
                return;
            }

            m_stateMachine.CurrentState.LateUpdate();
        }

        private void OnDamaged()
        {
            Animator.OnPlayerDamaged();
        }

        private void OnDeath()
        {
            PlayerDamager.gameObject.SetActive(false);
            PlayerLife.gameObject.SetActive(false);
            m_stateMachine.ChangeState(DeathState);
        }
    }
}
