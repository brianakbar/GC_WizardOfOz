namespace Creazen.Wizard.Control {
    using Creazen.Wizard.Combat;
    using Creazen.Wizard.Movement;
    using UnityEngine;
    using UnityEngine.InputSystem;
    
    public class PlayerController : MonoBehaviour {
        Mover mover;
        Fighter fighter;

        void Awake() {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        void OnMove(InputValue value) {
            Vector2 direction = value.Get<Vector2>();
            if(direction != Vector2.zero) {
                mover.StartMoving(direction);
            }
            else {
                mover.Stop();
            }
        }

        void OnAttack(InputValue value) {
            fighter.StartAttack();
        }
    }
}