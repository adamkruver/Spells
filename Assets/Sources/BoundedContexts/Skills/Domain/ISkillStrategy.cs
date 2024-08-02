using Sources.BoundedContexts.Units.Domain;

namespace Sources.BoundedContexts.Skills.Domain
{
    public interface ISkillStrategy
    {
        void Attack(IDamageable target);
        void Execute(float deltaTime);
        bool InProgress { get; }
    }
}