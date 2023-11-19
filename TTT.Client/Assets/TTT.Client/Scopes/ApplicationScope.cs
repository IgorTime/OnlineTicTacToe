using VContainer;
using VContainer.Unity;

namespace TTT.Client.Root
{
    public class ApplicationScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<NetworkClient>().As<INetworkClient>();
        }
    }
}