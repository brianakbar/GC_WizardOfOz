namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Move Action", menuName = "Action/Move")]
    public class Move : BaseAction {
        [SerializeField] [Min(0)] Vector2 speed = new Vector2(2f, 1f);

        public override bool StartAction(ActionLink actionLink) {
            MoveLink link = actionLink as MoveLink;
            if(link == null) return false;

            link.Body.velocity = link.Direction * speed;

            if(link.Direction.x != 0) {
                link.Transform.localScale = new Vector2(Mathf.Sign(link.Direction.x), link.Transform.localScale.y);
            }

            return true;
        }

        public override bool Cancel(ActionLink actionLink) {
            MoveLink moveLink = actionLink as MoveLink;
            if(moveLink == null) return false;

            moveLink.Body.velocity = new Vector2(0, 0);

            return true;
        }

        public static MoveLink GetLink() {
            return new MoveLink();
        }
    }
}
