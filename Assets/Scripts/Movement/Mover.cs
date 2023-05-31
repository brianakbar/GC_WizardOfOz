namespace Creazen.Wizard.Movement {
    using System;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    public class Mover : MonoBehaviour {
        [SerializeField] ActionScheduler movementScheduler;

        public bool StartMoving(Vector2 direction) {
            var input = movementScheduler.GetCache<Move>().Get<Move.Input>();
            input.moveDirection = direction;
            return movementScheduler.StartAction<Move>();
        }

        public bool Dash(Vector2 direction, Action onFinish = null, Action onCancel = null) {
            var input = movementScheduler.GetCache<Dash>().Get<Dash.Input>();
            input.moveDirection = direction;
            return movementScheduler.StartAction<Dash>(onFinish, onCancel);
        }

        public void Stop() {
            movementScheduler.StartAction<Idle>();
        }
    }
}