namespace Creazen.Wizard.BehaviorTree {
    using UnityEngine;
    
    public class BehaviorTreeAgent : MonoBehaviour {
        [SerializeField] GameObject performer;
        [SerializeField] BehaviorTree behaviorTree;

        void Awake() {
            if(performer == null) performer = gameObject;

            behaviorTree = behaviorTree.Clone();
            behaviorTree.Bind(performer);
        }

        void Update() {
            behaviorTree?.Update();
        }

        public BehaviorTree GetBehaviorTree() {
            return behaviorTree;
        }
    }
}