namespace Creazen.Wizard.Control {
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.Movement;
    using UnityEngine;
    using UnityEngine.InputSystem;
    
    public class PlayerController : MonoBehaviour {
        ActionScheduler actionScheduler;
        MoveActionLink moveActionLink;
        IdleActionLink idleActionLink;

        void Awake() {
            actionScheduler = GetComponent<ActionScheduler>();
            moveActionLink = MoveAction.GetLink();
            idleActionLink = IdleAction.GetLink();
        }

        void OnMove(InputValue value) {
            Vector2 direction = value.Get<Vector2>();
            if(direction != Vector2.zero) {
                moveActionLink.Direction = direction;
                actionScheduler.StartAction<MoveAction>(moveActionLink);
            }
            else {
                actionScheduler.StartAction<IdleAction>(idleActionLink);
            }
        }
    }
}