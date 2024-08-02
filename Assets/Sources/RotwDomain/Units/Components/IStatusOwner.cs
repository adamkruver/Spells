using Server.Combat.Domain.Common;
using Server.Combat.Domain.Statuses;

namespace Server.Combat.Domain.Units.Components
{
    public interface IStatusOwner : IUpdatable
    {
        void AddStatus(IStatus status);
        void RemoveStatus(int auraId);
        bool HasStatus(int auraId);
    }
}