namespace Creazen.Wizard.Combat {
    using System;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    public class Fighter : MonoBehaviour {
        [SerializeField] Weapon defaultWeapon;
        [SerializeField] AimTarget target;
        [SerializeField] ActionScheduler combatScheduler;
        [SerializeField] Transform rotateableHand;
        [SerializeField] Hand rightHand;
        [SerializeField] Hand leftHand;

        Weapon currentWeapon;

        bool canAttack = true;

        Action onFinishAttack = null;

        Aim.Input aimInput;
        Attack.Input attackInput;

        void Awake() {
            aimInput = combatScheduler.GetCache<Aim>().Get<Aim.Input>();
            aimInput.rotateableHand = rotateableHand;
            attackInput = combatScheduler.GetCache<Attack>().Get<Attack.Input>();
        }

        void Start() {
            if(currentWeapon == null) EquipWeapon(defaultWeapon);
        }

        void Update() {
            aimInput.targetPosition = target.GetTargetPosition();
        }

        public bool StartAttack(Action onFinish) {
            if(!StartAttack()) return false;

            onFinishAttack = onFinish;
            return true;
        }

        public bool StartAttack() {
            if(!canAttack) return false;
            //Attack attack = currentWeapon.GetCombo(attackLink.Combo);
            attackInput.attackType = currentWeapon.GetCombo(0);
            bool isSuccess = combatScheduler.StartAction<Attack>();

            if(isSuccess) canAttack = false;
            return isSuccess;
        }

        public void EquipWeapon(Weapon toEquip) {
            if(toEquip.IsRightHand()) {
                rightHand.EquipWeapon(toEquip.GetSprite(), toEquip.IsFlipped(), true);
                if(toEquip.IsTwoHanded) {
                    leftHand.EquipWeapon(toEquip.GetSprite(), toEquip.IsFlipped(), false);
                }
            }
            else {
                leftHand.EquipWeapon(toEquip.GetSprite(), toEquip.IsFlipped(), true);
                if(toEquip.IsTwoHanded) {
                    rightHand.EquipWeapon(toEquip.GetSprite(), toEquip.IsFlipped(), false);
                }
            }
            aimInput.animation = toEquip.AimAnimation;
            aimInput.rotateWeaponToTarget = toEquip.IsAimingTarget;
            currentWeapon = toEquip;
        }

        //Animation Event
        void OnFinishAttack() {
            combatScheduler.Finish();
            canAttack = true;
            if(onFinishAttack != null) onFinishAttack();
        }

        void OnSpawnProjectile() {
            combatScheduler.OnSpawnProjectile();
        }
    }
}