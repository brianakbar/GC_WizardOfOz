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

        void Awake() {
            animator = GetComponent<Animator>();
        }

        void Start() {
            EquipWeapon(currentWeapon);
        }

        void Update() {
            combatScheduler.GetCache<Aim>().Get<Aim.Input>().mouseScreenPosition = Mouse.current.position.ReadValue();
        }

        void OnFinishAttack() {
            combatScheduler.Finish();
            canAttack = true;
        }

        public void StartAttack() {
            if(!canAttack) return;

            //Attack attack = currentWeapon.GetCombo(attackLink.Combo);
            combatScheduler.StartAction<Attack>();
            animator.runtimeAnimatorController =  combatScheduler.GetCache<Attack>().Get<Attack.Link>().animator;
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