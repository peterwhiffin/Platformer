using UnityEngine;

namespace PetesPlatformer
{
    public class PlayerHitState : PlayerState
    {
        private float m_enterTime;
        private float m_stunTime = .2f; 

        public PlayerHitState(StateMachine stateMachine, Player player) : base(stateMachine, player)
        {
        }

        public override void FixedUpdate()
        {
            m_player.Motor.ApplyGravity();
        }
        public override void LateUpdate()
        {
        }
        public override void OnEnter()
        {
            m_enterTime = Time.time;
            m_player.Animator.OnPlayerDamaged();
            m_player.Motor.OnDamageTaken();
            m_player.gameObject.layer = 14;
        }
        public override void OnExit()
        {
            m_player.gameObject.layer = 13;
        }
        public override void Update()
        {
            m_player.Motor.CheckForGround();

            if (Time.time - m_enterTime >= m_stunTime)
            {
                m_stateMachine.ChangeState(m_player.FallingState);
            }
        }
    }

    public abstract class PlayerState : State
    {
        protected readonly Player m_player;

        public PlayerState(StateMachine stateMachine, Player player) : base(stateMachine)
        {
            m_player = player;
            m_player.PlayerLife.DamageTaken += OnHit;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void Update()
        {
            base.Update();
        }

        private void OnHit(Vector3 position)
        {
            m_stateMachine.ChangeState(m_player.HitState);
           
        }
    }
}
