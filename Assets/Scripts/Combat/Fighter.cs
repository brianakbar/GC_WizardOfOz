namespace Creazen.Wizard.Combat {
    using System;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class Fighter : MonoBehaviour {
        [SerializeField] Weapon currentWeapon;
        [SerializeField] ActionScheduler combatScheduler;
        [SerializeField] Hand rightHand;
        [SerializeField] Hand leftHand;

        Action onFinishAttack = null;

        void Start() {
            EquipWeapon(currentWeapon);
        }

        void Update() {
            combatScheduler.GetCache<Aim>().Get<Aim.Input>().mouseScreenPosition = Mouse.current.position.ReadValue();
        }

        public bool StartAttack(Action onFinish) {
            if(!StartAttack()) return false;

            onFinishAttack = onFinish;
            return true;
        }

        public bool StartAttack() {
            //Attack attack = currentWeapon.GetCombo(attackLink.Combo);
            return combatScheduler.StartAction<Attack>();
        }

        void EquipWeapon(Weapon toEquip) {
            if(toEquip.IsRightHand()) {
                rightHand.EquipWeapon(toEquip.GetSprite());
            }
            else {
                leftHand.EquipWeapon(toEquip.GetSprite());
            }
        }

        //Animation Event
        void OnFinishAttack() {
            combatScheduler.Finish();
            if(onFinishAttack != null) onFinishAttack();
        }
    }
}