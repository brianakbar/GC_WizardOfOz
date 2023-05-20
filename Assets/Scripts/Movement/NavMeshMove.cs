namespace Creazen.Wizard.Movement {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    using UnityEngine.AI;

    [CreateAssetMenu(fileName = "New Nav Mesh Move Action", menuName = "Action/Movement/Nav Mesh Move")]
    public class NavMeshMove : BaseAction {
        [SerializeField] [Min(0)] float speed = 2f;

        class Record {
            public Transform target;
        }

        public class Input {
            public string target = "Player";
        }

        public override void Initialize(ActionCache cache) {
            var agent = cache.GameObject.GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            cache.Add(agent);
            cache.Add(cache.GameObject.GetComponent<Animator>());
            cache.Add(new Input());
            cache.Add(new Record());
        }

        public override bool StartAction(ActionCache cache) {
            NavMeshAgent agent = cache.Get<NavMeshAgent>();
            agent.isStopped = false;
            GameObject target = GameObject.FindGameObjectWithTag(cache.Get<Input>().target);
            cache.Get<Record>().target = target.transform;
            agent.speed = speed;

            return true;
        }

        public override void Step(ActionCache cache) {
            Transform target = cache.Get<Record>().target;
            NavMeshAgent agent = cache.Get<NavMeshAgent>();
            agent.SetDestination(target.transform.position);

            cache.Get<Animator>().SetBool("hasSpeed", agent.velocity.magnitude > Mathf.Epsilon);
        }

        public override void Cancel(ActionCache cache) {
            NavMeshAgent agent = cache.Get<NavMeshAgent>();
            agent.isStopped = true;
            agent.SetDestination(cache.GameObject.transform.position);
            cache.Get<Animator>().SetBool("hasSpeed", false);
        }
    }
}
