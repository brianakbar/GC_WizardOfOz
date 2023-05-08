namespace Creazen.Wizard.BehaviorTree.Action {
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Debug Log Node", menuName = "Behavior Tree/Action/Debug Log")]
    public class DebugLogNode : ActionNode {
        [SerializeField] string message;

        protected override void OnStart() {
            Debug.Log($"OnStart: {message}");
        }

        protected override void OnStop() {
            Debug.Log($"OnStop: {message}");
        }

        protected override State OnUpdate() {
            Debug.Log($"OnUpdate: {message}");
            return State.Success;
        }
    }
}