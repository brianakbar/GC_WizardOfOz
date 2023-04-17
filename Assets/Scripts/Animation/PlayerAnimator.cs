namespace Creazen.Wizard.Animation {
    using UnityEngine;

    public class PlayerAnimator : MonoBehaviour {
        [SerializeField] Animator body;
        [SerializeField] Animator hands;

        Rigidbody2D rb2D;

        void Awake() {
            rb2D = GetComponent<Rigidbody2D>();
        }

        void Update() {
            body.SetBool("hasSpeed", rb2D.velocity != Vector2.zero);
        }
    }
}