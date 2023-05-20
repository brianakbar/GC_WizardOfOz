namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    public class NavMeshMover : MonoBehaviour {
        [SerializeField] ActionScheduler movementScheduler;

        public void StartMoving(string target) {
            movementScheduler.GetCache<NavMeshMove>().Get<NavMeshMove.Input>().target = target;
            movementScheduler.StartAction<NavMeshMove>();
        }

        public void Stop() {
            movementScheduler.StartAction<Idle>();
        }
    }
}