using System;

using Utils.DataTypes;

namespace Server.Combat.Domain.Attributes
{
    public interface IAttributeCollection<T> where T : struct, Enum
    {
        void Add(IAttributeCollection<T> attribute);

        PercentModifiedValue this[T attribute] { get; set; }
    }
}
