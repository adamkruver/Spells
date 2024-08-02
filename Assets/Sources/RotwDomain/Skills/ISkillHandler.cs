using Server.Combat.Domain.Common;

namespace Server.Combat.Domain.Skills
{
    public interface ISkillHandler : IUpdatable
    {
        ISkill Skill { get; }
        bool IsActive { get; }

        float ActiveTime { get; }

        //void HandleHit(IUnit target);
    }
}
