using Server.Combat.Domain.Skills;
using Server.Combat.Domain.Units.StateMachine;

using Sources.BoundedContexts.Units.Domain;

using Utils.Patterns.Factory;

namespace Client.Combat.Infrastructure.Unit.States
{
    public class ActionBuffer
    {
        public Factory<ISkillHandler, ISkill> NextSkill { get; set; }
    }

    public class Idle : IUnitState
    {
        public bool CanCast(ISkill spell, SkillModification spellModification) => true;

        public void Enter() { }

        public void Exit() { }

        public void Update(float deltaTime) { }
    }

    public class Dead : IUnitState
    {
        public bool CanCast(ISkill spell, SkillModification spellModification) => false;

        public void Enter() { }
        public void Exit() { }
        public void Update(float deltaTime) { }
    }

    public class Casting : IUnitState
    {
        private readonly ICaster _model;
        private readonly ActionBuffer _actionBuffer;

        public Casting(ICaster caster, ActionBuffer actionBuffer)
        {
            _model = caster;
            _actionBuffer = actionBuffer;
        }

        public bool CanCast(ISkill spell, SkillModification spellModification) => false;

        public void Enter()
        {
            //_caster.ActiveCast = _actionBuffer.NextSkill.Create(null);
            //_actionBuffer.NextSkill = null;
        }

        public void Exit() => _model.ActiveCast = null;

        public void Update(float deltaTime)
        {
            if(_model.ActiveCast == null)
            {
                return;
            }

            _model.ActiveCast.Update(deltaTime);
        }

        public bool FinishedCast() => _model.ActiveCast == null || !_model.ActiveCast.IsActive;
    }
}