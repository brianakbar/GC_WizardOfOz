namespace Creazen.Wizard.ActionScheduling {
    using System.Collections;
    using UnityEngine;

    public abstract class BaseAction : ScriptableObject {
        public Coroutine StartCoroutine(ActionCache cache, IEnumerator routine) {
            if(cache.Scheduler?.enabled ?? false) {
                return cache.Scheduler.StartCoroutine(routine);
            }
            return null;
        }

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