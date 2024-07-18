using System.Collections.Generic;

using Sources.Frameworks.StateMachines;

namespace Sources.BoundedContexts.Units.States
{
    public class UnitIdleState : FiniteState
    {
        private readonly object _model;

        public UnitIdleState()
        {
        }

        public UnitIdleState(IEnumerable<ITransition> transitions) : base(transitions)
        {
            
        }
    }
}