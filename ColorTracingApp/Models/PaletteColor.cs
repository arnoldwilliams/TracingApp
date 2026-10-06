using Microsoft.Maui.Graphics;

namespace ColorTracingApp.Models;

/// <summary>
/// A named pen colour offered in the tracing palette.
/// </summary>
public sealed class PaletteColor
{
    public required string Name { get; init; }

    public required Color Color { get; init; }

    public bool IsSelected { get; set; }
}
