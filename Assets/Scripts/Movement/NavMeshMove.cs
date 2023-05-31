namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    using UnityEngine.AI;

    [CreateAssetMenu(fileName = "New Nav Mesh Move Action", menuName = "Action/Movement/Nav Mesh Move")]
    public class NavMeshMove : BaseAction {
        [SerializeField] [Min(0)] float speed = 2f;

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
        }

        public override void Step(ActionCache cache) {
            Target target = cache.Get<Input>().target;
            NavMeshAgent agent = cache.Get<NavMeshAgent>();
            if(IsApproximately(cache.Transform.position, target.GetTargetPosition())) {
                cache.Scheduler.Finish();
            }

            agent.SetDestination(target.GetTargetPosition());

            cache.Get<Animator>().SetBool("hasSpeed", agent.velocity.magnitude > Mathf.Epsilon);
        }

        public override void OnCancel(ActionCache cache) {
            NavMeshAgent agent = cache.Get<NavMeshAgent>();
            agent.isStopped = true;
            agent.SetDestination(cache.GameObject.transform.position);
            cache.Get<Animator>().SetBool("hasSpeed", false);
        }

        bool IsApproximately(Vector2 a, Vector2 b) {
            if(Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y)) return true;

            return false;
        }
    }
}
