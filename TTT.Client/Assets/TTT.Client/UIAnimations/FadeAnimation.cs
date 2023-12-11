namespace TTT.Client.UIAnimations
{
    public class FadeAnimation : CanvasGroupAnimation
    {
        protected override void Animate(float progress)
        {
            AnimationTarget.alpha = progress;
        }
    }
}