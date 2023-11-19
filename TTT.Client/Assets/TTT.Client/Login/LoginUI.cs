using System.Threading.Tasks;
using TMPro;
using TTT.Client.PacketHandlers;
using TTT.Shared.Packets.ClientServer;
using TTT.Shared.Packets.ServerClient;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

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

        private INetworkClient networkClient;

        [Inject]
        public void Construct(
            INetworkClient networkClient)
        {
            this.networkClient = networkClient;
        }

        private void Start()
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

            networkClient.Connect();

            while (!networkClient.IsConnected)
            {
                await Task.Yield();
            }

            var request = new NetAuthRequest
            {
                Username = usernameInput.text,
                Password = passwordInput.text,
            };

            networkClient.SendServer(request);
        }

        private void Send()
        {
            var request = new NetAuthRequest
            {
                Username = usernameInput.text,
                Password = passwordInput.text,
            };

            networkClient.SendServer(request);
        }
    }
}