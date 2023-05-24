namespace Creazen.Wizard.SceneManagement {
    using System.Collections;
    using System.Collections.Generic;
    using Creazen.Wizard.Animation;
    using UnityEngine;

    public class Transition : MonoBehaviour {
        [SerializeField] float animationLength = 3f;
        [SerializeField] string defaultTransitionName = "Transition";
        [SerializeField] List<AnimationClip> transitions;

        bool isTransitionFinished;

        Animator animator;

        void Awake() {
            animator = GetComponent<Animator>();
        }

        public void StartTransition(int index) {
            StartCoroutine(ProcessTransition(index));
        }

        public IEnumerator ProcessTransition(int index) {
            isTransitionFinished = false;
            animator.speed = 1 / animationLength;
            int indexToUse = index >= 0 ? index : 0;
            indexToUse = index < transitions.Count ? index : transitions.Count - 1;
            animator.runtimeAnimatorController = animator.CreateOverrides(defaultTransitionName, transitions[indexToUse]);
            animator.Rebind();
            yield return new WaitUntil(() => isTransitionFinished);
        }

        public void Finished() {
            isTransitionFinished = true;
        }
    }
}