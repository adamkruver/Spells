using System;
using Sources.BoundedContexts.Skills.Domain;
using Sources.BoundedContexts.Units.Domain;
using Sources.Frameworks.Mvp;
using Sources.Frameworks.StateMachines;
using Sources.Frameworks.StateMachines.Implementations;

namespace Sources.BoundedContexts.Units.Presentation.Presenters
{
    public class UnitPresenter : IPresenter
    {
        private readonly IUnitView _view;
        private readonly FiniteStateMachine _stateMachine;

        private ISkillStrategyFactory _attackSkill;

        public UnitPresenter(IUnitView view, FiniteStateMachine stateMachine)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        }

        public void UseSkill(ISkillStrategyFactory attackSkill) =>
            _attackSkill = attackSkill;

        public void Tick(float deltaTime)
        {
            _stateMachine.Update(deltaTime);
        }
    }
}