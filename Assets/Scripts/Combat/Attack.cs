namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Attack Action", menuName = "Action/Combat/Attack")]
    public class Attack : BaseAction<AttackLink> {
        [SerializeField] RuntimeAnimatorController attackAnimator;

        public override bool StartAction(AttackLink actionLink) {
            actionLink.AddCombo();
            actionLink.Animator = attackAnimator;

            return true;
        }

        public override bool Cancel(AttackLink actionLink) {
            actionLink.ResetCombo();

            return true;
        }
    }
}
