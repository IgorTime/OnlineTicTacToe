using UnityEngine;

namespace TTT.Client.UIAnimations
{
    public abstract class RectTransformAnimation : UIAnimation<RectTransform>
    {
        [SerializeField]
        [Tooltip("If empty then use self")]
        private RectTransform animationTarget;

        protected RectTransform AnimationTarget { get; private set; }

        protected override void InitAnimationTarget()
        {
            AnimationTarget = animationTarget ? animationTarget : (RectTransform) transform;
        }
    }
}