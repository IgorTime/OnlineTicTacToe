using DG.Tweening;
using TriInspector;
using UnityEngine;

[DeclareFoldoutGroup("Debug")]
public class MoveAnimation : MonoBehaviour
{
    [SerializeField]
    private bool animateOnStart = true;

    [SerializeField]
    [Tooltip("If empty then use self")]
    private RectTransform animationTarget;

    [Header("Animation:")]
    [SerializeField]
    private float animationDuration = 0.3f;

    [SerializeField]
    private Ease animationEase = Ease.OutCubic;

    [SerializeField]
    private RectTransform startPosition;

    [SerializeField]
    private RectTransform endPosition;

    private Tweener moveAnimation;

    private RectTransform AnimationTarget { get; set; }

    private void Awake()
    {
        InitAnimationTarget();
        CreateAnimation();

        if (animateOnStart)
        {
            PlayShow();
        }
    }

    private void OnDestroy()
    {
        DOTween.Kill(moveAnimation);
    }

    [Button]
    [Group("Debug")]
    public void PlayShow()
    {
        moveAnimation.PlayForward();
    }

    [Button]
    [Group("Debug")]
    public void PlayHide()
    {
        moveAnimation.PlayBackwards();
    }

    private void InitAnimationTarget()
    {
        AnimationTarget = animationTarget ? animationTarget : (RectTransform) transform;
    }

    private void CreateAnimation()
    {
        moveAnimation = DOTween.To(Move, 0f, 1f, animationDuration)
                               .SetEase(animationEase)
                               .SetLink(gameObject)
                               .SetAutoKill(false);

        moveAnimation.Rewind();
    }

    private void Move(float progress)
    {
        AnimationTarget.position = Vector3.LerpUnclamped(
            startPosition.position,
            endPosition.position,
            progress);
    }
}