namespace Creazen.Wizard.UI {
    using UnityEngine;
    using UnityEngine.UI;

    public class HealthBarUI : MonoBehaviour {
        [SerializeField] RectMask2D fillAreaMask;

        Canvas canvas;

        void Awake() {
            canvas = GetComponentInChildren<Canvas>();
        }

        public void UpdateHealthBar(float healthFraction) {
            gameObject.SetActive(true);
            float canvasWidth = canvas.GetComponent<RectTransform>()?.rect.width ?? 0;

            fillAreaMask.padding = new Vector4(0, 0, canvasWidth * (1 - healthFraction), 0);
        }
    }
}