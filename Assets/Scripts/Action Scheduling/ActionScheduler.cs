namespace Creazen.Wizard.ActionScheduling {
    using System;
    using UnityEngine;

    public class ActionScheduler : MonoBehaviour {
        [SerializeField] GameObject performer;

        BaseAction defaultAction;
        ActionLink defaultActionLink;

        BaseAction currentAction;
        ActionLink currentActionLink;

        void Awake() {
            if(performer == null) performer = gameObject;
        }

        void Update() {
            IUpdate actionUpdate = currentAction as IUpdate;
            if(actionUpdate != null) {
                actionUpdate.Update(currentActionLink);
            }
        }

        public void SetDefaultAction<T>(BaseAction<T> action, T actionLink) where T : ActionLink {
            defaultAction = action;
            defaultActionLink = actionLink;

            if(currentAction == null) {
                StartAction(defaultAction, defaultActionLink);
            }
        }

        public bool StartAction<T>(BaseAction<T> action, T actionLink) where T : ActionLink {
            return StartAction(action as BaseAction, actionLink);
        }

        public void Finish() {
            if(defaultAction != null && defaultActionLink != null) {
                StartAction(defaultAction, defaultActionLink);
            }
            else {
                currentAction = null;
                currentActionLink = null;
            }
        }

        public void Cancel() {
            if(currentAction == null) return;

            currentAction.Cancel(currentActionLink);
            currentAction = null;
            currentActionLink = null;
        }

        bool StartAction(BaseAction action, ActionLink actionLink) {
            Cancel();
            if(actionLink.Performer == null) actionLink.Performer = performer;
            if(!action.StartAction(actionLink)) return false;
            this.currentAction = action;
            this.currentActionLink = actionLink;

            return true;
        }
    }
}