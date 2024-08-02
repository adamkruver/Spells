namespace Server.Combat.Domain.Skills.SpellEffects
{
    public interface ISpellEffectValue
    {
        float GetValue(EffectApplicationData data);
    }
}