using System;

using Server.Combat.Domain.Units;

namespace Server.Combat.Domain.DTO
{
    [Flags]
    public enum KillFlags
    {
        None = 0,
        CantRevive = 1,
        Forced = 2,
        NonReactable = 4,
    }

    public readonly struct KillData
    {
        public readonly IUnit Attacker;
        public readonly IUnit Victim;
        public readonly KillFlags Flags;

        public KillData(IUnit attacker, IUnit victim, KillFlags flags)
        {
            Attacker = attacker;
            Victim = victim;
            Flags = flags;
        }
    }
}
