using Server.Combat.Domain.Common;

using Utils.Patterns.StateMachine.FinitStateMachine;

namespace Server.Combat.Domain.Units.StateMachine
{
    //TODO: Unit <-??-> UnitStateMachine
    public interface IUnitStateMachine : IFinitStateMachine<IUnitState>, IUpdatable
    {
    }
}