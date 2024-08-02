using UnityEngine;

namespace Server.Combat.Domain.Entities.Components
{
    public interface IPositionOwner
    {
        Vector3 Position { get; }
    }
}