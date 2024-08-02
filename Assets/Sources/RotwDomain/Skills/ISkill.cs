using Server.Combat.Domain.Units;

using Utils.DataStructure;

namespace Server.Combat.Domain.Skills
{
    public class SkillModification
    {
        public float HasteModifier { get; }
        public float CastTimeModification { get; }
        public float RecoveryTimeModification { get; }

        public SpellFlags FlagsModificaton { get; }
    }

    public readonly struct SpellPerformData
    {
        public readonly SkillModification Values;
        public readonly IUnit Caster;

        public SpellPerformData(SkillModification values, IUnit caster)
        {
            Values = values;
            Caster = caster;
        }
    }

    public interface ISkill : Entity
    {
        public SpellFlags Flags { get; }

        //TODO: move to factory repository?
        ISkillHandler Trigger(SpellPerformData data);
    }
}
