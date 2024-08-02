using Server.Combat.Domain.DTO;

using Utils.DataTypes;

namespace Server.Combat.Domain.Units.Components
{
    public interface IResourcesOwner
    {
        float GetResourceValue(ResourceType type);
        void GiveResource(ResourceValue value);
        void SpendResource(ResourceValue value);
        bool HasResource(ResourceType type);
    }
}