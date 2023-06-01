namespace Creazen.Wizard.Combat.BehaviorTree.Action {
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.BehaviorTree;
    using UnityEngine;

    public class ChangeAimNode : ActionNode {
        [SerializeField] Target to;

        protected override State OnUpdate() {
            if(gameObject.TryGetComponent<Fighter>(out Fighter fighter)) {
                fighter.ChangeAim(to);

                return State.Success;
            }

            return State.Failure;
        }
    }
}
