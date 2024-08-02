namespace Sources.Frameworks.StateMachines.Common
{
    public interface IStateMachine<out T> where T : IState
    {
        T CurrentState { get; }
    }
}
