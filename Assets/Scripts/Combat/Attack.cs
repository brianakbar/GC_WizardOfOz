namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Attack Action", menuName = "Action/Combat/Attack")]
    public class Attack : BaseAction {
        [SerializeField] Animation attackAnimation;

        public override bool StartAction(ActionLink actionLink) {
            return true;
        }

        public override bool Cancel(ActionLink actionLink) {
            return true;
        }

        public static AttackLink GetLink() {
            return new AttackLink();
        }
    }
}
