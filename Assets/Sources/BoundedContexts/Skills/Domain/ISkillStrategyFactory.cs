using Server.Combat.Domain.Skills;

namespace Sources.BoundedContexts.Units.Domain
{
    public interface ISkillStrategyFactory : ISkill
    {
        ISkillStrategy Create(ITargetLocator targetLocator);
    }
}