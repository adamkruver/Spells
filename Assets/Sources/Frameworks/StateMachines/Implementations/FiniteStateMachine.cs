using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Frameworks.StateMachines.Interfaces;

using Sources.Frameworks.LifeCycles;

namespace Sources.Frameworks.StateMachines
{
    public class FiniteStateMachine : IFiniteStateMachine, IUpdatable
    {
        private readonly HashSet<IFiniteState> _statesHistory = new();

        public IFiniteState CurrentState { get; private set; }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnterState() => CurrentState?.Enter();       

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ExitState() => CurrentState?.Exit();
        

        private void Change(IFiniteState state)
        {
            ExitState();
            CurrentState = state;
            EnterState();
        }
    }
}