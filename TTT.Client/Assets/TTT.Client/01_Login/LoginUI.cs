using System;
using Cysharp.Threading.Tasks;
using MessagePipe;
using TMPro;
using TTT.Client.LocalMessages;
using TTT.Client.User;
using TTT.Shared.Packets.ClientServer;
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
        private IDisposable unsubscribe;

        [Inject]
        public void Construct(
            INetworkClient networkClient,
            IUserService userService,
            ISubscriber<OnAuthFailed> onAuthFiled)
        {
            this.networkClient = networkClient;
            this.userService = userService;

            var bag = DisposableBag.CreateBuilder();
            onAuthFiled.Subscribe(OnAuthFail).AddTo(bag);
            unsubscribe = bag.Build();
        }

        private void Start()
        {
            loginButton.onClick.AddListener(Login);
        }

        private void OnDestroy()
        {
            unsubscribe?.Dispose();
        }

        private void OnAuthFail(OnAuthFailed message)
        {
            loginButton.interactable = true;
            loginError.gameObject.SetActive(true);
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(usernameInput.text) ||
                string.IsNullOrEmpty(passwordInput.text))
            {
                return;
            }

            loginButton.interactable = false;
            loginError.gameObject.SetActive(false);

            networkClient.Connect();

            while (!networkClient.IsConnected)
            {
                await UniTask.Yield();
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