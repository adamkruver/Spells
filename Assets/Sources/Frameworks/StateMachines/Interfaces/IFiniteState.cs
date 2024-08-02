using Sources.Frameworks.LifeCycles;
using Sources.Frameworks.StateMachines.Common;

namespace Sources.Frameworks.StateMachines.Interfaces
{
    public interface IFiniteState : IState, ITransitionOwner, IUpdatable
    {
        bool CanTransit(out IFiniteState state);
    }
}