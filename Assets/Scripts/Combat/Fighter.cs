namespace Creazen.Wizard.Combat {
    using System;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    public class Fighter : MonoBehaviour {
        [SerializeField] Weapon defaultWeapon;
        [SerializeField] Target target;
        [SerializeField] ActionScheduler combatScheduler;
        [SerializeField] ActionScheduler aimScheduler;
        [SerializeField] Transform rotateableHand;
        [SerializeField] Hand rightHand;
        [SerializeField] Hand leftHand;

        Weapon currentWeapon;

        bool canAttack = true;

        Action onFinishAttack = null;

        Aim.Input aimInput;
        Attack.Input attackInput;
        Health health;

        public Target AimTarget { get => target; }

        void Awake() {
            aimInput = aimScheduler.GetCache<Aim>().Get<Aim.Input>();
            aimInput.rotateableHand = rotateableHand;
            attackInput = combatScheduler.GetCache<Attack>().Get<Attack.Input>();
            health = GetComponent<Health>();
        }

        void OnEnable() {
            if(health != null) health.onHit.AddListener(OnHit);
        }

        void OnDisable() {
            if(health != null) health.onHit.RemoveListener(OnHit);
        }

        void Start() {
            if(currentWeapon == null) EquipWeapon(defaultWeapon);
            aimScheduler.StartAction<Aim>();
        }

        void Update() {
            aimInput.targetPosition = target.GetTargetPosition(gameObject);
        }

        public void ChangeAim(Target to) {
            target = to;
        }

        public bool StartAttack(int index = 0, Action onFinish = null) {
            if(!StartAttack(index)) return false;

            onFinishAttack = onFinish;
            return true;
        }

        public bool StartAttack(int index = 0) {
            if(!canAttack) return false;
            //Attack attack = currentWeapon.GetCombo(attackLink.Combo);
            attackInput.attackType = currentWeapon.GetCombo(index);
            bool isSuccess = combatScheduler.StartAction<Attack>(FinishAttack, FinishAttack);

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

        void OnHit(float healthFraction) {
            canAttack = true;
        }

        void FinishAttack() {
            canAttack = true;
            if(onFinishAttack != null) onFinishAttack();
        }

        //Animation Event
        void OnFinishAttack() {
            combatScheduler.Finish();
        }

        void OnSpawnProjectile() {
            combatScheduler.OnSpawnProjectile();
        }
    }
}