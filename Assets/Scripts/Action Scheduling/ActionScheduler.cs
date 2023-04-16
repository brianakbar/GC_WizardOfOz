namespace Creazen.Wizard.ActionScheduling {
    using System.Collections.Generic;
    using UnityEngine;

    public class ActionScheduler : MonoBehaviour {
        [SerializeField] List<BaseAction> actions;

        BaseAction currentAction;
        ActionLink currentActionLink;

        public bool StartAction<T>(ActionLink actionLink) {
            Cancel();
            BaseAction actionToStart = actions.Find((action) => action.GetType() == typeof(T));
            if(actionToStart == null) return false;
            if(actionLink.Performer == null) actionLink.Performer = gameObject;
            actionToStart.StartAction(actionLink);
            this.currentActionLink = actionLink;
            return true;
        }

        public void Cancel() {
            if(currentAction == null) return;

            currentAction.Cancel(currentActionLink);
            currentAction = null;
            currentActionLink = null;
        }
    }
}