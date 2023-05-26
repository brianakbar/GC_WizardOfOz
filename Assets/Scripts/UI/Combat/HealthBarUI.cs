namespace Creazen.Wizard.UI.Combat {
    using Creazen.Wizard.Event.Combat;
    using UnityEngine;
    using UnityEngine.UI;

    public class HealthBarUI : MonoBehaviour {
        [SerializeField] RectTransform container;
        [SerializeField] RectMask2D fillAreaMask;

        [Header("Listening on channels")]
        [SerializeField] HealthChangeEventChannel healthChangeChannel;

        void OnEnable() {
            if(healthChangeChannel != null) healthChangeChannel.onEventRaised += UpdateHealthBar;
        }

        void OnDisable() {
            if(healthChangeChannel != null) healthChangeChannel.onEventRaised -= UpdateHealthBar;
        }

        public void UpdateHealthBar(float healthFraction) {
            gameObject.SetActive(true);
            float canvasWidth = container.rect.width;

            fillAreaMask.padding = new Vector4(0, 0, canvasWidth * (1 - healthFraction), 0);
        }
    }
}