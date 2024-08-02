namespace Sources.Frameworks.StateMachines.Common
{
    public interface IStateChanger<T> where T : IState
    {
        void Change(T state);
    }
}