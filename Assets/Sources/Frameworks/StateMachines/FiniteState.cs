using System.Collections.Generic;
using System.Linq;

namespace Sources.Frameworks.StateMachines
{
    public abstract class FiniteState : IFiniteState
    {
        private readonly List<ITransition> _transitions = new List<ITransition>();

        bool IFiniteState.CanTransit(out IFiniteState state)
        {
            state = _transitions
                .FirstOrDefault(transition => transition.CanTransit)?
                .NextState;

            return state != null;
        }

        public void Add(ITransition transition)
        {
            if (_transitions.Contains(transition))
                return;

            _transitions.Add(transition);
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update(float deltaTime)
        {
        }
    }
}