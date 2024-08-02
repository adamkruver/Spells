using System.Collections.Generic;
using System.Numerics;

using Server.Combat.Domain.Common;
using Server.Combat.Domain.Skills;

using Sources.BoundedContexts.Units.Domain;
using Sources.BoundedContexts.Units.Presentation.Presenters;
using Sources.Frameworks.Mvp;

namespace Sources.BoundedContexts.Units.Presentation.Views
{
    public class UnitView : View<Assets.Sources.BoundedContexts.Units.Infrastructure.Contollers.UnitPresenter>, IUnitView
    {
        public void UseSkill(ISkill skill) => Presenter.UseSkill(skill);

        //public void UseSkill(ISkillStrategyFactory attackSkill) => 
        //    Presenter.UseSkill(attackSkill);

        //public void Tick(float deltaTime) =>
        //    Presenter.Tick(deltaTime);

        void IUpdatable.Update(float deltaTime) => throw new System.NotImplementedException();
    }

    public interface IUnitView : IUpdatable
    {
        void UseSkill(ISkill skill);
    }
}