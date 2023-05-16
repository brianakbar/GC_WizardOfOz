namespace Creazen.Wizard.BehaviorTree {
    using UnityEngine;
    
    public class BehaviorTreeAgent : MonoBehaviour {
        [SerializeField] BehaviorTree behaviorTree;

        void Awake() {
            behaviorTree = behaviorTree.Clone();
            behaviorTree.Bind(gameObject);
        }

        void Update() {
            behaviorTree?.Update();
        }

        public BehaviorTree GetBehaviorTree() {
            return behaviorTree;
        }
    }
}