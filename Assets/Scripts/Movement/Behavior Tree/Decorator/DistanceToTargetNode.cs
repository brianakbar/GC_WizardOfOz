namespace Creazen.Wizard.Movement.BehaviorTree.Decorator {
    using Creazen.Wizard.BehaviorTree;
    using UnityEngine;

    public class DistanceToTargetNode : DecoratorNode {
        [SerializeField] string targetTag = "Player";
        [SerializeField] float distance = 5f;
        [SerializeField] Comparison comparison;

        Transform target;

        enum Comparison {
            LessThan,
            Equal,
            MoreThan
        }

        protected override void OnStart() {
            target = GameObject.FindGameObjectWithTag(targetTag).transform;
        }

        protected override State OnUpdate() {
            if(IsInRange()) {
                if(child != null) child.Update();
                return State.Running;
            }
            Abort(State.Failure);
            return State.Failure;
        }

        bool IsInRange() {
            float distanceToTarget = Vector3.Distance(gameObject.transform.position, target.position);
            if(comparison == Comparison.LessThan) {
                return distanceToTarget < distance;
            }
            else if(comparison == Comparison.Equal) {
                return Mathf.Approximately(distanceToTarget, distance);
            }
            else if(comparison == Comparison.MoreThan) {
                return distanceToTarget > distance;
            }
            return false;
        }
    }
}