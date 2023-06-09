namespace Creazen.Wizard.Core {
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;

    public class Timer : MonoBehaviour {
        [SerializeField] float countdownTime = 1f;
        [SerializeField] UnityEvent onCountdownComplete;
        [SerializeField] bool destroyAfterCountdown = false;

        void Start() {
            StartCoroutine(StartCountdown());
        }

        IEnumerator StartCountdown() {
            yield return new WaitForSeconds(countdownTime);

            onCountdownComplete?.Invoke();

            if(destroyAfterCountdown) Destroy(gameObject);
        }
    }
}