namespace Creazen.Wizard.Combat {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.Event.Audio;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Death Action", menuName = "Action/Combat/Death")]
    public class Death : BaseAction {
        [SerializeField] ParticleSystem deathParticle;
        [SerializeField] Vector3 particleSpawnPosition;
        [SerializeField] float waitBeforeDestroy = 0f;
        [SerializeField] AudioClip sfx;

        [Header("Channels")]
        [SerializeField] AudioPlayEventChannel audioPlayChannel;

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
            audioPlayChannel.RaiseEvent(new AudioSetting() {
                Clip = sfx,
                IsOneShot = true
            });

            if(deathParticle != null) {
                ParticleSystem instance = Instantiate(
                    deathParticle, 
                    cache.Transform.position + particleSpawnPosition,
                    Quaternion.identity, 
                    cache.Transform
                );

                yield return new WaitUntil(() => instance.isPlaying);
                yield return new WaitWhile(() => instance.isPlaying);
            }
            
            yield return new WaitForSeconds(waitBeforeDestroy);

            Destroy(cache.GameObject);
        }
    }
}
