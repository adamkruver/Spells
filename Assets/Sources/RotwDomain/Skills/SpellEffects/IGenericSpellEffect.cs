namespace Server.Combat.Domain.Skills.SpellEffects
{
    public interface IGenericSpellEffect
    {
        void ApplyEffect(EffectApplicationData data);
    }
}