namespace Creazen.Wizard.Combat {
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Character Type", menuName = "Combat/Character Type", order = 0)]
    public class CharacterType : ScriptableObject {
        [SerializeField] string type;
        [SerializeField] List<CharacterType> friends = new List<CharacterType>();

        public bool Equals(CharacterType other) {
            if(type != other.type) return false;

            return true;
        }

        public bool IsFriendly(CharacterType other) {
            return friends.Contains(other);
        }
    }
}