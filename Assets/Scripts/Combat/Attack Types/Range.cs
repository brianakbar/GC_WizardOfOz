namespace Creazen.Wizard.Combat.AttackTypes {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Range", menuName = "Combat/Attack/Type/Range", order = 0)]
    public class Range : AttackType, ISpawnProjectile {
        [SerializeField] Projectile projectile;
        [SerializeField] DamageType damageType;

        void ISpawnProjectile.OnSpawnProjectile(ActionCache cache) {
            Projectile instance = Instantiate(projectile, 
                cache.GameObject.GetComponentInChildren<Hand>().transform.position, 
                Quaternion.identity);
            instance.Launch(cache.GameObject, damageType);
        }
    }
}