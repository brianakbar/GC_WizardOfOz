namespace Creazen.Wizard.UI {
    using Creazen.Wizard.Preference;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerIconUI : MonoBehaviour {
        void Start() {
            var preference = FindObjectOfType<PreferenceManager>().Get<CharacterSelector.CharacterPreference>();
            if(preference == null) return;
            
            GetComponent<Image>().sprite = preference.character.icon;
        }
    }
}