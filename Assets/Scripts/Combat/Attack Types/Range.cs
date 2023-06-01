namespace Creazen.Wizard.Combat.AttackTypes {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Range", menuName = "Combat/Attack/Type/Range", order = 0)]
    public class Range : AttackType, ISpawnProjectile {
        [SerializeField] Projectile projectile;
        [SerializeField] DamageType damageType;

        void ISpawnProjectile.OnSpawnProjectile(ActionCache cache) {
            if(cache == null) return;
            if(cache.GameObject == null) return;

            Target target = cache.GameObject.GetComponent<Fighter>().AimTarget;
            Projectile instance = Instantiate(projectile, 
                cache.GameObject.GetComponentInChildren<Hand>().transform.position, 
                Quaternion.identity);
            instance.Launch(cache.GameObject, target, damageType);
        }
    }
}