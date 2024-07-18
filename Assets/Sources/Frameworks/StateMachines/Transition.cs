using System;

namespace Sources.Frameworks.StateMachines
{
    public class Transition : ITransition
    {
        private readonly Func<bool> _condition;

        public Transition(IFiniteState nextState, Func<bool> condition)
        {
            NextState = nextState ?? throw new ArgumentNullException(nameof(nextState));
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }

        public bool CanTransit => _condition.Invoke();
        public IFiniteState NextState { get; }
    }
}