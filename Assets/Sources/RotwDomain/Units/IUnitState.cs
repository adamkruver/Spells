using Server.Combat.Domain.Common;
using Server.Combat.Domain.Skills;

using Utils.Patterns.StateMachine;

namespace Server.Combat.Domain.Units.StateMachine
{
    public interface IUnitState : IState, IUpdatable
    {
        bool CanCast(ISkill spell, SkillModification spellModification);
    }
}