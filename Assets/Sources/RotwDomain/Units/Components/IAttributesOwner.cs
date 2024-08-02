using Server.Combat.Domain.Attributes;

namespace Server.Combat.Domain.Units.Components
{
    public interface IAttributesOwner
    {
        float GetAttributeValue(Attribute attribute);
    }
}
