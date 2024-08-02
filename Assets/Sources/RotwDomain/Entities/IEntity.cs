using Server.Combat.Domain.Entities.Components;

using Utils.DataStructure;

namespace Server.Combat.Domain.Entities
{
    public interface IEntity : Entity, IPositionOwner
    {
    }
}
