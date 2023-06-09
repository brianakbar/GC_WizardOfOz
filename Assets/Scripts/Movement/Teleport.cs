namespace Creazen.Wizard.Movement {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    using UnityEngine.AI;

    [CreateAssetMenu(fileName = "New Teleport Action", menuName = "Action/Movement/Teleport")]
    public class Teleport : BaseAction {
        [SerializeField] float waitTimeBeforeTeleport = 1f;

        public class Input {
            public Vector3 teleportPosition;
        }

        public override void Initialize(ActionCache cache) {
            cache.Add(cache.GameObject.GetComponent<NavMeshAgent>());
            cache.Add(cache.GameObject.GetComponent<Animator>());
            cache.Add(new Input());
        }

        public override void OnStartAction(ActionCache cache) {
            StartCoroutine(cache, ProcessTeleport(cache));
        }

        IEnumerator ProcessTeleport(ActionCache cache) {
            var agent = cache.Get<NavMeshAgent>();
            var animator = cache.Get<Animator>();

            animator.SetTrigger("teleport");
            animator.SetBool("isTeleporting", true);
            yield return new WaitForSeconds(waitTimeBeforeTeleport);

            if(agent != null) {
                agent.Warp(cache.Get<Input>().teleportPosition);
            }
            else {
                cache.GameObject.transform.position = cache.Get<Input>().teleportPosition;
            }

            animator.SetBool("isTeleporting", false);
        }
    }
}
