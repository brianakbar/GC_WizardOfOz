namespace Creazen.Wizard.Combat {
    using System;
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.Animation;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class Fighter : MonoBehaviour {
        [SerializeField] Weapon currentWeapon;
        [SerializeField] ActionScheduler combatScheduler;
        [SerializeField] Hand rightHand;
        [SerializeField] Hand leftHand;

        bool canAttack = true;
        Action onFinishAttack = null;

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

        public bool StartAttack(Action onFinish) {
            if(!StartAttack()) return false;

            onFinishAttack = onFinish;
            return true;
        }

        public bool StartAttack() {
            if(!canAttack) return false;

            //Attack attack = currentWeapon.GetCombo(attackLink.Combo);
            combatScheduler.StartAction<Attack>();
            
            var overrideAttackAnimation = combatScheduler.GetCache<Attack>().Get<Attack.Link>().animation;
            animator.runtimeAnimatorController = CreateAnimatorOverride("Attack", overrideAttackAnimation);
            
            animator.SetTrigger("attack");
            canAttack = false;
            return true;
        }

        void EquipWeapon(Weapon toEquip) {
            if(toEquip.IsRightHand()) {
                rightHand.EquipWeapon(toEquip.GetSprite());
            }
            else {
                leftHand.EquipWeapon(toEquip.GetSprite());
            }
        }

        AnimatorOverrideController CreateAnimatorOverride(string originalClipName, AnimationClip newClip) {
            if(animator == null) return null;

            AnimatorOverrideController animatorOverride = new AnimatorOverrideController(animator.runtimeAnimatorController);
            AnimationClipOverrides clipOverrides = AnimationClipOverrides.GetOverrides(animator);
            if(clipOverrides == null) {
                animatorOverride[originalClipName] = newClip;
            }
            else {
                clipOverrides[originalClipName] = newClip;
                animatorOverride.ApplyOverrides(clipOverrides);
            }
            
            return animatorOverride;
        }

        //Animation Event
        void OnFinishAttack() {
            combatScheduler.Finish();
            canAttack = true;
            if(onFinishAttack != null) onFinishAttack();
        }
    }
}