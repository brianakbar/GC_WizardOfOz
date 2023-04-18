namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    public class AttackLink : ActionLink {
        GameObject performer;
        Rigidbody2D body;
        
        public override GameObject Performer { get => performer; set {
            performer = value;
            body = performer.GetComponent<Rigidbody2D>();
        }}
        public Rigidbody2D Body { get => body; }
    }
}
