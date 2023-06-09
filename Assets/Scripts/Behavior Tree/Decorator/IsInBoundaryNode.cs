namespace Creazen.Wizard.BehaviorTree.Decorator {
    using Creazen.Wizard.ActionScheduling.Targets;
    using UnityEngine;

    public class IsInBoundaryNode : DecoratorNode {
        [SerializeField] BoundaryTarget target;
        [SerializeField] bool negate = false;

        protected override State OnUpdate() {
            Vector3 position = new Vector3(
                gameObject.transform.position.x,
                gameObject.transform.position.y,
                0
            );
            bool isInBoundary = target.Contains(position);
            if(isInBoundary != negate) {
                child.Update();
                return State.Running;
            }
            return State.Failure;
        }
    }
}