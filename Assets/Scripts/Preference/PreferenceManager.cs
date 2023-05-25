namespace Creazen.Wizard.Preference {
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class PreferenceManager : MonoBehaviour {
        Dictionary<Type, Preference> preferences = new Dictionary<Type, Preference>();

        public void Add<T>(T preference) where T : Preference {
            preferences[typeof(T)] = preference;
        }

        public T Get<T>() where T : Preference {
            if(!preferences.ContainsKey(typeof(T))) return null;

            return preferences[typeof(T)] as T;
        }

        public void Handle() {
            foreach(Preference preference in preferences.Values) {
                preference.Handle();
            }
        }
    }
}