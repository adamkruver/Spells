namespace Sources.BoundedContexts.Units.Domain
{
    public interface ISkillStrategyFactory
    {
        ISkillStrategy Create(ITargetLocator targetLocator);
    }
}