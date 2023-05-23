namespace Creazen.Wizard.Combat {
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Combat/Weapon", order = 0)]
    public class Weapon : ScriptableObject {
        [SerializeField] string weaponName;
        [SerializeField] Sprite sprite;
        [SerializeField] bool isRightHand = true;
        [SerializeField] bool isTwoHanded = false;
        [SerializeField] bool isFlipped = false;
        [SerializeField] bool isAimingTarget = false;
        [SerializeField] AnimationClip aimAnimation;
        [SerializeField] List<AttackType> combos = new List<AttackType>();

        public AnimationClip AimAnimation { get => aimAnimation; }
        public bool IsTwoHanded { get => isTwoHanded; }
        public bool IsAimingTarget { get => isAimingTarget; }

        public Sprite GetSprite() {
            return sprite;
        }

        public bool IsRightHand() {
            return isRightHand;
        }

        public bool IsFlipped() {
            return isFlipped;
        }

        public AttackType GetCombo(int index) {
            if(index < 0) return combos[0];
            if(index >= combos.Count) return combos[combos.Count - 1];
            return combos[index];
        }
    }
}
