namespace Canopy.Core.Attributes.Fields;

/// <summary>
/// Maps to a varchar or text column. Use MaxLength = -1 for unbounded text.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class TextFieldAttribute : CanopyBaseField
{
    /// <summary>Max character length. -1 = unlimited (TEXT column). Default 255.</summary>
    public int MaxLength { get; set; } = 255;

    /// <summary>Minimum character length. Validated on save.</summary>
    public int MinLength { get; set; } = 0;
    
    /// <summary>Placeholder text shown in the admin UI.</summary>
    public string? Placeholder { get; set; }
}