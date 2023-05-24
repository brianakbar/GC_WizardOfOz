namespace Creazen.Wizard.SceneManagement {
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    
    public class LevelManager : MonoBehaviour {
        [SerializeField] string faderTag = "Fader";

        Transition fader;

        void Awake() {
            fader = GameObject.FindGameObjectWithTag(faderTag).GetComponent<Transition>();
        }

        public void LoadNextLevel() {
            if(SceneManager.GetActiveScene().buildIndex != 1) {
                StartCoroutine(LoadScene(1));
                return;
            }
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
        }

        IEnumerator LoadScene(int buildIndex) {
            yield return fader.ProcessTransition(0);
            yield return SceneManager.LoadSceneAsync(buildIndex);
            yield return fader.ProcessTransition(1);
        }
    }
}
