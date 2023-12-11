using UnityEngine;

namespace TTT.Client.UIAnimations
{
    public class ScaleAnimation : RectTransformAnimation
    {
        [SerializeField]
        private Vector3 startScale = Vector3.zero;

        [SerializeField]
        private Vector3 endScale = Vector3.one;

        protected override void Animate(float progress)
        {
            AnimationTarget.localScale = Vector3.LerpUnclamped(
                startScale,
                endScale,
                progress);
        }
    }
}