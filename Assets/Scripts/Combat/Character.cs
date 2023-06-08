namespace Creazen.Wizard.Combat {
    using System.Collections.Generic;
    using UnityEngine;
    
    public class Character : MonoBehaviour {
        [SerializeField] CharacterType type;

        public bool Is(CharacterType type) {
           return this.type.Equals(type);
        }

        // public bool Is(IEnumerable<CharacterType> attackableTypes) {
        //     foreach(CharacterType attackable in attackableTypes) {
        //         if(Is(attackable)) return true;
        //     }

        //     return false;
        // }

        public bool IsFriendly(Character other) {
            return type.IsFriendly(other.type);
        }
    }
}