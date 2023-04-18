namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Attack Action", menuName = "Action/Combat/Attack")]
    public class Attack : BaseAction {
        [SerializeField] RuntimeAnimatorController attackAnimator;

        public override bool StartAction(ActionLink actionLink) {
            AttackLink link = actionLink as AttackLink;
            if(link == null) return false;
            link.AddCombo();
            link.Animator = attackAnimator;

            return true;
        }

        public override bool Cancel(ActionLink actionLink) {
            AttackLink link = actionLink as AttackLink;
            if(link == null) return false;
            link.ResetCombo();

            return true;
        }

        public static AttackLink GetLink() {
            return new AttackLink();
        }
    }
}
