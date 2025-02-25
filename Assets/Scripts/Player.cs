using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace PetesPlatformer
{
    public class Player : MonoBehaviour
    {
        StateMachine m_stateMachine;

        [field: SerializeField] public PlayerMotor Motor { get; private set; }
        [field: SerializeField] public PlayerAnimator Animator { get; private set; }
        [field: SerializeField] public PlayerInput Input { get; private set; }

        public IdleState IdleState { get; private set; }
        public MoveState MoveState { get; private set; }
        public JumpState JumpState { get; private set; }
        public FallingState FallingState { get; private set; }
        public WallSlideState WallSlideState { get; private set; }

        private void Awake()
        {
            m_stateMachine = new StateMachine();

            IdleState = new IdleState(this, m_stateMachine);
            MoveState = new MoveState(this, m_stateMachine);
            JumpState = new JumpState(this, m_stateMachine);
            FallingState = new FallingState(this, m_stateMachine);
            WallSlideState = new WallSlideState(this, m_stateMachine);

            m_stateMachine.Initialize(IdleState);
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
    }
}
