namespace Sources.Frameworks.StateMachines
{
    public interface IFiniteStateChanger
    {
        void Change(IFiniteState state);
    }
}