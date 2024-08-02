using Server.Combat.Domain.Units;

namespace Server.Combat.Domain.Skills.SpellEffects
{
    public readonly struct EffectApplicationData
    {
        public IUnit Caster { get; }
        public float EffectModification { get; }
        public IUnit Target { get; }

        public EffectApplicationData(IUnit caster, IUnit target, float effectModification)
        {
            Caster = caster;
            EffectModification = effectModification;
            Target = target;
        }
    }
}