namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Idle Action", menuName = "Action/Movement/Idle")]
    public class Idle : BaseAction {
        public override bool StartAction(ActionLink actionLink) {
            IdleLink idleLink = actionLink as IdleLink;
            if(idleLink == null) return false;

            idleLink.Body.velocity = new Vector2(0f, 0f);

            return true;
        }

        public override bool Cancel(ActionLink actionLink) {
            return true;
        }

        public static IdleLink GetLink() {
            return new IdleLink();
        }
    }
}
