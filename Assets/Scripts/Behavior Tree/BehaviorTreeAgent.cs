namespace Creazen.Wizard.BehaviorTree {
    using UnityEngine;
    
    public class BehaviorTreeAgent : MonoBehaviour {
        [SerializeField] BehaviorTree behaviorTree;

        void Update() {
            behaviorTree?.Update();
        }
    }
}