using System;

using Server.Combat.Domain.Common;
using Server.Combat.Domain.Skills;
using Server.Combat.Domain.Units.Components;
using Server.Combat.Domain.Units.StateMachine;

using Sources.BoundedContexts.Units.Domain;
using Sources.BoundedContexts.Units.Presentation.Views;
using Sources.Frameworks.Mvp;

using Utils.Patterns.Factory;

namespace Assets.Sources.BoundedContexts.Units.Infrastructure.Contollers
{
    public interface ISkillStrategyFactory<T> : Factory<ISkillHandler, (ISkill, SkillModification)> where T : IHitboxProvider
    { }

    public class UnitPresenter : IPresenter, IUpdatable
    {
        private readonly Factory<ISkillHandler, (ISkill, SkillModification)> _skillStrategyFactory;

        private readonly Unit _model;
        private readonly ISpellOwner _spellOwner;
        private readonly IUnitStateMachine _unitStateMachine;

        public UnitPresenter(Unit model, IUnitView unitView, Factory<ISkillHandler, (ISkill, SkillModification)> skillStrategyFactory, IUnitStateMachine unitStateMachine)
        {
            _model = model;
            _skillStrategyFactory = skillStrategyFactory;
            _unitStateMachine = unitStateMachine;
        }

        public void Update(float deltaTime) => _unitStateMachine.Update(deltaTime);

        public void UseSkill(ISkill skill)
        {
            SkillModification skillModification = _spellOwner?.GetSpellModification(skill);

            if (_unitStateMachine.CurrentState.CanCast(skill, skillModification) == false)
            {
                return;
            }

            _model.ActiveCast = _skillStrategyFactory.Create((skill, skillModification));
        }
    }

    public interface IHitbox
    {
        event Action<object> Hitted;
    }

    public interface IHitboxProvider
    {
        IHitbox GetHitbox(string type);
    }
}
