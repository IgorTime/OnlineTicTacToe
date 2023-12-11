using TriInspector;
using TTT.Client.UIAnimations;
using UnityEngine;

[DeclareFoldoutGroup("Debug")]
public class MoveAnimation : RectTransformAnimation
{
    [SerializeField]
    private RectTransform startPosition;

    [SerializeField]
    private RectTransform endPosition;

    protected override void Animate(float progress)
    {
        AnimationTarget.position = Vector3.LerpUnclamped(
            startPosition.position,
            endPosition.position,
            progress);
    }
}