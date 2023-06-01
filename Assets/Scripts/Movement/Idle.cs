namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Idle Action", menuName = "Action/Movement/Idle")]
    public class Idle : BaseAction {
        public override void Initialize(ActionCache cache) {
            cache.Add(cache.GameObject.GetComponent<Rigidbody2D>());
            cache.Add(cache.GameObject.GetComponent<Animator>());
        }

        public override void OnStartAction(ActionCache cache) {
            cache.Get<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            cache.Get<Animator>().SetBool("hasSpeed", false);
        }
    }
}
