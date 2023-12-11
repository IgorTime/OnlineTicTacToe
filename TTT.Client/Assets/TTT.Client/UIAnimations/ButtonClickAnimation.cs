using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TTT.Client.UIAnimations
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private float animationDuration = 0.1f;

        [SerializeField]
        private float clickedScale = 0.9f;

        [SerializeField]
        private Ease animationEase = Ease.Linear;

        private Tweener clickAnimation;

        private void Awake()
        {
            InitAnimation();
        }

        private void OnDestroy()
        {
            DOTween.Kill(clickAnimation);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            clickAnimation.PlayForward();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            clickAnimation.PlayBackwards();
        }

        private void InitAnimation()
        {
            clickAnimation = DOTween.To(Animate, 0f, 1f, animationDuration)
                                    .SetEase(animationEase)
                                    .SetLink(gameObject)
                                    .SetAutoKill(false);

            clickAnimation.Rewind();
        }

        private void Animate(float progress)
        {
            transform.localScale = Vector3.LerpUnclamped(
                Vector3.one,
                Vector3.one * clickedScale,
                progress);
        }
    }
}