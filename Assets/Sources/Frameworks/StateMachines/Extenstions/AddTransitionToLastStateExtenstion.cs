using System;

using Sources.Frameworks.StateMachines;

namespace Frameworks.StateMachines.Extenstions
{
    public static class AddTransitionToLastStateExtenstion
    {
        public static IFiniteStateMachineBuilder<FiniteStateMachine> AddTransitionToLast<T>(this IFiniteStateMachineBuilder<FiniteStateMachine> stateMachineBuilder, Func<bool> condition) where T : class, IFiniteState
        {
            stateMachineBuilder.AddTransition<T>(stateMachineBuilder.LastRegisteredState, condition);
            return stateMachineBuilder;
        }
    }
}
