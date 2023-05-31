namespace Creazen.Wizard.Movement.BehaviorTree.Action {
    using Creazen.Wizard.BehaviorTree;
    using Creazen.Wizard.Movement;

    public class ReturnNode : ActionNode {
        NavMeshMover mover;

        protected override void OnStart() {
            if(mover == null) mover = gameObject.GetComponent<NavMeshMover>();
            mover.Return();
        }

        protected override State OnUpdate() {
            if(mover == null) mover = gameObject.GetComponent<NavMeshMover>();
            if(!mover.IsMoving()) return State.Success;

            return State.Running;
        }

        protected override void OnStop() {
            if(mover == null) mover = gameObject.GetComponent<NavMeshMover>();
            mover.Stop();
        }
    }
}