namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Move Action", menuName = "Action/Movement/Move")]
    public class Move : BaseAction<MoveLink> {
        [SerializeField] [Min(0)] Vector2 speed = new Vector2(2f, 1f);

        public override bool StartAction(MoveLink actionLink) {
            actionLink.Body.velocity = actionLink.MoveDirection * speed;

            return true;
        }

        public override bool Cancel(MoveLink actionLink) {
            actionLink.Body.velocity = new Vector2(0, 0);

            return true;
        }
    }
}
