using System;

using Server.Combat.Domain.Skills;
using Server.Combat.Domain.Units.StateMachine;

using Sources.BoundedContexts.Units.Domain;
using Sources.BoundedContexts.Units.Presentation.Views;

using Utils.Patterns.Factory;

namespace Sources.BoundedContexts.Players.Controllers
{
    public enum SpellSlot : byte
    {
        Ability1,
        Ability2,
        Ability3,
        Ability4,
        Ability5,
        Ability6,
    }

    public enum ItemSlot : byte
    {
        Item1,
        Item2
    }

    public class PlayerController
    {
        private readonly IUnitView _view;
        private readonly Unit _model;

        public PlayerController(IUnitView characterView, Unit unit)
        {
            _model = unit ?? throw new ArgumentNullException(nameof(unit));
            _view = characterView ?? throw new ArgumentNullException(nameof(characterView));
        }

        public void UseSkill(SpellSlot slot)
        {
            var attackSkill = _model.GetSkillInSlot((int) slot);

            _view.UseSkill(attackSkill);
        }
    }
}