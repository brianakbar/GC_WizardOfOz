namespace Creazen.Wizard.Combat.AttackTypes {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Range", menuName = "Combat/Attack/Type/Range", order = 0)]
    public class Range : AttackType {
        [SerializeField] Projectile projectile;
        [SerializeField] DamageType damageType;

        public override void OnStart(ActionCache cache) {
            Projectile instance = Instantiate(projectile, 
                cache.GameObject.GetComponentInChildren<Hand>().transform.position, 
                Quaternion.identity);
            instance.Launch(damageType);
        }
    }
}