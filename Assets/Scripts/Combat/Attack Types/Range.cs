namespace Creazen.Wizard.Combat.AttackTypes {
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.Event.Audio;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Range", menuName = "Combat/Attack/Type/Range", order = 0)]
    public class Range : AttackType, ISpawnProjectile {
        [SerializeField] Projectile projectile;
        [SerializeField] DamageType damageType;
        [SerializeField] Target spawnTarget;
        [SerializeField] Vector3 spawnOffset;
        [SerializeField] Target target;

        [Header("Channels")]
        [SerializeField] AudioPlayEventChannel audioPlayChannel;

        void ISpawnProjectile.OnSpawnProjectile(ActionCache cache) {
            if(cache == null) return;
            if(cache.GameObject == null) return;

            Vector3 spawnPosition = spawnTarget?.GetTargetPosition(cache.GameObject) ?? default;
            if(spawnTarget == null) {
                spawnPosition = cache.GameObject.GetComponentInChildren<Hand>().transform.position;
            }
            spawnPosition += spawnOffset;

            Target projectileTarget = target;
            if(projectileTarget == null) projectileTarget = cache.GameObject.GetComponent<Fighter>().AimTarget;
            Projectile instance = Instantiate(projectile, 
                spawnPosition, 
                Quaternion.identity);
            instance.Launch(cache.GameObject, projectileTarget, damageType);

            audioPlayChannel.RaiseEvent(new AudioSetting() {
                Clip = Sfx,
                IsOneShot = true
            });
        }
    }
}