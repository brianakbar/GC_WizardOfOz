namespace Creazen.Wizard.Combat {
    using System.Collections.Generic;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Combat/Weapon", order = 0)]
    public class Weapon : ScriptableObject {
        [SerializeField] string weaponName;
        [SerializeField] Sprite sprite;
        [SerializeField] bool isRightHand = true;
        [SerializeField] List<BaseAction> actions = new List<BaseAction>();

        public Sprite GetSprite() {
            return sprite;
        }

        public bool IsRightHand() {
            return isRightHand;
        }
    }
}
