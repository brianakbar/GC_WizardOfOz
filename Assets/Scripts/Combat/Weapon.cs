namespace Creazen.Wizard.Combat {
    using System.Collections.Generic;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Combat/Weapon", order = 0)]
    public class Weapon : ScriptableObject {
        [SerializeField] string weaponName;
        [SerializeField] Sprite sprite;
        [SerializeField] bool isRightHand = true;
        [SerializeField] List<Attack> combos = new List<Attack>();

        public Sprite GetSprite() {
            return sprite;
        }

        public bool IsRightHand() {
            return isRightHand;
        }

        public Attack GetCombo(int index) {
            if(index < 0) return combos[0];
            if(index >= combos.Count) return combos[combos.Count - 1];
            return combos[index];
        }
    }
}
