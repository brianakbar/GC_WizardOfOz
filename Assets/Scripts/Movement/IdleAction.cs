namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Idle Action", menuName = "Action/Idle")]
    public class IdleAction : BaseAction {
        public override bool StartAction(ActionLink actionLink) {
            IdleActionLink link = actionLink as IdleActionLink;
            if(link == null) return false;

            link.Body.velocity = new Vector2(0f, 0f);

            return true;
        }

        public override bool Cancel(ActionLink actionLink) {
            return true;
        }

        public static IdleActionLink GetLink() {
            return new IdleActionLink();
        }
    }
}
