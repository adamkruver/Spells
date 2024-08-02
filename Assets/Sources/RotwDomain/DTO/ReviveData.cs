using System;

using Server.Combat.Domain.Units;

namespace Server.Combat.Domain.DTO
{
    [Flags]
    public enum ReviveFlags
    {
        None = 0,
        Healed = 1,
        Forced = 2,
        NonReactable = 4,
    }

    public readonly struct ReviveData
    {
        public readonly IUnit Reviver;
        public readonly IUnit Revivee;
        public readonly ReviveFlags Flags;

        public ReviveData(IUnit reviver, IUnit revivee, ReviveFlags flags)
        {
            Reviver = reviver;
            Revivee = revivee;
            Flags = flags;
        }
    }
}
