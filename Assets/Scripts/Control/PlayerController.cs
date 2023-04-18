namespace Creazen.Wizard.Control {
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.Movement;
    using UnityEngine;
    using UnityEngine.InputSystem;
    
    public class PlayerController : MonoBehaviour {
        ActionScheduler actionScheduler;
        MoveLink moveActionLink;
        IdleLink idleActionLink;

        void Awake() {
            actionScheduler = GetComponent<ActionScheduler>();
            moveActionLink = Move.GetLink();
            idleActionLink = Idle.GetLink();
        }

        void OnMove(InputValue value) {
            Vector2 direction = value.Get<Vector2>();
            if(direction != Vector2.zero) {
                moveActionLink.Direction = direction;
                actionScheduler.StartAction<Move>(moveActionLink);
            }
            else {
                actionScheduler.StartAction<Idle>(idleActionLink);
            }
        }
    }
}