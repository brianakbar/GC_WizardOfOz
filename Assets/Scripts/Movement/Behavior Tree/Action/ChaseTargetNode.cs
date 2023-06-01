namespace Creazen.Wizard.Movement.BehaviorTree.Action {
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.BehaviorTree;
    using Creazen.Wizard.Movement;
    using UnityEngine;

    public class ChaseTargetNode : ActionNode {
        [SerializeField] Target target;

        NavMeshMover mover;

        protected override State OnUpdate() {
            if(mover == null) mover = gameObject.GetComponent<NavMeshMover>();
            if(mover.IsMoving()) return State.Running;

            if(!mover.StartMoving(target)) return State.Failure;
            return State.Running;
        }

        protected override void OnStop() {
            if(mover == null) mover = gameObject.GetComponent<NavMeshMover>();
            mover.Stop();
        }
    }
}