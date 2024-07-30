﻿using Frameworks.StateMachines;

using Sources.Frameworks.LifeCycles;

namespace Sources.Frameworks.StateMachines
{
    public interface IFiniteState : IState, ITransitionOwner, IUpdatable
    {
        bool CanTransit(out IFiniteState state);
    }
}