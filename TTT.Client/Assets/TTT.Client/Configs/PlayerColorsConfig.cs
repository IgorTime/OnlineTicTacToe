using UnityEngine;

namespace TTT.Client.Configs
{
    [CreateAssetMenu(fileName = "PlayerColors", menuName = "TTT/Configs", order = 0)]
    public class PlayerColorsConfig : ScriptableObject
    {
        [SerializeField]
        private Color xPlayerColor = Color.yellow;

        [SerializeField]
        private Color oPlayerColor = Color.cyan;

        public Color XPlayerColor => xPlayerColor;
        public Color OPlayerColor => oPlayerColor;
    }
}