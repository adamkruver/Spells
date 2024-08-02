using Server.Combat.Domain.DTO;

namespace Server.Combat.Domain.Units.Components
{
    public interface IKillable
    {
        bool Alive { get; }

        void Kill(KillData data);
        void Revive(ReviveData data);
    }
}