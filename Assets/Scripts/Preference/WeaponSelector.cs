namespace Creazen.Wizard.Preference {
    using Creazen.Wizard.Combat;
    using UnityEngine;
    
    public class WeaponSelector : MonoBehaviour {
        [SerializeField] WeaponPreference preference;

        [System.Serializable]
        public class WeaponPreference : Preference {
            [SerializeField] public Weapon weapon;

            public override void Handle() {
                Fighter fighter = GameObject.FindGameObjectWithTag(tag)?.GetComponent<Fighter>();
                fighter?.EquipWeapon(weapon);
            }
        }

        public void Select(Weapon weapon) {
            preference.weapon = weapon;
            FindObjectOfType<PreferenceManager>().Add(preference);
        }
    }
}