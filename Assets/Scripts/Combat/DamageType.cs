namespace Creazen.Wizard.Combat {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    public abstract class DamageType : ScriptableObject {
        [SerializeField] protected float damage = 1f;
        [SerializeField] protected AudioClip sfx;

        public float Damage { get => damage; }

        public abstract IEnumerator Handle(ActionCache cache, GameObject attacker);
    }
}
