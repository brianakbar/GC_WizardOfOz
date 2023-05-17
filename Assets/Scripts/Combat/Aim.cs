namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Aim Action", menuName = "Action/Combat/Aim")]
    public class Aim : BaseAction {
        public class Input {
            public Vector3 mouseScreenPosition;
        }

        public override void Initialize(ActionCache cache) {
            cache.Add(new Input());
        }

        public override void Step(ActionCache cache) {
            SetLookDirection(cache);
        }

        void SetLookDirection(ActionCache cache) {
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(cache.Get<Input>().mouseScreenPosition);
            float localScaleX = cache.Transform.position.x < mouseWorldPoint.x ? 1 : -1;
            cache.Transform.localScale = new Vector2(localScaleX, cache.Transform.localScale.y);
        }
    }
}
