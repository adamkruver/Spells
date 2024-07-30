using System;
using System.Collections.Generic;
using System.Linq;

using Frameworks.StateMachines;

using Sources.BoundedContexts.Units.StateMachines;
using Sources.Frameworks.StateMachines;

namespace Server.Combat.Domain.Implementations.Units.Components
{
    public class UnitStateMachine : IStateMachine<IUnitState>
    {
        private readonly IStateCollection<IUnitState> _states;

        private readonly HashSet<IUnitState> _stateHistory;
        private readonly int _maxRecursionDepth;

        private IUnitState _currentState;
        private IEnumerable<ITransition<IUnitState>> _activeTransitions;

        private int _currentRecursionDepth;

        public UnitStateMachine(IStateCollection<IUnitState> states)
        {
            _states = states;

            _stateHistory = new();
            _maxRecursionDepth = 2;

            _currentState = null;
            _activeTransitions = Enumerable.Empty<ITransition<IUnitState>>();
        }

        public IUnitState CurrentState
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

        public void Run(IUnitState firstState)
        {
            if (_states.HasState(firstState) == false)
            {
                throw new InvalidOperationException($"Can't use external state {firstState}.");
            }

            CurrentState = firstState;
        }

        public void Stop()
        {
            CurrentState = null;
        }

        public void Update(float deltaTime)
        {
            if (CurrentState == null)
            {
                return;
            }

            UpdateTransitions();
            CurrentState.Update(deltaTime);
        }

        private void UpdateTransitions()
        {
            _currentRecursionDepth = 0;

            while (HasNextState(out IUnitState state))
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

        private bool HasNextState(out IUnitState unitState)
        {
            ITransition<IUnitState> transition;

            if ((transition = _activeTransitions.FirstOrDefault(transition => transition.CanTransit)) == null)
            {
                unitState = null;
                return false;
            }

            unitState = transition.NextState;
            return true;
        }
    }
}
