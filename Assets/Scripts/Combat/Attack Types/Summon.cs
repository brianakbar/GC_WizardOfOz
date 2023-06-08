namespace Creazen.Wizard.Combat.AttackTypes {
    using System.Collections.Generic;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Summon", menuName = "Combat/Attack/Type/Summon", order = 0)]
    public class Summon : AttackType, ISpawnProjectile {
        [SerializeField] List<GameObject> gameObjects;
        [SerializeField] float summonRadius = 1f;
        [SerializeField] int number = 1;
        [SerializeField] Target summonTarget;

        void ISpawnProjectile.OnSpawnProjectile(ActionCache cache) {
            if(cache == null) return;
            if(cache.GameObject == null) return;

            for(int i = 0; i < number; i++) {
                Vector3 summonPosition = summonTarget.GetTargetPosition(cache.GameObject);
                summonPosition = new Vector3(
                    summonPosition.x + summonRadius * Random.insideUnitCircle.x,
                    summonPosition.y + summonRadius * Random.insideUnitCircle.y,
                    0f
                );
                Instantiate(gameObjects[Random.Range(0, gameObjects.Count)], summonPosition, Quaternion.identity);
            }
        }
    }
}