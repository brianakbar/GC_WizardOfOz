namespace Creazen.Wizard.Combat {
    using System.Collections.Generic;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    public abstract class AttackType : ScriptableObject {
        [SerializeField] protected AnimationClip animation;
        [SerializeField] protected float cooldownAfterFinish = 0f;
        [SerializeField] protected float cooldownAfterCancelled = 0.2f;
        [SerializeField] protected AudioClip sfx;

        public AnimationClip Animation { get => animation; }
        public float CooldownAfterFinish { get => cooldownAfterFinish; }
        public float CooldownAfterCancelled { get => cooldownAfterCancelled; }
        public AudioClip Sfx { get => sfx; }

        public virtual void OnStart(ActionCache cache) {}
        public virtual void HandleTrigger(ActionCache cache, Collider2D other) {}
    }
}
