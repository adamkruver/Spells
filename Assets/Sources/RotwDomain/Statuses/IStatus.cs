using Server.Combat.Domain.Common;
using Server.Combat.Domain.Units;

namespace Server.Combat.Domain.Statuses
{
    public interface IStatus : IUpdatable
    {
        int AuraId { get; }
        IUnit Parent { get; }
        IUnit Caster { get; }
        Duration Duration { get; set; }
        int StackCount { get; set; }
    }
}
