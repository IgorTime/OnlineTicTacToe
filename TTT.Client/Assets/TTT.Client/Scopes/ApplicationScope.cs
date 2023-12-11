using MessagePipe;
using TTT.Client.Gameplay;
using TTT.Client.LocalMessages;
using TTT.Client.PacketHandlers;
using TTT.Client.Services;
using TTT.Client.User;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TTT.Client.Scopes
{
    public class ApplicationScope : LifetimeScope
    {
        [Header("TicTacToe Global Settings:")]
        [SerializeField]
        private NetworkSettings networkSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(networkSettings);
            builder.RegisterEntryPoint<NetworkClient>().As<INetworkClient>();
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IPacketHandlerResolver, PacketHandlerResolver>(Lifetime.Singleton);
            builder.Register<IGameManager, GameManager>(Lifetime.Singleton);
            builder.Register<IUserService, UserService>(Lifetime.Singleton);

            var options = RegisterMessagePipe(builder);
            RegisterMessages(builder, options);

            builder.RegisterBuildCallback(container =>
            {
                var sceneLoader = container.Resolve<ISceneLoader>();
                sceneLoader.LoadLoginScene();
            });
        }

        private static MessagePipeOptions RegisterMessagePipe(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe();
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
            return options;
        }

        private void RegisterMessages(IContainerBuilder builder, MessagePipeOptions options)
        {
            builder.RegisterMessageBroker<OnCellMarked>(options);
            builder.RegisterMessageBroker<OnPlayAgain>(options);
            builder.RegisterMessageBroker<OnGameRestart>(options);
            builder.RegisterMessageBroker<OnSurrender>(options);
            builder.RegisterMessageBroker<OnOpponentQuitGame>(options);
            builder.RegisterMessageBroker<OnServerStatusUpdated>(options);
        }
    }
}