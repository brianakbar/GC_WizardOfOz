namespace Creazen.Wizard.ActionScheduling {
    using UnityEngine;

    public abstract class ActionLink {
        public abstract GameObject Performer { get; set; }
        public Transform Transform { get => Performer.transform; }
        public ActionScheduler Scheduler { get; set; }
    }
}
