namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.ActionScheduling.Targets;
    using UnityEngine;

    public class NavMeshMover : MonoBehaviour {
        [SerializeField] ActionScheduler movementScheduler;
        
        Vector3Target originalPosition;

        void Awake() {
            originalPosition = ScriptableObject.CreateInstance<Vector3Target>();
            originalPosition.SetTarget(transform.position);
        }

        public bool StartMoving(Target target) {
            movementScheduler.GetCache<NavMeshMove>().Get<NavMeshMove.Input>().target = target;
            return movementScheduler.StartAction<NavMeshMove>();
        }

        public void Return() {
            StartMoving(originalPosition);
        }

        public bool IsMoving() {
            return movementScheduler.CurrentAction?.GetType() == typeof(NavMeshMove);
        }

        public void Stop() {
            movementScheduler.StartAction<Idle>();
        }
    }
}