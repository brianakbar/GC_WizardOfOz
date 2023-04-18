namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    public class Fighter : MonoBehaviour {
        [SerializeField] Animator handAnimator;
        [SerializeField] Weapon currentWeapon;
        [SerializeField] ActionScheduler combatScheduler;
        [SerializeField] Hand rightHand;
        [SerializeField] Hand leftHand;

        bool canAttack = true;

        AttackLink attackLink;

        void Awake() {
            attackLink = Attack.GetLink();
        }

        void Start() {
            EquipWeapon(currentWeapon);
        }

        void OnFinishAttack() {
            combatScheduler.Finish();
            canAttack = true;
        }

        public void StartAttack() {
            if(!canAttack) return;

            Attack attack = currentWeapon.GetCombo(attackLink.Combo);
            combatScheduler.StartAction(attack, attackLink);
            handAnimator.runtimeAnimatorController = attackLink.Animator;
            handAnimator.SetTrigger("attack");
            canAttack = false;
        }

        void EquipWeapon(Weapon toEquip) {
            if(toEquip.IsRightHand()) {
                rightHand.EquipWeapon(toEquip.GetSprite());
            }
            else {
                leftHand.EquipWeapon(toEquip.GetSprite());
            }
        }
    }
}