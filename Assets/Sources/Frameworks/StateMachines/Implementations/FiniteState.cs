using System.Collections.Generic;
using System.Linq;

namespace Sources.Frameworks.StateMachines
{
    public abstract class FiniteState : IFiniteState, ITransitionOwner
    {
        private readonly List<ITransition> _transitions;

        protected FiniteState()
        {
            _transitions = new();
        }

        protected FiniteState(IEnumerable<ITransition> transitions)
        {
            _transitions = transitions.ToList();
        }

        bool IFiniteState.CanTransit(out IFiniteState state)
        {
            state = _transitions
                .FirstOrDefault(transition => transition.CanTransit)?
                .NextState;

            return state != null;
        }

        public void AddTransition(ITransition transition)
        {
            if (_transitions.Contains(transition))
            {
                return;
            }

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