namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    public class Mover : MonoBehaviour {
        [SerializeField] ActionScheduler movementScheduler;
        [SerializeField] Idle idle;
        [SerializeField] Move move;

        Animator animator;
        Rigidbody2D rb2D;

        MoveLink moveActionLink;
        IdleLink idleActionLink;

        void Awake() {
            animator = GetComponent<Animator>();
            rb2D = GetComponent<Rigidbody2D>();
            moveActionLink = Move.GetLink();
            idleActionLink = Idle.GetLink();
        }

        void Start() {
            movementScheduler.SetDefaultAction(idle, idleActionLink);
        }

        void Update() {
            animator.SetBool("hasSpeed", rb2D.velocity != Vector2.zero);
        }

        public void StartMoving(Vector2 direction) {
            moveActionLink.MoveDirection = direction;
            movementScheduler.StartAction(move, moveActionLink);
        }

        public void Stop() {
            movementScheduler.StartAction(idle, idleActionLink);
        }
    }
}