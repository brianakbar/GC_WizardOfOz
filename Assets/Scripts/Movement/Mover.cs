namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    public class Mover : MonoBehaviour {
        [SerializeField] ActionScheduler movementScheduler;
        [SerializeField] Idle idle;
        [SerializeField] Move move;

        MoveLink moveActionLink;
        IdleLink idleActionLink;

        void Awake() {
            movementScheduler = GetComponent<ActionScheduler>();
            moveActionLink = Move.GetLink();
            idleActionLink = Idle.GetLink();
        }

        public void StartMoving(Vector2 direction) {
            moveActionLink.Direction = direction;
            movementScheduler.StartAction(move, moveActionLink);
        }

        public void Stop() {
            movementScheduler.StartAction(idle, idleActionLink);
        }
    }
}