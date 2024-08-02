using Server.Combat.Domain.Skills;
using Server.Combat.Domain.Units.Components;
using Server.Combat.Domain.Units.StateMachine;

using Sources.BoundedContexts.Units.Domain;
using Sources.BoundedContexts.Units.Presentation.Views;
using Sources.Frameworks.Mvp;

using Utils.Patterns.Factory;

namespace Assets.Sources.BoundedContexts.Units.Infrastructure.Contollers
{
    public class UnitPresenter : IPresenter
    {
        private readonly Factory<ISkillStrategy, (ISkill, SkillModification)> _skillStrategyFactory;

        private readonly Unit _model;
        private readonly ISpellOwner _spellOwner;
        private readonly IUnitStateMachine _unitStateMachine;

        public UnitPresenter(Unit model, IUnitView unitView)
        {
            _model = model;

        }

        public void UseSkill(ISkill skill)
        {
            SkillModification skillModification = _spellOwner.GetSpellModification(skill);

            if (_unitStateMachine.CurrentState.CanCast(skill, skillModification) == false)
            {
                return;
            }

            _model.ActiveCast = _skillStrategyFactory.Create((skill, skillModification));
        }
    }
}
