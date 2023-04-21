namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Aim Action", menuName = "Action/Combat/Aim")]
    public class Aim : BaseAction<AimLink> {
        public override void OnUpdate(AimLink actionLink) {
            SetLookDirection(actionLink);
        }

        void SetLookDirection(AimLink link) {
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(link.MouseScreenPosition);
            float localScaleX = link.Transform.position.x < mouseWorldPoint.x ? 1 : -1;
            link.Transform.localScale = new Vector2(localScaleX, link.Transform.localScale.y);
        }
    }
}
