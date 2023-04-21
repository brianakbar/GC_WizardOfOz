namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    public class AimLink : ActionLink {
        GameObject performer;
        Vector3 mouseScreenPosition;
        
        public override GameObject Performer { get => performer; set {
            performer = value;
        }}
        public Vector3 MouseScreenPosition { get => mouseScreenPosition; set => mouseScreenPosition = value; }
    }
}
