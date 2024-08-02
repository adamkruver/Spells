using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Frameworks.LifeCycles;
using Sources.Frameworks.StateMachines.Common;
using Sources.Frameworks.StateMachines.Interfaces;

namespace Sources.BoundedContexts.Units.StateMachines
{
    public class UnitStateMachine<T> : IStateMachine<T> where T: IUpdatableState
    {
        private readonly IStateCollection<T> _states;
        private readonly HashSet<T> _stateHistory;
        private readonly T _firstState;
        private readonly int _maxRecursionDepth;

        private T _currentState;
        private IEnumerable<ITransition<T>> _activeTransitions;

        private int _currentRecursionDepth;

        public UnitStateMachine(IStateCollection<T> states, T firstState)
        {
            _states = states ?? throw new ArgumentNullException(nameof(states));
            _firstState = firstState ?? throw new ArgumentNullException(nameof(firstState));

            if (_states.HasState(firstState) == false)
                throw new ArgumentException("Can't use external state.", nameof(firstState));

            _stateHistory = new();
            _maxRecursionDepth = 2;

            _currentState = default;
            _activeTransitions = Enumerable.Empty<ITransition<T>>();
        }

        public T CurrentState
        {
            get => _currentState;
            private set
            {
                _currentState?.Exit();
                _currentState = value;
                _activeTransitions = _states.GetTransitions(value);
                _currentState?.Enter();
            }
        }

        public void Run() =>
            CurrentState = _firstState;

        public void Stop() =>
            CurrentState = default;

        public void Update(float deltaTime)
        {
            if (CurrentState == null)
                return;

            UpdateTransitions();
            CurrentState.Update(deltaTime);
        }

        private void UpdateTransitions()
        {
            _currentRecursionDepth = 0;

            while (HasNextState(out T state))
            {
                if (_stateHistory.Contains(state))
                {
                    if (++_currentRecursionDepth == _maxRecursionDepth)
                    {
                        throw new Exception($"Loop detected in state machine: {state}");
                    }
                }

                _stateHistory.Add(CurrentState);
                CurrentState = state;
            }

            _stateHistory.Clear();
        }

        private bool HasNextState(out T updatableState)
        {
            ITransition<T> transition = _activeTransitions.FirstOrDefault(transition => transition.CanTransit);

            updatableState = default;
            
            if (transition is not null)
                updatableState = transition.NextState;

            return updatableState != null;
        }
    }
}