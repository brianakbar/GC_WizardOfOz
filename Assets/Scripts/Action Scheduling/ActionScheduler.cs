namespace Creazen.Wizard.ActionScheduling {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ActionScheduler : MonoBehaviour {
        [SerializeField] GameObject performer;
        [SerializeField] List<BaseAction> actions;

        BaseAction currentAction;

        Dictionary<BaseAction, ActionCache> cache = new Dictionary<BaseAction, ActionCache>();

        Action onFinish;
        Action onCancel;

        public BaseAction CurrentAction { get => currentAction; }

        void Awake() {
            if(performer == null) performer = gameObject;
            foreach(BaseAction action in actions) {
                ActionCache actionCache = new ActionCache();
                actionCache.GameObject = performer;
                actionCache.Transform = performer.transform;
                actionCache.Scheduler = this;
                actionCache.onFinish += Finish;
                cache.Add(action, actionCache);
                action.Initialize(actionCache);
            }
        }

        void Update() {
            if(currentAction == null) {
                StartDefaultAction();
            }

            currentAction.Step(cache[currentAction]);
        }

        void OnDestroy() {
            StopAllCoroutines();
        }

        public void OnTriggerEnter2D(Collider2D other) {
            if(currentAction == null) {
                return;
            }

            currentAction.TriggerEnter2D(cache[currentAction], other);
        }

        public void OnSpawnProjectile() {
            if(currentAction == null) return;

            if(currentAction is ISpawnProjectile spawn) {
                spawn.OnSpawnProjectile(cache[currentAction]);
            }
        }

        public bool StartAction<T>(Action onFinish = null, Action onCancel = null) where T : BaseAction {
            if(StartAction<T>()) {
                this.onFinish = onFinish;
                this.onCancel = onCancel;
                return true;
            }
            return false;
        }

        public bool StartAction<T>() where T : BaseAction {
            foreach(BaseAction action in actions) {
                if(!(action is T)) continue;

                return StartAction(action);
            }
            return false;
        }

        public bool IsPerforming<T>() where T : BaseAction {
            return CurrentAction?.GetType() == typeof(T);
        }

        public void Finish() {
            currentAction?.EndAction(cache[currentAction]);
            if(!StartDefaultAction()) {
                currentAction = null;
            }
            if(onFinish != null) onFinish();
        }

        public bool? Cancel() {
            if(currentAction == null) return null;
            if(!currentAction.CanBeCancelled) return false;

            currentAction.Cancel(cache[currentAction]);
            currentAction = null;
            if(onCancel != null) onCancel();
            return true;
        }

        public ActionCache GetCache<T>() where T : BaseAction {
            foreach(BaseAction action in actions) {
                if(!(action is T)) continue;

                return cache[action];
            }
            return null;
        }

        bool StartDefaultAction() {
            if(actions.Count > 0) {
                return StartAction(actions[0]);
            }
            return false;
        }

        bool StartAction<T>(T action) where T : BaseAction {
            if(action.CanStartBaseAction(cache[action])) {
                if(Cancel() == false) return false;

                currentAction = action;
                action.StartAction(cache[action]);
                return true;
            }
            return false;
        }
    }
}