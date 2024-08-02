using Sources.BoundedContexts.Players.Controllers;
using Sources.BoundedContexts.Units.Domain;
using Sources.BoundedContexts.Units.Infrastructure.Factories;

using Utils.Patterns.Factory;

namespace Assets.Sources.BoundedContexts.Players.Infrastucture.Factories
{
    public class PlayerControllerFactory : Factory<PlayerController, PlayerControllerFactory.PlayerControllerCreationData>
    {
        private readonly UnitViewFactory _unitViewFactory;

        public PlayerControllerFactory(UnitViewFactory unitViewFactory)
        {
            _unitViewFactory = unitViewFactory;
        }

        public PlayerController Create(PlayerControllerCreationData data)
        {
            return new(_unitViewFactory.Create(new(data.Model)), data.Model);
        }

        public readonly struct PlayerControllerCreationData
        {
            public readonly Unit Model;

            public PlayerControllerCreationData(Unit model)
            {
                Model = model;
            }
        }
    }
}
