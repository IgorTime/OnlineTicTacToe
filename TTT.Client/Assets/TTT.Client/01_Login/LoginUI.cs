using System.Threading.Tasks;
using TMPro;
using TTT.Client.PacketHandlers;
using TTT.Client.User;
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
        private GameObject loginError;

        private INetworkClient networkClient;
        private IUserService userService;

        [Inject]
        public void Construct(
            INetworkClient networkClient,
            IUserService userService)
        {
            this.networkClient = networkClient;
            this.userService = userService;
        }

        private void Start()
        {
            loginButton.onClick.AddListener(Login);
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
            userService.RegisterUser(request.Username);
        }
    }
}