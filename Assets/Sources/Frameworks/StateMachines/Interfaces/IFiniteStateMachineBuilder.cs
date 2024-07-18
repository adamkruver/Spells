using System;

using Frameworks.StateMachines.Interfaces;

namespace Sources.Frameworks.StateMachines
{
    public interface IFiniteStateMachineBuilder<out TMachine> where TMachine : IFiniteStateMachine
    {
        IFiniteStateMachineBuilder<TMachine> RegisterState(IFiniteState state);
        IFiniteStateMachineBuilder<TMachine> SetFirstState<T>() where T : class, IFiniteState;
        IFiniteStateMachineBuilder<TMachine> AddTransition<TFrom, TTarget>(Func<bool> condition) where TFrom : class, IFiniteState where TTarget : class, IFiniteState;

        TMachine Build();
        void Clear();
    }
}