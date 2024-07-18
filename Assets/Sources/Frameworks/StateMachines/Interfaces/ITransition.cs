namespace Sources.Frameworks.StateMachines
{
    public interface ITransition
    {
        bool CanTransit { get; }
        IFiniteState NextState { get; }
    }
}