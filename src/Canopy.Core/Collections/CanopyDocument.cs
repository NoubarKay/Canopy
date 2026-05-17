namespace Canopy.Core.Collections;

/// <summary>
/// Base class for all Crabshell collection documents.
/// Every collection gets Id, CreatedAt, and UpdatedAt for free.
/// </summary>
public abstract class CanopyDocument
{
    /// <summary>Unique identifier for this document. Defaults to a new <see cref="Guid"/>.</summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>UTC timestamp when this document was first created.</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>UTC timestamp when this document was last updated.</summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}