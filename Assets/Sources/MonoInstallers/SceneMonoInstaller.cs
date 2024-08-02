using Assets.Sources.BoundedContexts.Input;
using Assets.Sources.BoundedContexts.Players.Infrastucture.Factories;
using Assets.Sources.BoundedContexts.Units.Infrastructure.Contollers;

using Server.Combat.Domain.Units.StateMachine;

using Sources.BoundedContexts.Players.Controllers;
using Sources.BoundedContexts.Units.Domain;
using Sources.BoundedContexts.Units.Infrastructure.Factories;
using Sources.BoundedContexts.Units.Presentation.Views;
using Sources.Extensions.IServiceCollections;

using UniCtor.Installers;
using UniCtor.Services;

using UnityEngine;

using Utils.Patterns.Factory;

public class SceneMonoInstaller : MonoInstaller
{
    [SerializeField] private UnitView _viewPrefab;

    public override void OnConfigure(IServiceCollection services)
    {
        services.RegisterAsSingleton<Factory<PlayerController, PlayerControllerFactory.PlayerControllerCreationData>>(serviceProver
            => new PlayerControllerFactory(serviceProver.GetService<UnitViewFactory>()));
        services.RegisterAsSingleton<Factory<Unit>, UnitFactory>();
        services.RegisterAsSingleton<IInputService, InputService>();

        services.RegisterAsSingleton<Factory<UnitPresenter, (IUnitView view, Unit model)>, UnitPresenterFactory>();
        services.RegisterAsSingleton<Factory<UnitView, Unit>>(new UnitViewProvider(_viewPrefab));
        services.RegisterAsSingleton<UnitViewFactory>();
    }

    private class UnitFactory : Factory<Unit>
    {
        public Unit Create() => new Unit();
    }

    private class UnitPresenterFactory : Factory<UnitPresenter, (IUnitView view, Unit model)>
    {
        public UnitPresenter Create((IUnitView view, Unit model) data) => new(data.model, data.view);
    }

    private class UnitViewProvider : Factory<UnitView, Unit>
    {
        private readonly UnitView _commonUnitViewPrefab;

        public UnitViewProvider(UnitView commonUnitViewPrefab)
        {
            _commonUnitViewPrefab = commonUnitViewPrefab;
        }

        public UnitView Create(Unit data) => UnityEngine.Object.Instantiate(_commonUnitViewPrefab);
    }
}
