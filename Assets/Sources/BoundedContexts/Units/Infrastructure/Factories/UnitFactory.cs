using System;
using Sources.BoundedContexts.Units.Presentation.Presenters;
using Sources.BoundedContexts.Units.Presentation.Views;
using Sources.BoundedContexts.Units.States;
using Sources.Frameworks.StateMachines;
using Sources.Frameworks.StateMachines.Extensions;
using Object = UnityEngine.Object;

namespace Sources.BoundedContexts.Units.Infrastructure.Factories
{
    public class UnitFactory
    {
        private readonly FiniteStateMachineBuilder _stateMachineBuilder;

        public UnitFactory(FiniteStateMachineBuilder stateMachineBuilder) => 
            _stateMachineBuilder = stateMachineBuilder ?? throw new ArgumentNullException(nameof(stateMachineBuilder));

        public IUnitView Create(UnitView unitViewPrefab)
        {
            var view = Object.Instantiate(unitViewPrefab);

            var presenter = CreatePresenter(view, CreateStateMachine());
            view.Construct(presenter);

            return view;
        }

        private UnitPresenter CreatePresenter(UnitView view, FiniteStateMachine stateMachine) => 
            new UnitPresenter(view, stateMachine);

        private FiniteStateMachine CreateStateMachine() =>
            _stateMachineBuilder
                .AddState(new UnitIdleState())
                .AddTransition(() => true)
                .AddTransition(() => false)
                .AddState(new UnitDeadState())
                .AddTransition(() => true)
                .SetFirstState<UnitIdleState>()
                .Build();
    }
}