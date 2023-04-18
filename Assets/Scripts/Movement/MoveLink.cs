namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    public class MoveLink : ActionLink {
        GameObject performer;
        Rigidbody2D body;
        Vector2 direction = new Vector2();

        public override GameObject Performer { get => performer; set {
            performer = value;
            body = performer.GetComponent<Rigidbody2D>();
        }}
        public Rigidbody2D Body { get => body; }
        public Vector2 Direction { get => direction; set => direction = value; }
    }
}
