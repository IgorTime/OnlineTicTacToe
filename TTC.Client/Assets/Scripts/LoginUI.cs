using System.Threading.Tasks;
using TMPro;
using TTC.Shared.Packets.ClientServer;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField usernameInput;
    
    [SerializeField]
    private TMP_InputField passwordInput;
    
    [SerializeField]
    private Button loginButton;

    [SerializeField]
    private Button sendButton;

    private void Awake()
    {
        loginButton.onClick.AddListener(Login);
        sendButton.onClick.AddListener(Send);
    }

    private async void Login()
    {
        loginButton.interactable = false;
        NetworkClient.Instance.Connect();

        while (!NetworkClient.Instance.IsConnected)
        {
            await Task.Yield();
        }
        
        var request = new NetAuthRequest()
        {
            Username = usernameInput.text,
            Password = passwordInput.text,
        };
        
        NetworkClient.Instance.SendServer(request);
    }

    private void Send()
    {
        var request = new NetAuthRequest()
        {
            Username = usernameInput.text,
            Password = passwordInput.text,
        };
        
        NetworkClient.Instance.SendServer(request);
    }
}