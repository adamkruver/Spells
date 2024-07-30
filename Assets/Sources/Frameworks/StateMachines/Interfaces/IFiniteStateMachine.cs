using Sources.Frameworks.StateMachines;

namespace Frameworks.StateMachines.Interfaces
{
    public interface IFiniteStateMachine : IStateMachine<IFiniteState>
    {
        void Start(IFiniteState finiteState);
        void Stop();
    }
}
