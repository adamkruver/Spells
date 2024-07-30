using System.Collections.Generic;

using Frameworks.StateMachines;

namespace Sources.Frameworks.StateMachines
{
    public interface IStateCollection<T> where T : IState
    {
        void Add(T state, IEnumerable<ITransition<T>> transitions);
        bool HasState(T state);
        IEnumerable<ITransition<T>> GetTransitions(T state);
    }
}