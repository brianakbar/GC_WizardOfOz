namespace Creazen.Wizard.SceneManagement {
    using System.Collections;
    using System.Collections.Generic;
    using Creazen.Wizard.Preference;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    
    public class LevelManager : MonoBehaviour {
        [SerializeField] string faderTag = "Fader";
        [SerializeField] List<GameObject> levels = new List<GameObject>();

        Transition fader;

        int currentLevel = 0;

        void Awake() {
            fader = GameObject.FindGameObjectWithTag(faderTag).GetComponent<Transition>();
        }

        public void LoadNextLevel() {
            if(SceneManager.GetActiveScene().buildIndex != 1) {
                currentLevel = 0;     
            }
            StartCoroutine(LoadGame(currentLevel));
            currentLevel++;
        }

        IEnumerator LoadGame(int levelToLoad) {
            yield return StartLoadScene(1);

            if(levelToLoad < 0) levelToLoad = 0;
            if(levelToLoad >= levels.Count) levelToLoad = levels.Count - 1;
            Instantiate(levels[levelToLoad]);

            yield return EndLoadScene();
        }

        IEnumerator LoadScene(int buildIndex) {
            yield return StartLoadScene(buildIndex);
            yield return EndLoadScene();
        }

        IEnumerator StartLoadScene(int buildIndex) {
            yield return fader.ProcessTransition(0);
            yield return SceneManager.LoadSceneAsync(buildIndex);
        }

        IEnumerator EndLoadScene() {
            FindObjectOfType<PreferenceManager>()?.Handle();
            yield return fader.ProcessTransition(1);
        }
    }
}
