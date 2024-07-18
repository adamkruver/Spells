namespace Frameworks.StateMachines
{
    public interface IStateMachine<T> where T : IState
    {
        T CurrentState { get; }
    }

    public interface IStateChanger<T> where T : IState
    {
        void Change(T state);
    }
}
