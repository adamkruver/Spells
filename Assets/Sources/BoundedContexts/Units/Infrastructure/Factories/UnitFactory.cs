using System;

using Sources.BoundedContexts.Units.Presentation.Presenters;
using Sources.BoundedContexts.Units.Presentation.Views;
using Sources.BoundedContexts.Units.States;
using Sources.Frameworks.StateMachines;

using Object = UnityEngine.Object;

namespace Sources.BoundedContexts.Units.Infrastructure.Factories
{
    public class UnitFactory
    {
        private readonly IFiniteStateMachineBuilder _stateMachineBuilder;

        public UnitFactory(FiniteStateMachineBuilder stateMachineBuilder) =>
            _stateMachineBuilder = stateMachineBuilder ?? throw new ArgumentNullException(nameof(stateMachineBuilder));

        public IUnitView Create(UnitView unitViewPrefab)
        {
            var view = Object.Instantiate(unitViewPrefab);

            var presenter = CreatePresenter(view, CreateStateMachine());
            view.Construct(presenter);

            return view;
        }

        private UnitPresenter CreatePresenter(UnitView view, FiniteStateMachine stateMachine) => new(view, stateMachine);

        private FiniteStateMachine CreateStateMachine() => _stateMachineBuilder
                .RegisterState(new UnitIdleState())
                .RegisterState(new UnitDeadState())
                .RegisterState(new UnitCastingState())
                
                .AddTransition<UnitIdleState, UnitDeadState>(() => false)
                .AddTransition<UnitIdleState, UnitCastingState>(() => true)
                .AddTransition<UnitDeadState, UnitIdleState>(() => true)
                .AddTransition<UnitCastingState, UnitIdleState>(() => false)
                
                .SetFirstState<UnitIdleState>()
                .Build();
    }
}