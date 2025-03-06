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
            m_player.Motor.OnDamageTaken(m_player.PlayerLife.DamagerPosition);
            //m_player.gameObject.layer = 14;
        }
        public override void OnExit()
        {
            //m_player.gameObject.layer = 13;
        }
        public override void Update()
        {
            m_player.Motor.CheckForGround();
            

            if (m_player.PlayerLife.IsDead)
            {
                m_stateMachine.ChangeState(m_player.DeathState);
            }
            else if (Time.time - m_enterTime >= m_stunTime)
            {
                if (m_player.Motor.IsGrounded)
                {
                    m_stateMachine.ChangeState(m_player.IdleState);
                }
                else
                {
                    m_stateMachine.ChangeState(m_player.FallingState);
                }
            }
        }
    }
}
