namespace Creazen.Wizard.UI {
    using System.Collections;
    using Creazen.Wizard.SceneManagement;
    using UnityEngine;
    
    public class ShowHideUI : MonoBehaviour {
        [SerializeField] GameObject container;
        [SerializeField] float waitTime = 2f;

        public void SetActive(bool state) {
            StartCoroutine(ProcessSetActive(state));
        }

        IEnumerator ProcessSetActive(bool state) {
            yield return Wait();

            container.SetActive(state);
            GetComponent<Transition>().StartTransition(0);
        }

        IEnumerator Wait() {
            yield return new WaitForSeconds(waitTime);
        }
    }
}