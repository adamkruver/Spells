using Sources.Frameworks.LifeCycles;

namespace Sources.Frameworks.StateMachines
{
    public interface IFiniteState : IUpdatable
    {
        void Add(ITransition transition);
        void Enter();
        void Exit();
        bool CanTransit(out IFiniteState state);
    }
}