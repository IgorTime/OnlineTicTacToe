using DG.Tweening;
using TMPro;
using UnityEngine;

namespace TTT.Client.Game
{
    public class TurnUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI yourTurnText;
        
        [SerializeField]
        private TextMeshProUGUI opponentTurnText;
        
        [Header("Animation:")]
        [SerializeField]
        private float animationDuration = 0.2f;
        
        [SerializeField]
        private Ease animationEase = Ease.OutBack;
        
        [SerializeField]
        private float delay = 1f;
        
        public void SetTurn(bool isYourTurn)
        {
            yourTurnText.gameObject.SetActive(isYourTurn);
            opponentTurnText.gameObject.SetActive(!isYourTurn);
            
            PlayShowAnimation();
        }

        private void PlayShowAnimation()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1f, animationDuration)
                     .SetEase(animationEase)
                     .SetDelay(delay)
                     .SetLink(gameObject);
        }
    }
}