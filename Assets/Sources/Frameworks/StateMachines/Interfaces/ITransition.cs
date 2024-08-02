using Sources.Frameworks.StateMachines.Common;

namespace Sources.Frameworks.StateMachines.Interfaces
{
    public interface ITransition<T> where T : IState
    {
        bool CanTransit { get; }
        T NextState { get; }
    }
}