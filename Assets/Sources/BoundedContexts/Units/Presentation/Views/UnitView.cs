using Server.Combat.Domain.Skills;

using Sources.Frameworks.Mvp;

namespace Sources.BoundedContexts.Units.Presentation.Views
{
    public class UnitView : View<Assets.Sources.BoundedContexts.Units.Infrastructure.Contollers.UnitPresenter>, IUnitView
    {
        private void Update()
        {
            if (Presenter == null)
            {
                enabled = false;
                return;
            }

            Presenter.Update(UnityEngine.Time.deltaTime);
        }

        public void UseSkill(ISkill skill) => Presenter.UseSkill(skill);
    }

    public interface IUnitView
    {
        void UseSkill(ISkill skill);
    }
}