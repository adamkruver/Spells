using System;

namespace Sources.BoundedContexts.Units.Domain
{
    public interface IKillable
    {
        bool Alive { get; }
    }

    public interface ICaster
    {
        ISkillStrategy ActiveCast { get; set; }
    }

    public class Unit : IKillable, ICaster
    {
        public bool Alive { get; set; }

        public ISkillStrategy ActiveCast { get; set; }

        public ISkillStrategyFactory GetAttackSkill()
        {
            throw new NotImplementedException();
        }
    }
}