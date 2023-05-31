namespace Creazen.Wizard.Combat {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Death Action", menuName = "Action/Combat/Death")]
    public class Death : BaseAction {
        [SerializeField] ParticleSystem deathParticle;
        [SerializeField] Vector3 particleSpawnPosition;

        public override void Initialize(ActionCache cache) {
            cache.Add(cache.GameObject.GetComponent<Animator>());
        }

        public override void OnStartAction(ActionCache cache) {
            Animator animator = cache.Get<Animator>();

            animator.SetTrigger("dead");

            foreach(var scheduler in cache.GameObject.GetComponentsInChildren<ActionScheduler>()) {
                if(scheduler == cache.Scheduler) continue;

                scheduler.Cancel();
            }

            cache.Scheduler.StartCoroutine(ProcessDeath(cache));
        }

        IEnumerator ProcessDeath(ActionCache cache) {
            ParticleSystem instance = Instantiate(
                deathParticle, 
                cache.Transform.position + particleSpawnPosition,
                Quaternion.identity, 
                cache.Transform
            );

            yield return new WaitUntil(() => instance.isPlaying);
            yield return new WaitWhile(() => instance.isPlaying);

            Destroy(cache.GameObject);
        }
    }
}
