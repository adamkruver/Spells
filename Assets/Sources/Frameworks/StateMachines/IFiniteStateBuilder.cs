namespace Sources.Frameworks.StateMachines
{
    public interface IFiniteStateBuilder :IFiniteStateMachineBuilder
    {
        public IFiniteState State { get; set; }

    }
}