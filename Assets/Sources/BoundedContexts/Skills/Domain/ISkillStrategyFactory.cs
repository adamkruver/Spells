using Sources.BoundedContexts.Units.Domain;

namespace Sources.BoundedContexts.Skills.Domain
{
    public interface ISkillStrategyFactory
    {
        ISkillStrategy Create(ITargetLocator targetLocator);
    }
}