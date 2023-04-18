namespace Creazen.Wizard.ActionScheduling {
    using System.Collections.Generic;
    using UnityEngine;

    public class ActionScheduler : MonoBehaviour {
        BaseAction currentAction;
        ActionLink currentActionLink;

        public bool StartAction(BaseAction action, ActionLink actionLink) {
            Cancel();
            if(actionLink.Performer == null) actionLink.Performer = gameObject;
            action.StartAction(actionLink);
            this.currentActionLink = actionLink;
            return true;
        }

        public void Finish() {
            currentAction = null;
            currentActionLink = null;
        }

        public void Cancel() {
            if(currentAction == null) return;

            currentAction.Cancel(currentActionLink);
            currentAction = null;
            currentActionLink = null;
        }
    }
}