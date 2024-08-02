namespace Sources.Frameworks.StateMachines.Interfaces
{
    public interface ITransitionOwner
    {
        void AddTransition(ITransition<IFiniteState> transition);
    }
}