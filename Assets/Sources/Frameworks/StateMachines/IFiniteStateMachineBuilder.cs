using System;

namespace Sources.Frameworks.StateMachines
{
    public interface IFiniteStateMachineBuilder
    {
        IFiniteStateBuilder AddState<T>(T state) where T : IFiniteState;
        IFiniteStateMachineBuilder SetFirstState<T>();
        void AddTransition(IFiniteState state, Func<bool> condition);

        FiniteStateMachine Build();
    }
}