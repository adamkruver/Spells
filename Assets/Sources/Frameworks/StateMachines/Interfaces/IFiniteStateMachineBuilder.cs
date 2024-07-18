using System;

namespace Sources.Frameworks.StateMachines
{
    public interface IFiniteStateMachineBuilder
    {
        IFiniteStateMachineBuilder RegisterState(IFiniteState state);
        IFiniteStateMachineBuilder SetFirstState<T>() where T : class, IFiniteState;
        IFiniteStateMachineBuilder AddTransition<TFrom, TTarget>(Func<bool> condition) where TFrom : class, IFiniteState where TTarget : class, IFiniteState;

        FiniteStateMachine Build();
        void Clear();
    }
}