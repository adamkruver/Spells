using System;

using Server.Combat.Domain.Units;

namespace Server.Combat.Domain.DTO
{
    [Flags]
    public enum HealingFlags
    {
        None = 0,
        CanRevive = 1,
        Reflected = 2,
        NonReactable = 4,
    }

    public readonly struct HealingData
    {
        public readonly IUnit Healer;
        public readonly IUnit Healee;
        public readonly float Healing;
        public readonly HealingFlags Flags;

        public HealingData(IUnit healer, IUnit healee, float healing, HealingFlags flags)
        {
            Healer = healer;
            Healee = healee;
            Healing = healing;
            Flags = flags;
        }
    }
}
