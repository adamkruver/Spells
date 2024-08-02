using System.Collections.Generic;
using Sources.Frameworks.StateMachines.Common;

namespace Sources.Frameworks.StateMachines.Interfaces
{
    public interface IStateCollection<T> where T : IState
    {
        void Add(T state, IEnumerable<ITransition<T>> transitions);
        bool HasState(T state);
        IEnumerable<ITransition<T>> GetTransitions(T state);
    }
}