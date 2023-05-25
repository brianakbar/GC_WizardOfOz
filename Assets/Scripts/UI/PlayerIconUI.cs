namespace Creazen.Wizard.UI {
    using Creazen.Wizard.Preference;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerIconUI : MonoBehaviour {
        void Start() {
            var preference = FindObjectOfType<PreferenceManager>().Get<CharacterSelector.CharacterPreference>();
            GetComponent<Image>().sprite = preference.character.icon;
        }
    }
}