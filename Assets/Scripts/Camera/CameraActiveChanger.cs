namespace Creazen.Wizard.Camera {
    using Cinemachine;
    using UnityEngine;
    using UnityEngine.Events;

    public class CameraActiveChanger : MonoBehaviour {
        [SerializeField] string triggerToTag = "Player";
        [SerializeField] string changeTo;
        [SerializeField] UnityEvent onChangeCamera;

        Animator brainAnimator;

        void Awake() {
            brainAnimator = FindObjectOfType<CinemachineStateDrivenCamera>().GetComponent<Animator>();
        }

        public void ChangeVirtualCamera(string state) {
            brainAnimator.Play(state);
            onChangeCamera?.Invoke();
        }

        void OnTriggerEnter2D(Collider2D other) {
            if(!string.IsNullOrWhiteSpace(triggerToTag)) {
                if(other.tag != triggerToTag) return;
            }

            ChangeVirtualCamera(changeTo);
        }
    }
}