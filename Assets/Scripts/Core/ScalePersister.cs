namespace Creazen.Wizard.Core {
    using UnityEngine;
    
    public class ScalePersister : MonoBehaviour {
        [SerializeField] Transform parent;

        Vector3 originalScale;

        void Awake() {
            originalScale = transform.localScale;
        }

        void LateUpdate() {
            Vector3 newScale = new Vector3(originalScale.x / parent.localScale.x, 1, 1);
            transform.localScale = newScale;
        }
    }
}