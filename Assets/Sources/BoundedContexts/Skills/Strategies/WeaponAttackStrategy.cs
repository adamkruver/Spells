using Sources.BoundedContexts.Units.Domain;
using Sources.Frameworks.StateMachines;

namespace Sources.BoundedContexts.Skills.Strategies
{
    public class WeaponAttackStrategy : FiniteStateMachine, ISkillStrategy
    {
        public bool InProgress { get; private set; }

        public void Attack(IDamageable target)
        {
            throw new System.NotImplementedException();
        }

        public void Execute(float deltaTime) => 
            Update(deltaTime);
    }
}