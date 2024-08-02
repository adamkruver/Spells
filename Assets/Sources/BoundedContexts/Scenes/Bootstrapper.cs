using Assets.Sources.BoundedContexts.Input;
using Assets.Sources.BoundedContexts.Players.Infrastucture.Factories;

using Sources.BoundedContexts.Players.Controllers;
using Sources.BoundedContexts.Units.Domain;

using UniCtor.Attributes;

using UnityEngine;

using Utils.Patterns.Factory;

namespace Sources.BoundedContexts.Scenes
{
    public class Bootstrapper : MonoBehaviour
    {
        private Factory<Unit> _modelFactory;
        private IInputService _inputService;

        private PlayerController _playerController;

        [Constructor]
        private void Construct(Factory<PlayerController, PlayerControllerFactory.PlayerControllerCreationData> playerContollerFactory, Factory<Unit> modelFactory, IInputService inputService)
        {
            _inputService = inputService;

            Debug.Log(inputService);
            _modelFactory = modelFactory;

            Unit model = modelFactory.Create();

            _playerController = playerContollerFactory.Create(new(model));
            _inputService.SkillSlotUsed += _playerController.UseSkill;

        }

        private void OnDestroy()
        {
            _inputService.SkillSlotUsed -= _playerController.UseSkill;
        }

        private void Update()
        {
            _inputService.Update(Time.deltaTime);
        }
    }
}
