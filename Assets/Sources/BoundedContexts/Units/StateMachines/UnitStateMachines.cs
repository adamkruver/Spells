using System;
using System.Collections.Generic;
using System.Linq;

using Frameworks.StateMachines;

using Sources.Frameworks.LifeCycles;
using Sources.Frameworks.StateMachines;

using UnityEngine;

namespace Sources.BoundedContexts.Units.StateMachines
{
    public class UnitStateMachine : IStateMachine<IUnitState>, IUpdatable
    {
        private readonly Dictionary<IUnitState, ITransition<IUnitState>[]> _stateMap;
        private readonly HashSet<IUnitState> _stateHistory;

        private IUnitState _currentState;
        private ITransition<IUnitState>[] _activeTransitions;

        public UnitStateMachine(Dictionary<IUnitState, ITransition<IUnitState>[]> transitions)
        {
            _stateMap = transitions;

            _stateHistory = new();
            _activeTransitions = Array.Empty<ITransition<IUnitState>>();
        }

        public IUnitState CurrentState
        {
            get => _currentState;
            private set
            {
                Exit();
                _currentState = value;
                Enter();
            }
        }

        public void Start(IUnitState state)
        {
            if(_stateMap.ContainsKey(state) == false)
            {
                throw new InvalidOperationException($"Can't start state machine with missing {state}.");
            }

            CurrentState = state;
        }

        public void Stop() => CurrentState = null;

        public void Update(float deltaTime)
        {
            if (CurrentState == null)
            {
                return;
            }

            lock (CurrentState)
            {
                CheckTransitions();
                CurrentState.Update(deltaTime);
            }
        }

        private void CheckTransitions()
        {
            ITransition<IUnitState> transition;
            IUnitState nextState;

            while ((transition = _activeTransitions.FirstOrDefault(transition => transition.CanTransit)) != null)
            {
                nextState = transition.NextState;

                if (_stateHistory.Contains(nextState))
                {
                    throw new Exception($"Loop detected in state machine: {nextState}");
                }

                _stateHistory.Add(CurrentState);
                CurrentState = nextState;
            }

            _stateHistory.Clear();
        }

        private void Enter()
        {
            _activeTransitions = _stateMap.GetValueOrDefault(_currentState, Array.Empty< ITransition<IUnitState>>());
            _currentState?.Enter();
        }

        private void Exit() => _currentState?.Exit();
    }
}
