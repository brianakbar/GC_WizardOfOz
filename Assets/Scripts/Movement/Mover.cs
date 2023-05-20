namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    public class Mover : MonoBehaviour {
        [SerializeField] ActionScheduler movementScheduler;

        public bool StartMoving(Vector2 direction) {
            var input = movementScheduler.GetCache<Move>().Get<Move.Input>();
            input.moveDirection = direction;
            return movementScheduler.StartAction<Move>();
        }

        public void Stop() {
            movementScheduler.StartAction<Idle>();
        }
    }
}