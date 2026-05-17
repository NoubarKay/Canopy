namespace Canopy.Core.Engine.MetaData;

/// <summary>Runtime descriptor for a registered collection or singleton, built from attribute metadata at startup.</summary>
public sealed class CollectionMetaData
{
    /// <summary>URL-safe identifier used as the table name and admin route segment.</summary>
    public required string Slug { get; init; }

    /// <summary>Human-readable label shown in the admin sidebar.</summary>
    public required string Label { get; init; }

    /// <summary>The CLR type of the <see cref="Documents.CrabshellDocument"/> subclass.</summary>
    public required Type ClrType { get; init; }
}