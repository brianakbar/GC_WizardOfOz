namespace Creazen.Wizard.Core {
    using System.Collections.Generic;
    using UnityEngine;
    
    public class SessionStore : MonoBehaviour {
        Dictionary<string, object> store = new Dictionary<string, object>();

        public void Save<T>(string key, T value) {
            store[key] = value;
        }

        public bool HasKey(string key) {
            return store.ContainsKey(key);
        }

        public T Load<T>(string key) {
            if(!HasKey(key)) return default;
            if(store[key].GetType() != typeof(T)) return default;

            return (T)store[key];
        }
    }
}