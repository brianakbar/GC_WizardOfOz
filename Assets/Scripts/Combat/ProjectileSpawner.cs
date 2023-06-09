namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.ActionScheduling.Targets;
    using Creazen.Wizard.Core;
    using UnityEngine;
    
    public class ProjectileSpawner : PrefabSpawner {
        [SerializeField] GameObjectTarget attacker;
        [SerializeField] Target target;
        [SerializeField] DamageType damageType;

        public void SpawnProjectile() {
            GameObject instance = Spawn();
            instance.GetComponent<Projectile>().Launch(attacker.Target, target, damageType);
        }
    }
}