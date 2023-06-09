namespace Creazen.Wizard.Control {
    using Creazen.Wizard.Combat;
    using Creazen.Wizard.Movement;
    using UnityEngine;
    using UnityEngine.InputSystem;
    
    public class PlayerController : MonoBehaviour {
        Mover mover;
        Fighter fighter;

        Vector2 moveDirection = Vector2.zero;

        void Awake() {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        void OnMove(InputValue value) {
            moveDirection = value.Get<Vector2>();
            Move();
        }

        void Move() {
            if (moveDirection != Vector2.zero) {
                mover.StartMoving(moveDirection);
            }
            else {
                mover.Stop();
            }
        }

        void OnDash(InputValue value) {
            mover.Dash(moveDirection, Move);
        }

        void OnAttack(InputValue value) {
            fighter.StartAttack(0);
        }
    }
}