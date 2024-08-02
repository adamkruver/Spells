using Server.Combat.Domain.Common;
using Server.Combat.Domain.Skills;

namespace Server.Combat.Domain.Units.Components
{
    public interface ISpellOwner
    {
        void GiveSpell(ISkill spell);
        void RemoveSpell(ISkill spell);

        Duration GetCooldown(ISkill spell);
        void SetCooldown(ISkill spell, Duration cooldown);

        SkillModification GetSpellModification(ISkill spell);
    }
}