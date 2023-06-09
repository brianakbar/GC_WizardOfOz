namespace Creazen.Wizard.ActionScheduling {
    using System.Collections;
    using UnityEngine;

    public abstract class BaseAction : ScriptableObject {
        [SerializeField] bool canBeCancelled = true;
        [SerializeField] protected float cooldownDuration = 0f;
        [SerializeField] bool cancelOtherActionsWhenStarted = false;

        public bool CanBeCancelled { get => canBeCancelled; }

        public class Cooldown {
            public float duration = 0f;

            public bool IsInCooldown { get => duration > 0f; }
        }

        public Coroutine StartCoroutine(ActionCache cache, IEnumerator routine) {
            if(cache.Scheduler?.enabled ?? false) {
                return cache.Scheduler.StartCoroutine(routine);
            }
            return null;
        }

        public void StartCooldown(ActionCache cache, float duration) {
            if(cache.Get<Cooldown>() == null) {
                cache.Add(new Cooldown());
            }

            Cooldown cooldown = cache.Get<Cooldown>();
            cooldown.duration = duration;

            StartCoroutine(cache, ProcessCooldown(cooldown));
        }

        public IEnumerator ProcessCooldown(Cooldown cooldown) {
            while(true) {
                yield return null;
                cooldown.duration -= Time.deltaTime;
                if(cooldown.duration <= 0f) {
                    yield break;
                }
            }
        }

        public void Finish(ActionCache cache) {
            EndAction(cache);
            cache.OnFinish();
        }

        public bool CanStartBaseAction(ActionCache cache) {
            Cooldown cooldown = cache.Get<Cooldown>();
            if(cooldown != null) {
                if(cooldown.IsInCooldown) return false;
            }
            return CanStartAction(cache);
        }

        public void StartAction(ActionCache cache) {
            if(cancelOtherActionsWhenStarted) {
                CancelOtherActions(cache);
            }
            OnStartAction(cache);
        }

        public void EndAction(ActionCache cache) {
            OnEndAction(cache);
            StartCooldown(cache, cooldownDuration);
        }

        public void Cancel(ActionCache cache) {
            OnCancel(cache);
            StartCooldown(cache, cooldownDuration);
        }

        void CancelOtherActions(ActionCache cache) {
            foreach(var scheduler in cache.GameObject.GetComponentsInChildren<ActionScheduler>()) {
                if(scheduler == cache.Scheduler) continue;

                scheduler.Cancel();
            }
        }

        public virtual void Initialize(ActionCache cache) {}
        public virtual bool CanStartAction(ActionCache cache) { return true; }
        public virtual void OnStartAction(ActionCache cache) {}
        public virtual void OnEndAction(ActionCache cache) {}
        public virtual void Step(ActionCache cache) {}
        public virtual void TriggerEnter2D(ActionCache cache, Collider2D other) {}
        public virtual void OnCancel(ActionCache cache) {}
    }
}