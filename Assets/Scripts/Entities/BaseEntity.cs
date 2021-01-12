using System;
using UnityEngine;

namespace Entities
{
    public class BaseEntity : MonoBehaviour 
    {
        protected StateMachine StateMachine;

        protected void AddTransition(IState to, IState from, Func<bool> condition) => StateMachine.AddTransition(to, from, condition);
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