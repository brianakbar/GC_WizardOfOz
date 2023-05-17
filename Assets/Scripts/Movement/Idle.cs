namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Idle Action", menuName = "Action/Movement/Idle")]
    public class Idle : BaseAction {
        public override void Initialize(ActionCache cache) {
            cache.Add(cache.GameObject.GetComponent<Rigidbody2D>());
        }

        public override bool StartAction(ActionCache cache) {
            cache.Get<Rigidbody2D>().velocity = new Vector2(0f, 0f);

            return true;
        }
    }
}
