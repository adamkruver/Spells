using System.Collections.Generic;
using System.Linq;
using Sources.Frameworks.StateMachines.Common;
using Sources.Frameworks.StateMachines.Interfaces;

namespace Sources.Frameworks.StateMachines.Implementations
{
    public class DictionaryStateCollection<T> : IStateCollection<T> where T : IState
    {
        private readonly Dictionary<IState, IEnumerable<ITransition<T>>> _states;

        public DictionaryStateCollection() => 
            _states = new();

        public void Add(T state, IEnumerable<ITransition<T>> transitions) => 
            _states.Add(state, transitions);

        public IEnumerable<ITransition<T>> GetTransitions(T state) => 
            _states.GetValueOrDefault(state, Enumerable.Empty<ITransition<T>>());

        public bool HasState(T state) => 
            _states.ContainsKey(state);
    }
}