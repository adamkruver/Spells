using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sources.Frameworks.StateMachines.Interfaces;

namespace Sources.Frameworks.StateMachines.Implementations
{
    public class FiniteStateMachineBuilder
    {
        private readonly Dictionary<Type, FiniteState> _states;
        private readonly Dictionary<(Type, Type), Func<bool>> _transitions;

        private FiniteState _firstState;

        public FiniteStateMachineBuilder()
        {
            _states = new();
            _transitions = new();
        }

        public Type LastAddedState { get; private set; }

        public FiniteStateMachineBuilder AddState(FiniteState state)
        {
            Type stateType = state.GetType();

            if (_states.TryAdd(stateType, state) == false)
            {
                throw new Exception($"State with type {stateType} already added");
            }

            return this;
        }

        public FiniteStateMachineBuilder AddTransition<T>(Type from, Func<bool> condition) where T : class, IFiniteState
        {
            (Type Source, Type Target) nodeData = (from, typeof(T));

            if (nodeData.Source == nodeData.Target)
            {
                throw new InvalidOperationException("Source and target states can't be of same type.");
            }

            if (_transitions.TryAdd(nodeData, condition) == false)
            {
                throw new InvalidOperationException("Can't assign multiple transitions of same type.");
            }

            return this;
        }

        public FiniteStateMachineBuilder SetFirstState<T>() where T : class, IFiniteState
        {
            Type stateType = typeof(T);

            if (_states.TryGetValue(stateType, out _firstState) == false)
            {
                throw new Exception($"State with type {stateType} is not registered");
            }

            return this;
        }

        public FiniteStateMachine Build()
        {
            if (_firstState == null)
            {
                throw new Exception("First state is not set");
            }

            FiniteStateMachine stateMachine = new();

            CreateTransitions();

            stateMachine.Start(_firstState);

            Clear();

            return stateMachine;
        }

        public void Clear()
        {
            _states.Clear();
            _transitions.Clear();
            _firstState = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CreateTransitions()
        {
            foreach (KeyValuePair<(Type, Type), Func<bool>> value in _transitions)
            {
                CreateTransition(value.Key, value.Value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Transition CreateTransition((Type Source, Type Target) transitionData, Func<bool> condition)
        {
            if (_states.TryGetValue(transitionData.Source, out FiniteState sourceState))
            {
                throw new Exception($"Can't create transition from unregistered state of type {transitionData.Source}.");
            }

            if (_states.TryGetValue(transitionData.Target, out FiniteState targetState))
            {
                throw new Exception($"Can't create transition to unregistered state of type {transitionData.Target}.");
            }

            Transition transition = new(targetState, condition);
            sourceState.AddTransition(transition);

            return transition;
        }
    }
}
