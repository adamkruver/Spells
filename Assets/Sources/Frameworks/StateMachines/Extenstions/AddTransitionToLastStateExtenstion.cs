using System;

using Sources.Frameworks.StateMachines;

namespace Frameworks.StateMachines.Extenstions
{
    public static class AddTransitionToLastStateExtenstion
    {
        public static FiniteStateMachineBuilder AddTransitionToLast<T>(this FiniteStateMachineBuilder stateMachineBuilder, Func<bool> condition) where T : class, IFiniteState
        {
            stateMachineBuilder.AddTransition<T>(stateMachineBuilder.LastAddedState, condition);
            return stateMachineBuilder;
        }
    }
}
