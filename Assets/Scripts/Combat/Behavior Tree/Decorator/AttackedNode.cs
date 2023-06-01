namespace Creazen.Wizard.Combat.BehaviorTree.Decorator {
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.BehaviorTree;

    public class AttackedNode : DecoratorNode {
        ActionScheduler scheduler;
        bool hasStarted = false;

        protected override void OnStart() {
            if(scheduler == null) scheduler = gameObject.GetComponent<ActionScheduler>();
            hasStarted = false;
        }

        protected override State OnUpdate() {
            if(hasStarted) return child.Update();

            if(scheduler == null) scheduler = gameObject.GetComponent<ActionScheduler>();

            if(scheduler != null) {
                if(scheduler.CurrentAction.GetType() == typeof(Damage)) {
                    child.Update();
                    hasStarted = true;
                    return State.Running;
                }
            }

            Abort(State.Failure);
            return State.Failure;
        }
    }
}