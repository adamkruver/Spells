using Server.Combat.Domain.Skills;

namespace Server.Combat.Domain.Units.Components
{
    public interface ICaster
    {
        ISkillHandler ActiveCast { get; set; }
    }
}
