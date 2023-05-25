namespace Creazen.Wizard.Preference {
    using UnityEngine;
    
    public class CharacterSelector : MonoBehaviour {
        [SerializeField] CharacterPreference preference;

        [System.Serializable]
        public class CharacterPreference : Preference {
            [SerializeField] public CharacterData character;

            public override void Handle() {
                Animator animator = GameObject.FindGameObjectWithTag(tag)?.GetComponent<Animator>();
                if(animator) {
                    animator.runtimeAnimatorController = character.animator;
                    animator.Rebind();
                }
            }
        }

        public void Select(CharacterData character) {
            preference.character = character;
            FindObjectOfType<PreferenceManager>().Add(preference);
        }
    }
}