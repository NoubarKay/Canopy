using System.Reflection;
using Canopy.Core.Engine;
using Microsoft.Extensions.DependencyInjection;

namespace Canopy.Core;

public static class DependencyInjection
{
    /// <summary>
    /// Scans <paramref name="assemblies"/> for <c>[Collection]</c> types,
    /// builds the <see cref="CollectionRegistry"/>, and registers it as a singleton.
    /// </summary>
    public static IServiceCollection AddCanopyCore(this IServiceCollection services, params Assembly[] assemblies)
    {
        var collectionRegistry = new CollectionRegistry();
        foreach (var assembly in assemblies)
            collectionRegistry.Register(assembly);
        
        collectionRegistry.Initialize();
        services.AddSingleton(collectionRegistry);

        return services;
    }

}