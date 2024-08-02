using Server.Combat.Domain.DTO;

namespace Server.Combat.Domain.Units.Components
{
    public interface IHealthOwner
    {
        float CurrentHealth { get; }
        float MaxHealth { get; }

        void TakeDamage(DamageData damage);
        void Heal(HealingData healing);
    }
}