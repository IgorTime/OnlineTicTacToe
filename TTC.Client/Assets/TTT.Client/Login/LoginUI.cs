using System.Threading.Tasks;
using TMPro;
using TTC.Shared.Packets.ClientServer;
using TTC.Shared.Packets.ServerClient;
using TTT.Client.PacketHandlers;
using UnityEngine;
using UnityEngine.UI;

namespace TTT.Client.Login
{
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

        [SerializeField] 
        private GameObject loginError;

        private void Awake()
        {
            loginButton.onClick.AddListener(Login);
            sendButton.onClick.AddListener(Send);
            OnAuthFailHandler.OnAuthFail += OnAuthFail;
        }

        private void OnDestroy()
        {
            OnAuthFailHandler.OnAuthFail -= OnAuthFail;
        }

        private void OnAuthFail(NetOnAuthFail obj)
        {
            loginButton.interactable = true;
            loginError.gameObject.SetActive(true);
        }

        private async void Login()
        {
            loginButton.interactable = false;
            loginError.gameObject.SetActive(false);
        
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
}