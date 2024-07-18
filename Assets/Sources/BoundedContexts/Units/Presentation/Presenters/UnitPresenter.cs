using System;
using Sources.BoundedContexts.Units.Domain;
using Sources.Frameworks.Mvp;
using Sources.Frameworks.StateMachines;

namespace Sources.BoundedContexts.Units.Presentation.Presenters
{
    public class UnitPresenter : IPresenter
    {
        private readonly IUnitView _view;
        private readonly FiniteStateMachine _stateMachine;

        private ISkillStrategyFactory _attackSkill;
        private ISkillStrategy _skillStrategy;

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
            // UpdateSkillStrategy();
            // _skillStrategy?.Execute(deltaTime);
        }

        private void UpdateSkillStrategy()
        {
            if (_skillStrategy?.InProgress == false)
            {
                _skillStrategy = _attackSkill?.Create(_view);
                _attackSkill = null;
            }
        }
    }
}