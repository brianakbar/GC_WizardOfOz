namespace Creazen.Wizard.Movement.BehaviorTree.Action {
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.BehaviorTree;
    using Creazen.Wizard.Movement;
    using UnityEngine;

    public class TeleportNode : ActionNode {
        [SerializeField] Target target;

        ActionScheduler scheduler;
        NavMeshMover mover;

        protected override State OnUpdate() {
            if(scheduler == null) scheduler = gameObject.GetComponent<ActionScheduler>();
            if(scheduler.IsPerforming<Teleport>()) return State.Running;

            var input = scheduler.GetCache<Teleport>().Get<Teleport.Input>();
            input.teleportPosition = target.GetTargetPosition(gameObject);

            if(!scheduler.StartAction<Teleport>()) return State.Failure;
            return State.Running;
        }

        protected override void OnStop() {
            if(mover == null) mover = gameObject.GetComponent<NavMeshMover>();
            mover.Stop();
        }
    }
}