using Cysharp.Threading.Tasks;
using DG.Tweening;
using TriInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TTT.Client.UIAnimations
{
    public abstract class UIAnimation<T> : MonoBehaviour
        where T : Object
    {
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
        public bool IsPlaying => uiAnimation.IsPlaying();

        private void Awake()
        {
            InitAnimationTarget();
            CreateAnimation();
        }

        private void Start()
        {
            if (animateOnStart)
            {
                PlayOnStart().Forget();
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

        protected abstract void InitAnimationTarget();

        private async UniTaskVoid PlayOnStart()
        {
            if (startDelay > 0)
            {
                Animate(0f);
                await UniTask.Delay((int) (startDelay * 1000), cancellationToken: destroyCancellationToken);
                destroyCancellationToken.ThrowIfCancellationRequested();
            }

            PlayForward();
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