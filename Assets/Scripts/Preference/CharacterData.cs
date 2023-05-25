namespace Creazen.Wizard.Preference {
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Character Data", menuName = "Preference/Character", order = 0)]
    public class CharacterData : ScriptableObject {
        public Sprite icon;
        public RuntimeAnimatorController animator;
    }
}