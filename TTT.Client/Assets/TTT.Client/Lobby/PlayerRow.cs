using TMPro;
using TTT.Shared.Packets.ServerClient;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI userName;

    [SerializeField]
    private TextMeshProUGUI score;

    [SerializeField]
    private Image onlineStatus;

    [SerializeField]
    private Image offlineStatus;

    public void Initialize(PlayerNetDto playerNetDto)
    {
        userName.text = playerNetDto.Username;
        score.text = playerNetDto.Score.ToString();
        onlineStatus.enabled = playerNetDto.IsOnline;
        offlineStatus.enabled = !playerNetDto.IsOnline;
    }
}