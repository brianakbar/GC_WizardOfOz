namespace Creazen.Wizard.ActionScheduling {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ActionCache {
        public GameObject GameObject { get; set; }
        public Transform Transform { get; set; }
        public ActionScheduler Scheduler { get; set; }
        Dictionary<Type, object> cache = new Dictionary<Type, object>();

        public event Action onFinish;
        
        public bool Add<T>(T item) {
            Type itemType = typeof(T);
            if(cache.ContainsKey(itemType)) return false;
            
            cache[itemType] = item;

            return true;
        }

        public T Get<T>() {
            Type itemType = typeof(T);
            if (cache.ContainsKey(itemType)) {
                return (T)cache[itemType];
            }
            else {
                return default(T);
            }
        }

        public void OnFinish() {
            if(onFinish != null) onFinish();
        }
    }
}