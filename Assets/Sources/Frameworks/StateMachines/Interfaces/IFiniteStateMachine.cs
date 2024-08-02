using Sources.Frameworks.StateMachines.Common;

namespace Sources.Frameworks.StateMachines.Interfaces
{
    public interface IFiniteStateMachine : IStateMachine<IFiniteState>
    {
        void Start(IFiniteState finiteState);
        void Stop();
    }
}
