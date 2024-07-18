using System;

using Frameworks.StateMachines.Interfaces;

namespace Sources.Frameworks.StateMachines
{
    public interface IFiniteStateMachineBuilder<out TMachine> where TMachine : IFiniteStateMachine
    {
        Type LastRegisteredState { get; }

        IFiniteStateMachineBuilder<TMachine> RegisterState(IFiniteState state);
        IFiniteStateMachineBuilder<TMachine> AddTransition<T>(Type from, Func<bool> condition) where T : class, IFiniteState;
        IFiniteStateMachineBuilder<TMachine> AddTransition<TFrom, TTarget>(Func<bool> condition) where TFrom : class, IFiniteState where TTarget : class, IFiniteState;
        IFiniteStateMachineBuilder<TMachine> SetFirstState<T>() where T : class, IFiniteState;

        TMachine Build();
        void Clear();
    }
}