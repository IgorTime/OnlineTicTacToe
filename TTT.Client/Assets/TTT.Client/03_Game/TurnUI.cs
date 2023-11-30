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
        
        public void SetTurn(bool isYourTurn)
        {
            yourTurnText.gameObject.SetActive(isYourTurn);
            opponentTurnText.gameObject.SetActive(!isYourTurn);
        }
    }
}