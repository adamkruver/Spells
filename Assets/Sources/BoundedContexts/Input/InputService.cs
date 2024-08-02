using System;

using Server.Combat.Domain.Common;

using Sources.BoundedContexts.Players.Controllers;

namespace Assets.Sources.BoundedContexts.Input
{
    public interface IInputService : IUpdatable
    {
        event Action<SpellSlot> SkillSlotUsed;
    }

    public class InputService : IInputService
    {
        public event Action<SpellSlot> SkillSlotUsed;

        public void Update(float deltaTime)
        {
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha1))
            {
                SkillSlotUsed.Invoke(SpellSlot.Ability1);
            }
        }
    }
}
