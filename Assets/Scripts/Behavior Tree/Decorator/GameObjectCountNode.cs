namespace Creazen.Wizard.BehaviorTree.Decorator {
    using System.Collections.Generic;
    using UnityEngine;

    public class GameObjectCountNode : DecoratorNode {
        [SerializeField] List<ComparisonCondition> conditions = new List<ComparisonCondition>();
        
        [System.Serializable]
        class ComparisonCondition {
            [SerializeField] string tag = "Player";
            [SerializeField] int count = 0;
            [SerializeField] Comparison comparison;

            enum Comparison {
                LessThan,
                Equal,
                MoreThan
            }

            public bool IsSatisfied() {
                int gameObjectCount = GameObject.FindGameObjectsWithTag(tag).Length;
                if(comparison == Comparison.LessThan) {
                    return gameObjectCount < count;
                }
                else if(comparison == Comparison.Equal) {
                    return gameObjectCount == count;
                }
                else if(comparison == Comparison.MoreThan) {
                    return gameObjectCount > count;
                }
                return false;
            }
        }

        protected override State OnUpdate() {
            if(IsConditionSatisfied()) {
                return child.Update();
            }
            return State.Failure;
        }

        bool IsConditionSatisfied() {
            foreach(ComparisonCondition condition in conditions) {
                if(!condition.IsSatisfied()) return false;
            }
            return true;
        }
    }
}