using System;
using Sources.BoundedContexts.Units.Domain;
using Sources.BoundedContexts.Units.Presentation.Views;

namespace Sources.BoundedContexts.Players.Controllers
{
    public class PlayerController
    {
        private readonly UnitView _characterView;
        private Unit _unit;

        public PlayerController(UnitView characterView, Unit unit)
        {
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));
            
            _characterView = characterView
                ? characterView
                : throw new ArgumentNullException(nameof(characterView));
        }

        public void Attack()
        {
            var attackSkill = _unit.GetAttackSkill();
            
            _characterView.UseSkill(attackSkill);
        }
    }
}