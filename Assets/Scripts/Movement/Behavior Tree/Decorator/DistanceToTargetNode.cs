namespace Creazen.Wizard.Movement.BehaviorTree.Decorator {
    using System.Collections.Generic;
    using Creazen.Wizard.BehaviorTree;
    using UnityEngine;

    public class DistanceToTargetNode : DecoratorNode {
        [SerializeField] string targetTag = "Player";
        [SerializeField] List<ComparisonCondition> conditions = new List<ComparisonCondition>();

        Transform target;

        [System.Serializable]
        class ComparisonCondition {
            [SerializeField] float distance = 5f;
            [SerializeField] Comparison comparison;

            public bool IsInRange(GameObject gameObject, Transform target) {
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

        enum Comparison {
            LessThan,
            Equal,
            MoreThan
        }

        protected override void OnStart() {
            target = GameObject.FindGameObjectWithTag(targetTag)?.transform;
        }

        protected override State OnUpdate() {
            if(target == null) return State.Failure;

            if(IsConditionSatisfied()) {
                if(child != null) child.Update();
                return State.Running;
            }
            
            Abort(State.Failure);
            return State.Failure;
        }

        bool IsConditionSatisfied() {
            foreach(ComparisonCondition condition in conditions) {
                if(!condition.IsInRange(gameObject, target)) return false;
            }
            return true;
        }
    }
}