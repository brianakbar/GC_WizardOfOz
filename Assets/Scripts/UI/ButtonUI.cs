namespace Creazen.Wizard.UI {
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class ButtonUI : MonoBehaviour, IPointerEnterHandler {
        [SerializeField] UnityEvent onHover;
        [SerializeField] UnityEvent onClick;

        Button button;

        void Awake() {
            button = GetComponent<Button>();
        }

        void OnEnable() {
            button.onClick.AddListener(OnClick);
        }

        void OnDisable() {
            button.onClick.RemoveListener(OnClick);
        }

        public void OnPointerEnter(PointerEventData eventData) {
            onHover?.Invoke();
        }

        void OnClick() {
            onClick?.Invoke();
        }
    }
}