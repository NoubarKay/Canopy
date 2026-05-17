namespace Canopy.Core.Attributes;

/// <summary>
/// Marks a Canopy document subclass as a managed collection.
/// The slug becomes the API route segment and the Postgres table name.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class CollectionAttribute : Attribute
{
    /// <summary>
    /// URL-safe slug for this collection, e.g. "articles", "authors".
    /// Used as the table name and REST route segment.
    /// </summary>
    public string Slug { get; set; }
 
    /// <summary>
    /// Human-readable label shown in the admin UI. Defaults to the slug if not set.
    /// </summary>
    public string? Label { get; set; }
    
    public CollectionAttribute(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Collection slug must not be empty.", nameof(slug));
 
        Slug = slug.ToLowerInvariant().Trim();
    }
}