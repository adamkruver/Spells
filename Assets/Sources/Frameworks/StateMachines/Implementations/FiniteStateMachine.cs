using System;
using System.Collections.Generic;

using Frameworks.StateMachines;
using Frameworks.StateMachines.Interfaces;

using Sources.Frameworks.LifeCycles;

namespace Sources.Frameworks.StateMachines
{
    public class FiniteStateMachine : IFiniteStateMachine, IStateChanger<IFiniteState>, IUpdatable
    {
        private readonly HashSet<IFiniteState> _statesHistory = new();

        public IFiniteState CurrentState { get; private set; }

        void IStateChanger<IFiniteState>.Change(IFiniteState state)
        {
            ExitState();
            CurrentState = state;
            EnterState();
        }

        public void Start(IFiniteState state) => Change(state);

        public void Stop() => Change(null);

        public void Update(float deltaTime)
        {
            if (CurrentState == null)
            {
                return;
            }

            while (CurrentState.CanTransit(out IFiniteState nextState))
            {
                if (_statesHistory.Contains(nextState))
                {
                    throw new Exception($"Loop detected in state machine: {nextState}");
                }

                _statesHistory.Add(CurrentState);
                Change(nextState);
            }

            _statesHistory.Clear();

            CurrentState.Update(deltaTime);
        }

        private void EnterState()
        {
            if (CurrentState == null)
            {
                return;
            }

            CurrentState.Enter();
        }

        private void ExitState()
        {
            if (CurrentState == null)
            {
                return;
            }

            CurrentState.Exit();
        }

        private void Change(IFiniteState state) => ((IStateChanger<IFiniteState>) this).Change(state);
    }
}