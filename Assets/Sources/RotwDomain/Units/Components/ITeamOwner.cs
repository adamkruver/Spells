namespace Server.Combat.Domain.Units.Components
{
    public interface ITeamOwner
    {
        byte Team { get; }

        bool CanHelp(IUnit target);
        bool CanHurt(IUnit target);
    }
}
