using System;

using Server.Combat.Domain.Units;

namespace Server.Combat.Domain.DTO
{
    [Flags]
    public enum DamageFlags
    {
        None = 0,
        NonLethal = 1,
        Reflected = 2,
        NonReactable = 4,
    }

    public readonly struct DamageData
    {
        public readonly IUnit Attacker;
        public readonly IUnit Victim;
        public readonly float Damage;
        public readonly DamageFlags Flags;

        public DamageData(IUnit attacker, IUnit victim, float damage, DamageFlags flags)
        {
            Attacker = attacker;
            Victim = victim;
            Damage = damage;
            Flags = flags;
        }
    }
}
