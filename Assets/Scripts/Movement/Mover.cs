namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    public class Mover : MonoBehaviour {
        [SerializeField] Animator body;
        [SerializeField] ActionScheduler movementScheduler;
        [SerializeField] Idle idle;
        [SerializeField] Move move;

        MoveLink moveActionLink;
        IdleLink idleActionLink;
        Rigidbody2D rb2D;

        void Awake() {
            rb2D = GetComponent<Rigidbody2D>();
            moveActionLink = Move.GetLink();
            idleActionLink = Idle.GetLink();
        }

        void Update() {
            body.SetBool("hasSpeed", rb2D.velocity != Vector2.zero);
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