﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TTT.Shared.Attributes;
using TTT.Shared.Handlers;

namespace TTT.Server.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPacketHandlers(this IServiceCollection serviceCollection)
    {
        var handlers = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(x => x.DefinedTypes)
                                .Where(t => t is {IsInterface: false, IsAbstract: false})
                                .Where(x => typeof(IPacketHandler).IsAssignableFrom(x))
                                .Select(t => (type: t, attibute: t.GetCustomAttribute<HandlerRegisterAttribute>()))
                                .Where(x => x.attibute != null);

        foreach (var (type, _) in handlers)
        {
            serviceCollection.AddScoped(type);
        }

        return serviceCollection;
    }
}