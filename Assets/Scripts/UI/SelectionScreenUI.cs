namespace Creazen.Wizard.UI {
    using System.Collections.Generic;
    using UnityEngine;
    using Creazen.Wizard.SceneManagement;
    using System.Collections;

    public class SelectionScreenUI : MonoBehaviour {
        [SerializeField] List<Transition> transitions;

        int current = 0;

        Coroutine process = null;

        public void Next() {
            process = StartCoroutine(ProcessNext());
        }

        public void Back() {
            process = StartCoroutine(ProcessBack());
        }

        public IEnumerator ProcessNext() {
            if(process != null) yield break;
            if(current + 1 >= transitions.Count) {
                FindObjectOfType<LevelManager>().LoadNextLevel();
                yield break;
            }

            yield return transitions[current].ProcessTransition(0);
            yield return transitions[++current].ProcessTransition(0);

            process = null;
        }

        public IEnumerator ProcessBack() {
            if(process != null) yield break;
            if(current <= 0) {
                yield break;
            }

            yield return transitions[current].ProcessTransition(1);
            yield return transitions[--current].ProcessTransition(1);

            process = null;
        }
    }
}
