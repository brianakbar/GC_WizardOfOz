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
            [SerializeField] By by = By.Vector3;
            [SerializeField] float distance = 5f;
            [SerializeField] Comparison comparison;

            enum By {
                X,
                Y,
                Z,
                Vector2,
                Vector3
            }

            public bool IsInRange(GameObject gameObject, Transform target) {
                float distanceToTarget = Distance(gameObject.transform, target);
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

            float Distance(Transform from, Transform to) {
                if(by == By.Vector2) {
                    return Vector2.Distance(from.position, to.position);
                }
                if(by == By.X) {
                    return Mathf.Abs(from.position.x - to.position.x);
                }
                if(by == By.Y) {
                    return Mathf.Abs(from.position.y - to.position.y);
                }
                if(by == By.Z) {
                    return Mathf.Abs(from.position.z - to.position.z);
                }
                return Vector3.Distance(from.position, to.position);
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