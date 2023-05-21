namespace Creazen.Wizard.Animation {
    using UnityEngine;

    public static class AnimatorExtension {
        public static AnimatorOverrideController CreateOverrides(this Animator animator, string originalClipName, AnimationClip newClip) {
            AnimatorOverrideController animatorOverride = new AnimatorOverrideController(animator.runtimeAnimatorController);
            AnimationClipOverrides clipOverrides = AnimationClipOverrides.GetOverrides(animator);
            if(clipOverrides == null) {
                animatorOverride[originalClipName] = newClip;
            }
            else {
                clipOverrides[originalClipName] = newClip;
                animatorOverride.ApplyOverrides(clipOverrides);
            }
            
            return animatorOverride;
        }
    }
}