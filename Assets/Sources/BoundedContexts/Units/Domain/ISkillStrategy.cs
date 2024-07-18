namespace Sources.BoundedContexts.Units.Domain
{
    public interface ISkillStrategy
    {
        void Attack(IDamageable target);
        void Execute(float deltaTime);
        bool InProgress { get; }
    }
}