using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Sources.Frameworks.StateMachines
{
    public class FiniteStateMachineBuilder : IFiniteStateMachineBuilder
    {
        private readonly Dictionary<Type, IFiniteState> _states;
        private readonly Dictionary<(Type, Type), Func<bool>> _transitions;

        private IFiniteState _firstState;

        public FiniteStateMachineBuilder()
        {
            _states = new();
            _transitions = new();
        }

        IFiniteStateMachineBuilder IFiniteStateMachineBuilder.AddTransition<TFrom, TTarget>(Func<bool> condition)
        {
            (Type Source, Type Target) nodeData = (typeof(TFrom), typeof(TTarget));

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

        IFiniteStateMachineBuilder IFiniteStateMachineBuilder.SetFirstState<T>()
        {
            Type stateType = typeof(T);

            if (_states.TryGetValue(stateType, out _firstState) == false)
            {
                throw new Exception($"State with type {stateType} is not registered");
            }

            return this;
        }

        IFiniteStateMachineBuilder IFiniteStateMachineBuilder.RegisterState(IFiniteState state)
        {
            Type stateType = state.GetType();

            if (_states.TryAdd(stateType, state) == false)
            {
                throw new Exception($"State with type {stateType} already registered");
            }

            return this;
        }

        public FiniteStateMachine Build()
        {
            if (_firstState == null)
            {
                throw new Exception("First state is not set");
            }

            var stateMachine = new FiniteStateMachine();

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
            if (_states.TryGetValue(transitionData.Source, out IFiniteState sourceState))
            {
                throw new Exception($"Can't create transition from unregistered state of type {transitionData.Source}.");
            }

            if (_states.TryGetValue(transitionData.Target, out IFiniteState targetState))
            {
                throw new Exception($"Can't create transition to unregistered state of type {transitionData.Target}.");
            }

            Transition transition = new(targetState, condition);
            sourceState.AddTransition(transition);
            return transition;
        }
    }
}