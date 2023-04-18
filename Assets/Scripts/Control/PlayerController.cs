namespace Creazen.Wizard.Control {
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.Movement;
    using UnityEngine;
    using UnityEngine.InputSystem;
    
    public class PlayerController : MonoBehaviour {
        Mover mover;

        void Awake() {
            mover = GetComponent<Mover>();
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
    }
}