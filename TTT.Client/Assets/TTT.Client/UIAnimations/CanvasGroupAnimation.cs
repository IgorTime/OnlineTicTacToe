using UnityEngine;

namespace TTT.Client.UIAnimations
{
    public abstract class CanvasGroupAnimation : UIAnimation<CanvasGroup>
    {
        [SerializeField]
        private CanvasGroup animationTarget;
        
        protected CanvasGroup AnimationTarget { get; private set; }
        
        protected override void InitAnimationTarget()
        {
            AnimationTarget = animationTarget ? animationTarget : GetComponent<CanvasGroup>();
        }
    }
}