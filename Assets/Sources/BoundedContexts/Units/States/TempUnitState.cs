using Sources.BoundedContexts.Units.Domain;
using Sources.BoundedContexts.Units.StateMachines;
using Sources.Frameworks.StateMachines;

namespace Client.Combat.Infrastructure.Unit.States
{
    public class ActionBuffer
    {
        public ISkillStrategyFactory NextSkill { get; set; }
    }

    public class Idle : IUnitState
    {
        public void Enter() { }

        public void Exit() { }

        public void Update(float deltaTime) { }
    }

    public class Dead : IUnitState
    {
        public void Enter() { }
        public void Exit() { }
        public void Update(float deltaTime) { }
    }

    public class Casting : IUnitState
    {
        private readonly ICaster _caster;
        private readonly ActionBuffer _actionBuffer;

        public Casting(ICaster caster, ActionBuffer actionBuffer)
        {
            _caster = caster;
            _actionBuffer = actionBuffer;
        }

        public void Enter()
        {
            _caster.ActiveCast = _actionBuffer.NextSkill.Create(null);
            _actionBuffer.NextSkill = null;
        }

        public void Exit() => _caster.ActiveCast = null;

        public void Update(float deltaTime)
        {
            _caster.ActiveCast.Execute(deltaTime);
        }
    }

    public class IdleToDeadTransition : ITransition<IUnitState>
    {
        private readonly IKillable _model;

        public IdleToDeadTransition(Dead nextState, IKillable model)
        {
            NextState = nextState;
            _model = model;
        }

        public bool CanTransit => _model.Alive == false;

        public IUnitState NextState { get; }
    }

    public class DeadToIdleTransition : ITransition<IUnitState>
    {
        private readonly IKillable _model;

        public DeadToIdleTransition(IUnitState nextState, IKillable model)
        {
            _model = model;
            NextState = nextState;
        }

        public bool CanTransit => _model.Alive;

        public IUnitState NextState { get; }
    }

    public class IdleToCastingTransition : ITransition<IUnitState>
    {
        private readonly ActionBuffer _actionBuffer;

        public IdleToCastingTransition(IUnitState nextState, ActionBuffer actionBuffer)
        {
            _actionBuffer = actionBuffer;
            NextState = nextState;
        }

        public bool CanTransit => _actionBuffer.NextSkill != null;

        public IUnitState NextState { get; }
    }

    public class CastingToIdleTransition : ITransition<IUnitState>
    {
        private readonly ICaster _model;

        public CastingToIdleTransition(IUnitState nextState, ICaster model)
        {
            _model = model;
            NextState = nextState;
        }

        public bool CanTransit => _model.ActiveCast.InProgress == false;

        public IUnitState NextState { get; }
    }
}