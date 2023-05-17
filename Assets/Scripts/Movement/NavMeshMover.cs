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

        void Start() {
            StartMoving();
        }

        void Update() {
            animator.SetBool("hasSpeed", agent.velocity.magnitude > Mathf.Epsilon);
        }

        public void StartMoving() {
            movementScheduler.StartAction<NavMeshMove>();
        }

        public void Stop() {
            movementScheduler.StartAction<Idle>();

        }
    }
}