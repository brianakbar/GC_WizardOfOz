namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    using UnityEngine.AI;

    [CreateAssetMenu(fileName = "New Nav Mesh Move Action", menuName = "Action/Movement/Nav Mesh Move")]
    public class NavMeshMove : BaseAction {
        [SerializeField] [Min(0)] float speed = 2f;

        int retryCount = 0;
        const int Retry = 10;

        public class Input {
            public Target target;
        }

        public override void Initialize(ActionCache cache) {
            var agent = cache.GameObject.GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            cache.Add(agent);
            cache.Add(cache.GameObject.GetComponent<Animator>());
            cache.Add(new Input());
        }

        public override void OnStartAction(ActionCache cache) {
            NavMeshAgent agent = cache.Get<NavMeshAgent>();
            agent.isStopped = false;
            agent.speed = speed;
            retryCount = 0;
        }

        public override void Step(ActionCache cache) {
            Target target = cache.Get<Input>().target;
            NavMeshAgent agent = cache.Get<NavMeshAgent>();
            if(IsApproximately(cache.Transform.position, target.GetTargetPosition(cache.GameObject))) {
                cache.Scheduler.Finish();
            }

            agent.SetDestination(target.GetTargetPosition(cache.GameObject));

            if(agent.velocity.magnitude <= Mathf.Epsilon) {
                if(++retryCount >= Retry) {
                    cache.Scheduler.Finish();
                }
            }
            else {
                retryCount = 0;
            }

            cache.Get<Animator>().SetBool("hasSpeed", agent.velocity.magnitude > Mathf.Epsilon);
        }

        public override void OnCancel(ActionCache cache) {
            NavMeshAgent agent = cache.Get<NavMeshAgent>();
            agent.isStopped = true;
            agent.SetDestination(cache.GameObject.transform.position);
            cache.Get<Animator>().SetBool("hasSpeed", false);
        }

        public override void OnEndAction(ActionCache cache) {
            NavMeshAgent agent = cache.Get<NavMeshAgent>();
            agent.isStopped = true;
            agent.SetDestination(cache.GameObject.transform.position);
            cache.Get<Animator>().SetBool("hasSpeed", false);
        }

        bool IsApproximately(Vector2 a, Vector2 b) {
            if(Approximately(a.x, b.x, 0.1f) && Approximately(a.y, b.y, 0.1f)) return true;

            return false;
        }

        bool Approximately(float a, float b, float maxDifference) {
            if (a - maxDifference < b && b < a + maxDifference) {
                return true;
            }
            return false;
        }
    }
}
