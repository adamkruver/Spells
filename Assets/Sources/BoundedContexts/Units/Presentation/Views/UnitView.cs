using System.Collections.Generic;
using Sources.BoundedContexts.Units.Domain;
using Sources.BoundedContexts.Units.Presentation.Presenters;
using Sources.Frameworks.Mvp;

namespace Sources.BoundedContexts.Units.Presentation.Views
{
    public class UnitView : View<UnitPresenter>, IUnitView
    {
        public void UseSkill(ISkillStrategyFactory attackSkill) => 
            Presenter.UseSkill(attackSkill);

        public void Tick(float deltaTime) =>
            Presenter.Tick(deltaTime);

        IEnumerable<IDamageable> ITargetLocator.FindTargets() => 
            throw new System.NotImplementedException();
    }
}