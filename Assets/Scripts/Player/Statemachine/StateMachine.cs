using System;
using System.Collections.Generic;
using UnityEngine;

namespace PetesPlatformer
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        public void Initialize(State state)
        {
            CurrentState = state;
            CurrentState.OnEnter();
        }

        public void ChangeState(State newState)
        {
            CurrentState.OnExit();
            CurrentState = newState;
            CurrentState.OnEnter();
        }
    }
}
