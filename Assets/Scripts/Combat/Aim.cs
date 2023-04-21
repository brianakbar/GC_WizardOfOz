namespace Creazen.Wizard.Combat {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Aim Action", menuName = "Action/Combat/Aim")]
    public class Aim : BaseAction, IUpdate {
        public override bool StartAction(ActionLink actionLink) {
            AimLink link = actionLink as AimLink;
            if(link == null) return false;

            return true;
        }

        public override bool Cancel(ActionLink actionLink) {
            AimLink link = actionLink as AimLink;
            if(link == null) return false;

            return true;
        }

        public static AimLink GetLink() {
            return new AimLink();
        }

        void IUpdate.Update(ActionLink actionLink) {
            AimLink link = actionLink as AimLink;
            if(link == null) return;

            SetLookDirection(link);
        }

        void SetLookDirection(AimLink link) {
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(link.MouseScreenPosition);
            float localScaleX = link.Transform.position.x < mouseWorldPoint.x ? 1 : -1;
            link.Transform.localScale = new Vector2(localScaleX, link.Transform.localScale.y);
        }
    }
}
