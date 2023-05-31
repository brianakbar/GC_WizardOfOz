namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Move Action", menuName = "Action/Movement/Move")]
    public class Move : BaseAction {
        [SerializeField] [Min(0)] Vector2 speed = new Vector2(2f, 1f);

        public class Input {
            public Vector2 moveDirection = Vector2.right;
        }

        public override void Initialize(ActionCache cache) {
            cache.Add(cache.GameObject.GetComponent<Rigidbody2D>());
            cache.Add(cache.GameObject.GetComponent<Animator>());
            cache.Add(new Input());
        }

        public override void OnStartAction(ActionCache cache) {
            cache.Get<Rigidbody2D>().velocity = cache.Get<Input>().moveDirection * speed;
        }

        public override void Step(ActionCache cache) {
            cache.Get<Animator>().SetBool("hasSpeed", cache.Get<Rigidbody2D>().velocity != Vector2.zero);
        }

        public override void OnCancel(ActionCache cache) {
            cache.Get<Rigidbody2D>().velocity = new Vector2(0, 0);
            cache.Get<Animator>().SetBool("hasSpeed", false);
        }
    }
}
