using Assets.Sources.BoundedContexts.Units.Infrastructure.Contollers;

using Sources.BoundedContexts.Units.Domain;
using Sources.BoundedContexts.Units.Presentation.Views;

using Utils.Patterns.Factory;

namespace Sources.BoundedContexts.Units.Infrastructure.Factories
{
    public class UnitViewFactory
    {
        private readonly Factory<UnitPresenter, (IUnitView view, Unit model)> _presenterFactory;
        private readonly Factory<UnitView, Unit> _viewPrefabProvider;

        public UnitViewFactory(Factory<UnitPresenter, (IUnitView view, Unit model)> presenterFactory,  Factory<UnitView, Unit> viewPrefabProvider)
        {
            _presenterFactory = presenterFactory;
            _viewPrefabProvider = viewPrefabProvider;
        }

        public IUnitView Create(UnitViewCreationData creationData)
        {
            var view = _viewPrefabProvider.Create(creationData.Model);
            var presenter = _presenterFactory.Create((view, creationData.Model));
            view.Construct(presenter);

            return view;
        }

        public readonly struct UnitViewCreationData
        {
            public readonly Unit Model;

            public UnitViewCreationData(Unit model)
            {
                Model = model;
            }
        }
    }
}