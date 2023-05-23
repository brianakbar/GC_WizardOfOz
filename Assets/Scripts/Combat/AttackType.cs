namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    public abstract class AttackType : ScriptableObject {
        [SerializeField] protected AnimationClip animation;
        [SerializeField] protected float cooldownAfterFinish = 0f;
        [SerializeField] protected float cooldownAfterCancelled = 0.2f;

        public AnimationClip Animation { get => animation; }
        public float CooldownAfterFinish { get => cooldownAfterFinish; }
        public float CooldownAfterCancelled { get => cooldownAfterCancelled; }

        public virtual void OnStart(ActionCache cache) {}
        public virtual void HandleTrigger(ActionCache cache, Collider2D other) {}
    }
}
