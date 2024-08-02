using Server.Combat.Domain.Events;

namespace Server.Combat.Domain.Units.Components
{
    public interface IEventReaction<in T> where T : IEvent
    {
        void HandleEvent(T @event);
    }

    public interface IBonusOwner<in TValue>
    {
        void ApplyBonuses(TValue target);
    }
}