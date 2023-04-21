namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Idle Action", menuName = "Action/Movement/Idle")]
    public class Idle : BaseAction<IdleLink> {
        public override bool StartAction(IdleLink actionLink) {
            actionLink.Body.velocity = new Vector2(0f, 0f);

            return true;
        }
    }
}
