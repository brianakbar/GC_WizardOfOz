namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class Fighter : MonoBehaviour {
        [SerializeField] Weapon currentWeapon;
        [SerializeField] ActionScheduler combatScheduler;
        [SerializeField] Hand rightHand;
        [SerializeField] Hand leftHand;

        bool canAttack = true;

        Animator animator;

        AimLink aimLink;
        AttackLink attackLink;

        void Awake() {
            animator = GetComponent<Animator>();
            attackLink = Attack.GetLink();
            aimLink = Aim.GetLink();
        }

        void Start() {
            EquipWeapon(currentWeapon);
            combatScheduler.SetDefaultAction(currentWeapon.GetAim(), aimLink);
        }

        void Update() {
            aimLink.MouseScreenPosition = Mouse.current.position.ReadValue();
        }

        void OnFinishAttack() {
            combatScheduler.Finish();
            canAttack = true;
        }

        public void StartAttack() {
            if(!canAttack) return;

            Attack attack = currentWeapon.GetCombo(attackLink.Combo);
            combatScheduler.StartAction(attack, attackLink);
            animator.runtimeAnimatorController = attackLink.Animator;
            animator.SetTrigger("attack");
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