using System;
using Sources.BoundedContexts.Units.Presentation.Presenters;
using Sources.BoundedContexts.Units.Presentation.Views;
using Sources.BoundedContexts.Units.States;
using Sources.Frameworks.StateMachines;
using Sources.Frameworks.StateMachines.Extensions;
using Sources.Frameworks.StateMachines.Implementations;
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

        private UnitPresenter CreatePresenter(UnitView view, FiniteStateMachine stateMachine) => new(view, stateMachine);

        //private FiniteStateMachine CreateStateMachine() => _stateMachineBuilder
        //        .RegisterState(new UnitIdleState())
        //        .RegisterState(new UnitDeadState())
        //        .RegisterState(new UnitCastingState())

        //        .AddTransition<UnitIdleState, UnitDeadState>(() => false)
        //        .AddTransition<UnitIdleState, UnitCastingState>(() => true)
        //        .AddTransition<UnitDeadState, UnitIdleState>(() => true)
        //        .AddTransition<UnitCastingState, UnitIdleState>(() => false)

        //        .SetFirstState<UnitIdleState>()
        //        .Build();

        private FiniteStateMachine CreateStateMachine() => _stateMachineBuilder
                .AddState(new UnitIdleState())
                    .AddTransitionToLast<UnitDeadState>(() => false)
                    .AddTransitionToLast<UnitCastingState>(() => true)
                .AddState(new UnitDeadState())
                    .AddTransitionToLast<UnitIdleState>(() => true)
                .AddState(new UnitCastingState())
                    .AddTransitionToLast<UnitIdleState>(() => false)
                .SetFirstState<UnitIdleState>()
                .Build();
    }
}