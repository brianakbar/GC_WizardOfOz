namespace Creazen.Wizard.Animation {
    using System.Collections.Generic;
    using UnityEngine;

    public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>> {
        public AnimationClipOverrides(int capacity) : base(capacity) {}

        public AnimationClip this[string name] {
            get { return this.Find(x => x.Key.name.Equals(name)).Value; }
            set {
                int index = this.FindIndex(x => x.Key.name.Equals(name));
                if (index != -1)
                    this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
            }
        }

        public static AnimationClipOverrides GetOverrides(Animator animator) {
            if(animator == null) return null;

            AnimatorOverrideController animatorOverride = animator.runtimeAnimatorController as AnimatorOverrideController;
            if(animatorOverride != null) {
                AnimationClipOverrides clipOverrides = new AnimationClipOverrides(animatorOverride.overridesCount);
                animatorOverride.GetOverrides(clipOverrides);
                return clipOverrides;
            }
            return null;
        }
    }
}
