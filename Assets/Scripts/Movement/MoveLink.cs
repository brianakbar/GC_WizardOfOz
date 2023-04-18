namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    public class MoveLink : ActionLink {
        GameObject performer;
        Rigidbody2D body;
        Vector2 moveDirection = new Vector2();

        public override GameObject Performer { get => performer; set {
            performer = value;
            body = performer.GetComponent<Rigidbody2D>();
        }}
        public Rigidbody2D Body { get => body; }
        public Vector2 MoveDirection { get => moveDirection; set => moveDirection = value; }
    }
}
