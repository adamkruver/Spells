using Sources.BoundedContexts.Skills.Domain;
using Sources.BoundedContexts.Units.Domain;
using Sources.BoundedContexts.Units.StateMachines;
using Sources.Frameworks.StateMachines.Interfaces;

namespace Sources.BoundedContexts.Units.States
{
    public class ActionBuffer
    {
        public ISkillStrategyFactory NextSkill { get; set; }
    }

    public class Idle : IUpdatableState
    {
        public void Enter() { }

        public void Exit() { }

        public void Update(float deltaTime) { }
    }

    public class Dead : IUpdatableState
    {
        public void Enter() { }
        public void Exit() { }
        public void Update(float deltaTime) { }
    }

    public class Casting : IUpdatableState
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

    public class IdleToDeadTransition : ITransition<IUpdatableState>
    {
        private readonly IKillable _model;

        public IdleToDeadTransition(Dead nextState, IKillable model)
        {
            NextState = nextState;
            _model = model;
        }

        public bool CanTransit => _model.Alive == false;

        public IUpdatableState NextState { get; }
    }

    public class DeadToIdleTransition : ITransition<IUpdatableState>
    {
        private readonly IKillable _model;

        public DeadToIdleTransition(IUpdatableState nextState, IKillable model)
        {
            _model = model;
            NextState = nextState;
        }

        public bool CanTransit => _model.Alive;

        public IUpdatableState NextState { get; }
    }

    public class IdleToCastingTransition : ITransition<IUpdatableState>
    {
        private readonly ActionBuffer _actionBuffer;

        public IdleToCastingTransition(IUpdatableState nextState, ActionBuffer actionBuffer)
        {
            _actionBuffer = actionBuffer;
            NextState = nextState;
        }

        public bool CanTransit => _actionBuffer.NextSkill != null;

        public IUpdatableState NextState { get; }
    }

    public class CastingToIdleTransition : ITransition<IUpdatableState>
    {
        private readonly ICaster _model;

        public CastingToIdleTransition(IUpdatableState nextState, ICaster model)
        {
            _model = model;
            NextState = nextState;
        }

        public bool CanTransit => _model.ActiveCast.InProgress == false;

        public IUpdatableState NextState { get; }
    }
}