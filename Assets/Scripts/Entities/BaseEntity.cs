using System;
using UnityEngine;

namespace Entities
{
    public class BaseEntity : MonoBehaviour 
    {
        protected StateMachine StateMachine;

        protected void AddTransition(IState from, IState to, Func<bool> condition) => StateMachine.AddTransition(from, to, condition);
        protected void AddAnyTransition(IState state, Func<bool> condition) => StateMachine.AddAnyTransition(state, condition);

        protected virtual void Awake()
        {
            StateMachine = new StateMachine();
        }

        protected virtual void Update()
        {
            StateMachine.Tick();
        }
    }
}