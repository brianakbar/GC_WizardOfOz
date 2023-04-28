namespace Creazen.Wizard.ActionScheduling {
    using System;
    using UnityEngine;

    public class ActionScheduler : MonoBehaviour {
        [SerializeField] GameObject performer;

        BaseAction defaultAction;
        ActionLink defaultActionLink;

        BaseAction currentAction;
        ActionLink currentActionLink;

        Action onFinish;

        void Awake() {
            if(performer == null) performer = gameObject;
        }

        void Update() {
            currentAction.Step(currentActionLink);
        }

        public void OnTriggerEnter2D(Collider2D other) {
            currentAction.TriggerEnter2D(currentActionLink, other);
        }

        public void SetDefaultAction<T>(BaseAction<T> action, T link) where T : ActionLink {
            defaultAction = action;
            defaultActionLink = link;

            if(currentAction == null) {
                StartAction(defaultAction, defaultActionLink);
            }
        }

        public bool StartAction<T>(BaseAction<T> action, T link, Action onFinish) where T : ActionLink {
            this.onFinish = onFinish;
            return StartAction(action as BaseAction, link);
        }

        public bool StartAction<T>(BaseAction<T> action, T link) where T : ActionLink {
            return StartAction(action as BaseAction, link);
        }

        public void Finish() {
            if(defaultAction != null && defaultActionLink != null) {
                StartAction(defaultAction, defaultActionLink);
            }
            else {
                currentAction = null;
                currentActionLink = null;
            }
            if(onFinish != null) onFinish();
        }

        public void Cancel() {
            if(currentAction == null) return;

            currentAction.Cancel(currentActionLink);
            currentAction = null;
            currentActionLink = null;
        }

        bool StartAction(BaseAction action, ActionLink link) {
            Cancel();
            if(link.Performer == null) link.Performer = performer;
            if(link.Scheduler == null) link.Scheduler = this;
            if(!action.StartAction(link)) return false;
            this.currentAction = action;
            this.currentActionLink = link;

            return true;
        }
    }
}