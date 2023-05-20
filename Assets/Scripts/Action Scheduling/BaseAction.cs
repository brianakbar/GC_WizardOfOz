namespace Creazen.Wizard.ActionScheduling {
    using UnityEngine;

    public abstract class BaseAction : ScriptableObject {
        public void Finish(ActionCache cache) {
            EndAction(cache);
            cache.OnFinish();
        }

        public virtual void Initialize(ActionCache cache) {}
        public virtual bool StartAction(ActionCache cache) { return true; }
        public virtual void EndAction(ActionCache cache) {}
        public virtual void Step(ActionCache cache) {}
        public virtual void TriggerEnter2D(ActionCache cache, Collider2D other) {}
        public virtual void Cancel(ActionCache cache) {}
    }
}