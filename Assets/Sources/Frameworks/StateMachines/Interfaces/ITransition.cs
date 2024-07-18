using Frameworks.StateMachines;

namespace Sources.Frameworks.StateMachines
{
    public interface ITransition<T> where T : IState
    {
        bool CanTransit { get; }
        T NextState { get; }
    }
}