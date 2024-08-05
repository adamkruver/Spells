using Assets.Sources.BoundedContexts.Input;
using Assets.Sources.BoundedContexts.Players.Infrastucture.Factories;
using Assets.Sources.BoundedContexts.Units.Infrastructure.Contollers;

using Client.Combat.Infrastructure.Unit.States;

using Server.Combat.Domain.Implementations.Units.Components;
using Server.Combat.Domain.Skills;
using Server.Combat.Domain.Units.StateMachine;

using Sources.BoundedContexts.Players.Controllers;
using Sources.BoundedContexts.Units.Domain;
using Sources.BoundedContexts.Units.Infrastructure.Factories;
using Sources.BoundedContexts.Units.Presentation.Views;

using UniCtor.Installers;
using UniCtor.Services;

using UnityEngine;

using Utils.Patterns.Factory;
using Utils.Patterns.StateMachine.FinitStateMachine;
using Utils.Patterns.StateMachine.Implementations;

public class SceneMonoInstaller : MonoInstaller
{
    [SerializeField] private UnitView _viewPrefab;

    public override void OnConfigure(IServiceCollection services)
    {
        services.RegisterAsSingleton<Factory<UnitView, Unit>>(new UnitViewProvider(_viewPrefab));
        services.RegisterAsSingleton<Factory<PlayerController, PlayerControllerFactory.PlayerControllerCreationData>, PlayerControllerFactory>();
        services.RegisterAsSingleton<Factory<Unit>, UnitFactory>();
        services.RegisterAsSingleton<IInputService, InputService>();

        services.RegisterAsSingleton<Factory<ISkillHandler, (ISkill, SkillModification)>, SkillHandlerFactory>();
        services.RegisterAsSingleton<Factory<UnitPresenter, (IUnitView view, Unit model)>, UnitPresenterFactory>();
        services.RegisterAsSingleton<UnitViewFactory>();
        services.RegisterAsSingleton<Factory<IUnitStateMachine, (IUnitView view, Unit model)>, UnitStateMachineFactory>();
    }

    private class UnitFactory : Factory<Unit>
    {
        public Unit Create() => new Unit();
    }

    private class UnitPresenterFactory : Factory<UnitPresenter, (IUnitView view, Unit model)>
    {
        private readonly Factory<ISkillHandler, (ISkill, SkillModification)> _skillLibrary;
        private readonly Factory<IUnitStateMachine, (IUnitView view, Unit model)> _stateMachineFactory;

        public UnitPresenterFactory(Factory<ISkillHandler, (ISkill, SkillModification)> skillLibrary, Factory<IUnitStateMachine, (IUnitView view, Unit model)> unitStateMachineFactory)
        {
            _skillLibrary = skillLibrary;
            _stateMachineFactory = unitStateMachineFactory;
        }

        public UnitPresenter Create((IUnitView view, Unit model) data) => new(data.model, data.view, _skillLibrary, _stateMachineFactory.Create(data));
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

    private class SkillHandlerFactory : Factory<ISkillHandler, (ISkill, SkillModification)>
    {
        private class SaySalvationTestSkillHandler : ISkillHandler
        {
            private readonly SkillModification _skillModification;

            public SaySalvationTestSkillHandler(ISkill skill, SkillModification skillModification)
            {
                _skillModification = skillModification;
                Skill = skill;
                ActiveTime = 0;
            }

            public ISkill Skill { get; }

            public bool IsActive => ActiveTime < 2;

            public float ActiveTime { get; private set; }

            public void Update(float deltaTime)
            {
                ActiveTime += deltaTime;

                if (ActiveTime > 1)
                {
                    UnityEngine.Debug.Log("Salvation!");
                }
            }
        }

        public ISkillHandler Create((ISkill, SkillModification) data) => new SaySalvationTestSkillHandler(data.Item1, data.Item2);
    }

    private class UnitStateMachineFactory : Factory<IUnitStateMachine, (IUnitView view, Unit model)>
    {
        public IUnitStateMachine Create((IUnitView view, Unit model) data)
        {
            Idle idle = new();
            Dead dead = new();
            Casting casting = new(data.model, null);
            DeadTransition toDeadTransition = new(data.model, dead);

            ITransition<IUnitState>[] fromIdleTransitions =
            {
                toDeadTransition,
                new FuncTransition<IUnitState>(() =>  data.model.ActiveCast != null &&  data.model.ActiveCast.IsActive, casting),
            };

            ITransition<IUnitState>[] fromDeadTransitions =
            {
                new FuncTransition<IUnitState>(() => data.model.Alive, idle),
            };

            ITransition<IUnitState>[] fromCastingTransitions =
            {
                new FuncTransition<IUnitState>(casting.FinishedCast, idle),
                toDeadTransition,
            };

            SeparatedTransitionsStateCollection<IUnitState> stateCollection = new();

            stateCollection.SetTransitions(idle, fromIdleTransitions);
            stateCollection.SetTransitions(dead, fromDeadTransitions);
            stateCollection.SetTransitions(casting, fromCastingTransitions);

            UnitStateMachine unitStateMachine = new(stateCollection, idle);

            unitStateMachine.Run();

            return unitStateMachine;
        }

        private class DeadTransition : ITransition<IUnitState>
        {
            private readonly IKillable _killable;

            public DeadTransition(IKillable killable, IUnitState nextState)
            {
                _killable = killable;
                NextState = nextState;
            }

            public bool CanTransit => _killable.Alive == false;

            public IUnitState NextState { get; }
        }
    }
}
