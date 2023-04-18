namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    public class AttackLink : ActionLink {
        GameObject performer;
        int combo;
        RuntimeAnimatorController animator;
        
        public override GameObject Performer { get => performer; set {
            performer = value;
        }}
        public int Combo { get => combo; }
        public RuntimeAnimatorController Animator { get => animator; set => animator = value; }

        public void AddCombo() {
            combo++;
        }

        public void ResetCombo() {
            combo = 0;
        }
    }
}
