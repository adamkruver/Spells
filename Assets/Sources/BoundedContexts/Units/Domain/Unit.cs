using Server.Combat.Domain.Skills;

namespace Sources.BoundedContexts.Units.Domain
{
    public interface IKillable
    {
        bool Alive { get; }
    }

    public interface ICaster
    {
        ISkillHandler ActiveCast { get; set; }
    }

    public class Unit : IKillable, ICaster
    {
        public bool Alive { get; set; } = true;
        public ISkillHandler ActiveCast { get; set; }

        public ISkill GetSkillInSlot(int slotId)
        {
            return null;
        }
    }
}