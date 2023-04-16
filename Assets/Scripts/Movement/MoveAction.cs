namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Move Action", menuName = "Action/Move")]
    public class MoveAction : BaseAction {
        [SerializeField] [Min(0)] Vector2 speed = new Vector2(2f, 1f);

        public override bool StartAction(ActionLink actionLink) {
            MoveActionLink link = actionLink as MoveActionLink;
            if(link == null) return false;

            link.Body.velocity = link.Direction * speed;

            if(link.Direction.x != 0) {
                link.Transform.localScale = new Vector2(Mathf.Sign(link.Direction.x), link.Transform.localScale.y);
            }

            return true;
        }

        public override bool Cancel(ActionLink actionLink) {
            MoveActionLink moveActionLink = actionLink as MoveActionLink;
            if(moveActionLink == null) return false;

            moveActionLink.Body.velocity = new Vector2(0, 0);

            return true;
        }

        public static MoveActionLink GetLink() {
            return new MoveActionLink();
        }
    }
}
