namespace Sources.Frameworks.StateMachines
{
    public interface ITransitionOwner
    {
        void AddTransition(ITransition transition);
    }
}