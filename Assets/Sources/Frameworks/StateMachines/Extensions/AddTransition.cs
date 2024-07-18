using System;

namespace Sources.Frameworks.StateMachines.Extensions
{
    public static  class StateExtensions
    {
        public static IFiniteStateBuilder AddTransition(this IFiniteStateBuilder builder, Func<bool> condition)
        {
            builder.AddTransition(builder.State, condition);

            return builder;
        }
    }
}