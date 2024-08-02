namespace Server.Combat.Domain.Statuses
{
    public interface IBonusCollection
    {
        bool TryAdd(object value);
        bool Remove(object value);
    }
}
