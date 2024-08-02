using Server.Combat.Domain.Entities;
using Server.Combat.Domain.Units.Components;

namespace Server.Combat.Domain.Units
{
    public interface IUnit : IEntity, IStatusOwner, IHealthOwner, IKillable, ISpellOwner, IResourcesOwner, ITeamOwner, IAttributesOwner, ICaster, IEquipmentOwner
    {
    }
}
