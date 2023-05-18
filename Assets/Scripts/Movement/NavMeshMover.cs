namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    using UnityEngine.AI;

    public class NavMeshMover : MonoBehaviour {
        [SerializeField] ActionScheduler movementScheduler;

        Animator animator;
        NavMeshAgent agent;

        void Awake() {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
        }

        void Update() {
            animator.SetBool("hasSpeed", agent.velocity.magnitude > Mathf.Epsilon);
        }

        public void StartMoving(string target) {
            movementScheduler.GetCache<NavMeshMove>().Get<NavMeshMove.Input>().target = target;
            movementScheduler.StartAction<NavMeshMove>();
        }

        public void Stop() {
            movementScheduler.StartAction<Idle>();
        }
    }
}