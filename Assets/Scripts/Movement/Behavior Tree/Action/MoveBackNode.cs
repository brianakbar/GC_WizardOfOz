namespace Creazen.Wizard.Movement.BehaviorTree.Action {
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.ActionScheduling.Targets;
    using Creazen.Wizard.BehaviorTree;
    using Creazen.Wizard.Movement;
    using UnityEngine;

    public class MoveBackNode : ActionNode {
        [SerializeField] Target moveFrom;
        [SerializeField] float interval = 1f;

        Vector3Target target;
        NavMeshMover mover;

        bool wasMoving = false;

        protected override void OnStart() {
            if(mover == null) mover = gameObject.GetComponent<NavMeshMover>();
            if(target == null) {
                target = ScriptableObject.CreateInstance<Vector3Target>();
            }
            wasMoving = false;
        }

        protected override State OnUpdate() {
            if(mover == null) mover = gameObject.GetComponent<NavMeshMover>();
            if(mover.IsMoving()) return State.Running;
            if(wasMoving && !mover.IsMoving()) {
                wasMoving = false;
                return State.Success;
            }

            Vector2 targetPosition = moveFrom.GetTargetPosition(gameObject);
            Vector2 position = mover.transform.position;
            Vector2 positionDelta = (targetPosition - position).normalized;

            target.SetTarget(new Vector3(
                position.x - interval * positionDelta.x,
                position.y - interval * positionDelta.y,
                0
            ));

            if(!mover.StartMoving(target)) return State.Failure;
            wasMoving = true;
            return State.Running;
        }

        protected override void OnStop() {
            if(mover == null) mover = gameObject.GetComponent<NavMeshMover>();
            mover.Stop();
        }
    }
}