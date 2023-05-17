namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    public class Mover : MonoBehaviour {
        [SerializeField] ActionScheduler movementScheduler;

        Animator animator;
        Rigidbody2D rb2D;

        void Awake() {
            animator = GetComponent<Animator>();
            rb2D = GetComponent<Rigidbody2D>();
        }

        void Update() {
            animator.SetBool("hasSpeed", rb2D.velocity != Vector2.zero);
        }

        public void StartMoving(Vector2 direction) {
            var input = movementScheduler.GetCache<Move>().Get<Move.Input>();
            input.moveDirection = direction;
            movementScheduler.StartAction<Move>();
        }

        public void Stop() {
            movementScheduler.StartAction<Idle>();

        }
    }
}