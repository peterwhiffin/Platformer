using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace PetesPlatformer
{
    public class Player : MonoBehaviour
    {
        private StateMachine m_stateMachine;

        [field: SerializeField] public Life Life { get; private set; }
        [field: SerializeField] public PlayerMotor Motor { get; private set; }
        [field: SerializeField] public PlayerAnimator Animator { get; private set; }
        [field: SerializeField] public PlayerInput Input { get; private set; }

        public IdleState IdleState { get; private set; }
        public MoveState MoveState { get; private set; }
        public JumpState JumpState { get; private set; }
        public FallingState FallingState { get; private set; }
        public WallSlideState WallSlideState { get; private set; }
        public WallJumpState WallJumpState { get; private set; }
        public DeathState DeathState { get; private set; }

        private void Awake()
        {
            m_stateMachine = new StateMachine();

            IdleState = new IdleState(this, m_stateMachine);
            MoveState = new MoveState(this, m_stateMachine);
            JumpState = new JumpState(this, m_stateMachine);
            FallingState = new FallingState(this, m_stateMachine);
            WallSlideState = new WallSlideState(this, m_stateMachine);
            WallJumpState = new WallJumpState(this, m_stateMachine);
            DeathState = new DeathState(this, m_stateMachine);

            m_stateMachine.Initialize(IdleState);
        }

        private void Start()
        {
            Life.Died += OnDeath;
        }

        private void OnDestroy()
        {
            Life.Died -= OnDeath;
        }

        private void Update()
        {
            m_stateMachine.CurrentState.Update();
        }

        private void FixedUpdate()
        {
            m_stateMachine.CurrentState.FixedUpdate();
        }

        private void LateUpdate()
        {
            m_stateMachine.CurrentState.LateUpdate();
        }

        private void OnDeath()
        {
            m_stateMachine.ChangeState(DeathState);
        }
    }
}
