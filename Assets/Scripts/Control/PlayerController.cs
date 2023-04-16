namespace Creazen.Wizard.Control {
    using UnityEngine;
    using UnityEngine.InputSystem;
    
    public class PlayerController : MonoBehaviour {
        void OnMove(InputValue value) {
            Debug.Log(value.Get<Vector2>());
        }
    }
}