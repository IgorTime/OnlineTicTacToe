using Cysharp.Threading.Tasks;
using DG.Tweening;
using TriInspector;
using UnityEngine;

namespace TTT.Client.UIAnimations
{
    public abstract class UIAnimation : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("If empty then use self")]
        private RectTransform animationTarget;

        [Header("Start:")]
        [SerializeField]
        private bool animateOnStart = true;

        [SerializeField]
        [ShowIf(nameof(animateOnStart))]
        private float startDelay;

        [Header("Animation:")]
        [SerializeField]
        private float animationDuration = 0.3f;

        [SerializeField]
        private Ease animationEase = Ease.OutCubic;

        private Tweener uiAnimation;

        protected RectTransform AnimationTarget { get; private set; }
        public bool IsPlaying => uiAnimation.IsPlaying();

        private async UniTaskVoid Awake()
        {
            InitAnimationTarget();
            CreateAnimation();

            if (animateOnStart)
            {
                if (startDelay > 0)
                {
                    await UniTask.Delay((int) (startDelay * 1000), cancellationToken: destroyCancellationToken);
                    destroyCancellationToken.ThrowIfCancellationRequested();
                }

                PlayForward();
            }
        }

        private void OnDestroy()
        {
            DOTween.Kill(uiAnimation);
        }

        [Button]
        [Group("Debug")]
        public void PlayForward()
        {
            uiAnimation.PlayForward();
        }

        [Button]
        [Group("Debug")]
        public void PlayBackward()
        {
            uiAnimation.PlayBackwards();
        }

        protected abstract void Animate(float progress);

        private void InitAnimationTarget()
        {
            AnimationTarget = animationTarget ? animationTarget : (RectTransform) transform;
        }

        private void CreateAnimation()
        {
            uiAnimation = DOTween.To(Animate, 0f, 1f, animationDuration)
                                 .SetEase(animationEase)
                                 .SetLink(gameObject)
                                 .SetAutoKill(false);

            uiAnimation.Rewind();
        }
    }
}