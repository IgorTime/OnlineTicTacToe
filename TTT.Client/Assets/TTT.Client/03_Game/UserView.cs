using TMPro;
using UnityEngine;

namespace TTT.Client.Game
{
    public class UserView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI userName;
        
        [SerializeField]
        private TextMeshProUGUI score;
        
        public void SetUser(string userName, int score)
        {
            this.userName.text = userName;
            this.score.text = score.ToString();
        }
    }
}