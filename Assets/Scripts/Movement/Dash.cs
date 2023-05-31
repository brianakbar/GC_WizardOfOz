namespace Creazen.Wizard.Movement {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Dash Action", menuName = "Action/Movement/Dash")]
    public class Dash : BaseAction {
        [SerializeField] [Min(0)] Vector2 speed = new Vector2(2f, 1f);
        [SerializeField] [Min(0)] float duration = 0.2f;

        public class Input {
            public Vector2 moveDirection = Vector2.right;
        }

        public override void Initialize(ActionCache cache) {
            cache.Add(cache.GameObject.GetComponent<Rigidbody2D>());
            cache.Add(new Input());
        }

        public override void OnStartAction(ActionCache cache) {
            StartCoroutine(cache, ProcessDash(cache));
        }

        public override void OnCancel(ActionCache cache) {
            cache.Get<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        public override void OnEndAction(ActionCache cache) {
            cache.Get<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        IEnumerator ProcessDash(ActionCache cache) {
            cache.Get<Rigidbody2D>().velocity = cache.Get<Input>().moveDirection * speed;
            yield return new WaitForSeconds(duration);
            cache.Scheduler.Finish();
        }
    }
}
