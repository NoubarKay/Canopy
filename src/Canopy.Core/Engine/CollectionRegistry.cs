using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using Canopy.Core.Attributes;
using Canopy.Core.Collections;
using Canopy.Core.Engine.MetaData;

namespace Canopy.Core.Engine;

public sealed class CollectionRegistry
{
    private readonly Dictionary<string, CollectionMetaData> _collections = new();
    private static readonly HashSet<string> _baseProperties = ["Id", "CreatedAt", "UpdatedAt"];
    private static readonly Regex _safeSlug = new(@"^[a-z][a-z0-9_]*$", RegexOptions.Compiled);
    private bool _initialized = false;
    
    public IReadOnlyDictionary<string, CollectionMetaData> Collections =>                                                                                                                                    
        _initialized ? _collections : throw new InvalidOperationException("Registry has not been built yet.");

    public void Register(Assembly assembly)
    {
        if(_initialized) throw new InvalidOperationException("Registry is already initialized.");
        var collectionTypes = assembly.GetTypes()
            .Where(x=>x.IsClass 
                      && x.IsSubclassOf(typeof(CanopyDocument))
                      && !x.IsAbstract
                      && x.GetCustomAttribute<CollectionAttribute>() is not null);

        foreach (var collectionType in collectionTypes)
        {
            var attribute = collectionType.GetCustomAttribute<CollectionAttribute>();
            
            var slug = attribute!.Slug;
            if (!_safeSlug.IsMatch(slug))
                throw new ArgumentException($"Collection slug '{slug}' is not safe.", nameof(slug));
            
            if (_collections.ContainsKey(slug))                                                                                                                                                                      
                throw new InvalidOperationException($"Duplicate collection slug '{slug}'.");
            
            var label = attribute.Label ?? collectionType.Name;
            
            var fields = collectionType
                .GetProperties()
                .Where(x=>x.GetCustomAttribute<CanopyBaseField>() is not null 
                          && !_baseProperties.Contains(x.Name));

            foreach (var field in fields)
            {
                
            }


            _collections[slug] = new CollectionMetaData
            {
                Slug = slug,
                Label = label,
                ClrType = collectionType,
            };
        }
    }
    
    public void Initialize() => _initialized = true;
}