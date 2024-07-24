using System;
using System.Collections.Generic;
using System.Linq;

namespace Sources.Frameworks.StateMachines
{
    public class FiniteStateMachineBuilder : IFiniteStateBuilder
    {
        private readonly Dictionary<Type, IFiniteState> _states = new Dictionary<Type, IFiniteState>();
        
        private IFiniteState _firstState;

        IFiniteState IFiniteStateBuilder.State { get; set; }

        void IFiniteStateMachineBuilder.AddTransition(IFiniteState state, Func<bool> condition)
        {
            Type stateType = state?.GetType()?? throw new ArgumentNullException(nameof(state)); 
            
            if(_states.Keys.Contains(stateType) == false)
                throw new Exception($"State with type {stateType} is not registered");

            state.Add(new Transition(_states[stateType], condition)); 
        }

        public IFiniteStateMachineBuilder SetFirstState<T>()
        {
            Type stateType = typeof(T);
            
            if(_states.TryGetValue(stateType, out _firstState) == false)
                throw new Exception($"State with type {stateType} is not registered");
            
            return this;
        }

        public IFiniteStateBuilder AddState<T>(T state) where T: IFiniteState
        {
            _states.TryAdd(state.GetType(), state);

            return this;
        }

        public FiniteStateMachine Build()
        {
            var stateMachine = new FiniteStateMachine();
            
            if(_firstState == null)
                throw new Exception("First state is not set");
            
            stateMachine.Start(_firstState);
            Clear();
            
            return stateMachine;
        }

        private void Clear()
        {
            _states.Clear();
            _firstState = null;
        }
    }
}