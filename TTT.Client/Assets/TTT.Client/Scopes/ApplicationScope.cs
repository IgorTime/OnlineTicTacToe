using TTT.Client.PacketHandlers;
using TTT.Client.Services;
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

            builder.RegisterBuildCallback(container =>
            {
                var sceneLoader = container.Resolve<ISceneLoader>();
                sceneLoader.LoadLoginScene();
            });
        }
    }
}