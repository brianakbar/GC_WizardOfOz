namespace Creazen.Wizard.Movement.BehaviorTree.Action {
    using Creazen.Wizard.BehaviorTree;
    using Creazen.Wizard.Movement;
    using UnityEngine;

    public class ChaseTargetNode : ActionNode {
        [SerializeField] string target = "Player";

        NavMeshMover mover;

        protected override State OnUpdate() {
            if(mover == null) mover = gameObject.GetComponent<NavMeshMover>();
            mover.StartMoving(target);
            return State.Running;
        }

        protected override void OnStop() {
            if(mover == null) mover = gameObject.GetComponent<NavMeshMover>();
            mover.Stop();
        }
    }
}