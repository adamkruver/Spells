using System.Collections.Generic;

namespace Sources.BoundedContexts.Units.Domain
{
    public interface ITargetLocator
    {
        IEnumerable<IDamageable> FindTargets();
    }
}