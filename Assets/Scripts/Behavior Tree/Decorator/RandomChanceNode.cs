namespace Creazen.Wizard.BehaviorTree.Decorator {
    using UnityEngine;

    public class RandomChanceNode : DecoratorNode {
        [SerializeField] [Range(0, 1)] float chance = 0.5f;

        bool isChanceSuccess = false;

        protected override void OnStart() {
            isChanceSuccess = false;
        }

        protected override State OnUpdate() {
            if(isChanceSuccess) {
                return child.Update();
            }

            if(Random.Range(0f, 1f) < chance) { 
                isChanceSuccess = true;
                return State.Running;
            }
            return State.Failure;
        }
    }
}
