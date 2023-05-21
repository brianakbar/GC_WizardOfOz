namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.Animation;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Aim Action", menuName = "Action/Combat/Aim")]
    public class Aim : BaseAction {
        public class Input {
            public AnimationClip animation;
            public Vector3 targetPosition;
            public Transform rotateableHand;
            public bool rotateWeaponToTarget;
        }

        public override void Initialize(ActionCache cache) {
            cache.Add(new Input());
            cache.Add(cache.GameObject.GetComponent<Animator>());
        }

        public override bool StartAction(ActionCache cache) {
            Input input = cache.Get<Input>();
            Animator animator = cache.Get<Animator>();

            if(input.animation != null) {
                animator.runtimeAnimatorController = animator.CreateOverrides("Aim", input.animation);
            }

            return true;
        }

        public override void Step(ActionCache cache) {
            SetLookDirection(cache);
            SetHandDirection(cache);
        }

        void SetLookDirection(ActionCache cache) {
            Vector3 mouseWorldPoint = cache.Get<Input>().targetPosition;
            float localScaleX = cache.Transform.position.x < mouseWorldPoint.x ? 1 : -1;
            cache.Transform.localScale = new Vector2(localScaleX, cache.Transform.localScale.y);
        }

        void SetHandDirection(ActionCache cache) {
            Input input = cache.Get<Input>();
            if(!input.rotateWeaponToTarget) return;

            var positionInLocal = input.rotateableHand.InverseTransformPoint(input.targetPosition);
            float angle = Mathf.Atan2(positionInLocal.y, positionInLocal.x) * Mathf.Rad2Deg;

            input.rotateableHand.Rotate(0, 0, angle);
        }
    }
}
