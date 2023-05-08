namespace Creazen.Wizard.BehaviorTree {
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Behavior Tree", menuName = "Behavior Tree/Behavior Tree", order = 0)]
    public class BehaviorTree : ScriptableObject {
        public Node rootNode;
        public State state = State.Running;

        public State Update() {
            if(rootNode.state == State.Running) {
                state = rootNode.Update();
            }
            return state;
        }
    }
}
