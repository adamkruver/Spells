﻿using System;
using Sources.Frameworks.StateMachines.Interfaces;

namespace Sources.Frameworks.StateMachines.Implementations
{
    public class Transition : ITransition<IFiniteState>
    {
        private readonly Func<bool> _condition;

        public Transition(IFiniteState nextState, Func<bool> condition)
        {
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));

            NextState = nextState ?? throw new ArgumentNullException(nameof(nextState));
        }

        public bool CanTransit => _condition.Invoke();
        public IFiniteState NextState { get; }
    }
}