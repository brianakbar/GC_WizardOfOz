namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    using UnityEngine.InputSystem;

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

        void Update() {
            animator.SetBool("hasSpeed", rb2D.velocity != Vector2.zero);
            SetLookDirection();
        }

        public void StartMoving(Vector2 direction) {
            moveActionLink.MoveDirection = direction;
            movementScheduler.StartAction(move, moveActionLink);
        }

        public void Stop() {
            movementScheduler.StartAction(idle, idleActionLink);
        }

        void SetLookDirection() {
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            float localScaleX = transform.position.x < mouseWorldPoint.x ? 1 : -1;
            transform.localScale = new Vector2(localScaleX, transform.localScale.y);
        }
    }
}