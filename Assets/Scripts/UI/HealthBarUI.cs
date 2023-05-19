namespace Creazen.Wizard.UI {
    using UnityEngine;
    using UnityEngine.UI;

    public class HealthBarUI : MonoBehaviour {
        [SerializeField] RectTransform container;
        [SerializeField] RectMask2D fillAreaMask;

        public void UpdateHealthBar(float healthFraction) {
            gameObject.SetActive(true);
            float canvasWidth = container.rect.width;

            fillAreaMask.padding = new Vector4(0, 0, canvasWidth * (1 - healthFraction), 0);
        }
    }
}