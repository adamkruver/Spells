using System;
using Sources.Frameworks.StateMachines.Implementations;
using Sources.Frameworks.StateMachines.Interfaces;

namespace Sources.Frameworks.StateMachines.Extensions
{
    public static class AddTransitionToLastStateExtension
    {
        public static FiniteStateMachineBuilder AddTransitionToLast<T>(
            this FiniteStateMachineBuilder stateMachineBuilder,
            Func<bool> condition
        ) where T : class, IFiniteState =>
            stateMachineBuilder.AddTransition<T>(stateMachineBuilder.LastAddedState, condition);
    }
}