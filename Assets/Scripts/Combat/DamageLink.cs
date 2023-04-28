namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    public class DamageLink : ActionLink {
        GameObject performer;
        Rigidbody2D body;
        Vector3 knockback;
        float knockbackTime;
        
        public override GameObject Performer { get => performer; set {
            performer = value;
            body = performer.GetComponent<Rigidbody2D>();
        }}
        public Rigidbody2D Body { get => body; }
        public Vector3 Knockback { get => knockback; set => knockback = value; }
        public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }
    }
}
